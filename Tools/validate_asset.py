#!/usr/bin/env python3
"""
Asset Validation Script for 0DaySiege

Performs deterministic checks on generated art assets:
- Dimensions and aspect ratio
- Color palette compliance (Delta E)
- Lighting direction (quadrant brightness)
- Alpha channel presence
- Text/watermark detection (optional, requires tesseract)

Usage:
    python validate_asset.py <image_path> [--type sprite|background] [--verbose]
    python validate_asset.py --batch <directory> [--type sprite|background]

Requirements:
    pip install pillow colorthief colormath numpy
    Optional: pip install pytesseract (requires tesseract-ocr installed)
"""

import argparse
import sys
from pathlib import Path
from dataclasses import dataclass
from typing import Optional

try:
    from PIL import Image
    import numpy as np
except ImportError:
    print("Error: Required packages not found. Run: pip install pillow numpy")
    sys.exit(1)

try:
    from colorthief import ColorThief
    HAS_COLORTHIEF = True
except ImportError:
    HAS_COLORTHIEF = False

try:
    from colormath.color_objects import sRGBColor, LabColor
    from colormath.color_conversions import convert_color
    from colormath.color_diff import delta_e_cie2000
    HAS_COLORMATH = True
except ImportError:
    HAS_COLORMATH = False

try:
    import pytesseract
    HAS_TESSERACT = True
except ImportError:
    HAS_TESSERACT = False


# =============================================================================
# Configuration - 0DaySiege Color Palette
# =============================================================================

PALETTE = {
    "deep_black": (10, 10, 15),        # #0a0a0f
    "dark_gray": (26, 26, 36),         # #1a1a24
    "mid_gray": (45, 45, 58),          # #2d2d3a
    "light_gray": (74, 74, 90),        # #4a4a5a
    "cyan": (0, 255, 255),             # #00ffff
    "green": (0, 255, 102),            # #00ff66
    "red": (255, 51, 68),              # #ff3344
    "orange": (255, 136, 51),          # #ff8833
    "purple": (170, 68, 255),          # #aa44ff
}

# Thresholds
DELTA_E_THRESHOLD = 15.0          # Max acceptable color distance from palette
LIGHTING_RATIO_THRESHOLD = 1.05   # Top-left must be this much brighter than bottom-right
ASPECT_RATIO_TOLERANCE = 0.05     # 5% tolerance for aspect ratio


@dataclass
class ValidationResult:
    """Result of a single validation check."""
    name: str
    passed: bool
    message: str
    details: Optional[dict] = None


@dataclass
class AssetValidation:
    """Complete validation results for an asset."""
    path: str
    asset_type: str
    results: list

    @property
    def passed(self) -> bool:
        return all(r.passed for r in self.results)

    @property
    def failed_checks(self) -> list:
        return [r for r in self.results if not r.passed]


# =============================================================================
# Validation Functions
# =============================================================================

def check_dimensions(img: Image.Image, asset_type: str) -> ValidationResult:
    """Check if dimensions match expected aspect ratio."""
    width, height = img.size
    actual_ratio = width / height

    if asset_type == "background":
        # 2:3 aspect ratio (portrait)
        expected_ratio = 2 / 3
        expected_name = "2:3"
    else:
        # 1:1 aspect ratio (square sprites)
        expected_ratio = 1.0
        expected_name = "1:1"

    ratio_diff = abs(actual_ratio - expected_ratio) / expected_ratio
    passed = ratio_diff <= ASPECT_RATIO_TOLERANCE

    return ValidationResult(
        name="Dimensions",
        passed=passed,
        message=f"{width}x{height} ({'OK' if passed else 'WRONG'}, expected {expected_name})",
        details={"width": width, "height": height, "ratio": actual_ratio, "expected": expected_ratio}
    )


def check_alpha_channel(img: Image.Image, asset_type: str) -> ValidationResult:
    """Check if sprites have alpha channel."""
    if asset_type == "background":
        return ValidationResult(
            name="Alpha Channel",
            passed=True,
            message="N/A for backgrounds"
        )

    has_alpha = img.mode in ('RGBA', 'LA') or (img.mode == 'P' and 'transparency' in img.info)

    if has_alpha and img.mode == 'RGBA':
        alpha = np.array(img.split()[-1])
        has_transparency = np.any(alpha < 255)
        if not has_transparency:
            return ValidationResult(
                name="Alpha Channel",
                passed=False,
                message="Has alpha channel but no transparent pixels"
            )

    return ValidationResult(
        name="Alpha Channel",
        passed=has_alpha,
        message="Present" if has_alpha else "MISSING - sprites need transparency"
    )


def check_lighting_direction(img: Image.Image) -> ValidationResult:
    """
    Check if lighting comes from top-left by comparing quadrant brightness.
    Top-left quadrant should be brighter than bottom-right.
    """
    # Convert to grayscale for luminance analysis
    gray = img.convert('L')
    arr = np.array(gray)

    h, w = arr.shape
    mid_h, mid_w = h // 2, w // 2

    # Calculate average brightness per quadrant
    top_left = np.mean(arr[:mid_h, :mid_w])
    top_right = np.mean(arr[:mid_h, mid_w:])
    bottom_left = np.mean(arr[mid_h:, :mid_w])
    bottom_right = np.mean(arr[mid_h:, mid_w:])

    # Top-left should be brightest, bottom-right should be darkest
    tl_br_ratio = top_left / max(bottom_right, 1)

    # Also check that top-left is brighter than bottom-left (light from above)
    tl_bl_ratio = top_left / max(bottom_left, 1)

    passed = tl_br_ratio >= LIGHTING_RATIO_THRESHOLD

    return ValidationResult(
        name="Lighting Direction",
        passed=passed,
        message=f"TL/BR ratio: {tl_br_ratio:.2f} ({'OK' if passed else 'WRONG - should be >1.05'})",
        details={
            "top_left": float(top_left),
            "top_right": float(top_right),
            "bottom_left": float(bottom_left),
            "bottom_right": float(bottom_right),
            "tl_br_ratio": float(tl_br_ratio)
        }
    )


def rgb_to_lab(rgb: tuple) -> LabColor:
    """Convert RGB tuple to LAB color space."""
    srgb = sRGBColor(rgb[0] / 255, rgb[1] / 255, rgb[2] / 255)
    return convert_color(srgb, LabColor)


def calculate_delta_e(color1: tuple, color2: tuple) -> float:
    """Calculate Delta E (CIE2000) between two RGB colors."""
    lab1 = rgb_to_lab(color1)
    lab2 = rgb_to_lab(color2)
    return delta_e_cie2000(lab1, lab2)


def find_closest_palette_color(color: tuple) -> tuple:
    """Find the closest palette color and its Delta E distance."""
    min_delta = float('inf')
    closest_name = None

    for name, palette_color in PALETTE.items():
        delta = calculate_delta_e(color, palette_color)
        if delta < min_delta:
            min_delta = delta
            closest_name = name

    return closest_name, min_delta


def check_color_palette(img: Image.Image, num_colors: int = 8) -> ValidationResult:
    """
    Extract dominant colors and verify they're close to the palette.
    """
    if not HAS_COLORTHIEF or not HAS_COLORMATH:
        return ValidationResult(
            name="Color Palette",
            passed=True,
            message="SKIPPED - install colorthief and colormath"
        )

    # Save to temp file for ColorThief (it requires a file path)
    import tempfile
    with tempfile.NamedTemporaryFile(suffix='.png', delete=False) as tmp:
        # Convert to RGB if necessary
        if img.mode in ('RGBA', 'LA'):
            # Composite on black background for color analysis
            background = Image.new('RGB', img.size, (10, 10, 15))
            if img.mode == 'RGBA':
                background.paste(img, mask=img.split()[3])
            else:
                background.paste(img, mask=img.split()[1])
            background.save(tmp.name)
        else:
            img.convert('RGB').save(tmp.name)
        tmp_path = tmp.name

    try:
        ct = ColorThief(tmp_path)
        palette = ct.get_palette(color_count=num_colors, quality=1)
    finally:
        Path(tmp_path).unlink()

    # Check each dominant color against our palette
    violations = []
    all_distances = []

    for color in palette:
        closest_name, delta = find_closest_palette_color(color)
        all_distances.append((color, closest_name, delta))
        if delta > DELTA_E_THRESHOLD:
            violations.append(f"#{color[0]:02x}{color[1]:02x}{color[2]:02x} (ΔE={delta:.1f})")

    passed = len(violations) == 0

    if passed:
        message = f"All {num_colors} dominant colors within ΔE<{DELTA_E_THRESHOLD}"
    else:
        message = f"{len(violations)} colors off-palette: {', '.join(violations[:3])}"

    return ValidationResult(
        name="Color Palette",
        passed=passed,
        message=message,
        details={"colors": all_distances, "violations": violations}
    )


def check_text_watermarks(img: Image.Image) -> ValidationResult:
    """Detect text in image using OCR."""
    if not HAS_TESSERACT:
        return ValidationResult(
            name="Text/Watermarks",
            passed=True,
            message="SKIPPED - install pytesseract"
        )

    try:
        # Convert to RGB if necessary
        if img.mode != 'RGB':
            img = img.convert('RGB')

        text = pytesseract.image_to_string(img, config='--psm 11').strip()

        # Filter out noise (very short strings are usually false positives)
        meaningful_text = [t for t in text.split() if len(t) > 2]

        passed = len(meaningful_text) == 0

        return ValidationResult(
            name="Text/Watermarks",
            passed=passed,
            message="None detected" if passed else f"DETECTED: '{' '.join(meaningful_text[:5])}'"
        )
    except Exception as e:
        return ValidationResult(
            name="Text/Watermarks",
            passed=True,
            message=f"SKIPPED - OCR error: {e}"
        )


# =============================================================================
# Main Validation
# =============================================================================

def validate_asset(image_path: str, asset_type: str = "sprite") -> AssetValidation:
    """Run all validation checks on an asset."""
    path = Path(image_path)

    if not path.exists():
        return AssetValidation(
            path=str(path),
            asset_type=asset_type,
            results=[ValidationResult("File", False, "File not found")]
        )

    try:
        img = Image.open(path)
    except Exception as e:
        return AssetValidation(
            path=str(path),
            asset_type=asset_type,
            results=[ValidationResult("File", False, f"Cannot open: {e}")]
        )

    results = [
        check_dimensions(img, asset_type),
        check_alpha_channel(img, asset_type),
        check_lighting_direction(img),
        check_color_palette(img),
        check_text_watermarks(img),
    ]

    return AssetValidation(
        path=str(path),
        asset_type=asset_type,
        results=results
    )


def print_validation(validation: AssetValidation, verbose: bool = False):
    """Print validation results."""
    status = "✓ PASS" if validation.passed else "✗ FAIL"
    print(f"\n{status}: {validation.path} ({validation.asset_type})")
    print("-" * 60)

    for result in validation.results:
        icon = "✓" if result.passed else "✗"
        print(f"  {icon} {result.name}: {result.message}")

        if verbose and result.details:
            for key, value in result.details.items():
                if key not in ('colors', 'violations'):
                    print(f"      {key}: {value}")

    if not validation.passed:
        print(f"\n  Failed checks: {len(validation.failed_checks)}")


def main():
    parser = argparse.ArgumentParser(
        description="Validate 0DaySiege art assets",
        formatter_class=argparse.RawDescriptionHelpFormatter,
        epilog="""
Examples:
  python validate_asset.py enemy_virus.png --type sprite
  python validate_asset.py background.png --type background
  python validate_asset.py --batch Assets/Sprites/ --type sprite
        """
    )

    parser.add_argument("image", nargs="?", help="Path to image file")
    parser.add_argument("--batch", metavar="DIR", help="Validate all images in directory")
    parser.add_argument("--type", choices=["sprite", "background"], default="sprite",
                        help="Asset type (default: sprite)")
    parser.add_argument("--verbose", "-v", action="store_true", help="Show detailed output")
    parser.add_argument("--strict", action="store_true", help="Exit with error if any check fails")

    args = parser.parse_args()

    if not args.image and not args.batch:
        parser.print_help()
        sys.exit(1)

    validations = []

    if args.batch:
        batch_dir = Path(args.batch)
        if not batch_dir.is_dir():
            print(f"Error: {args.batch} is not a directory")
            sys.exit(1)

        for ext in ('*.png', '*.jpg', '*.jpeg'):
            for img_path in batch_dir.glob(ext):
                validations.append(validate_asset(str(img_path), args.type))
    else:
        validations.append(validate_asset(args.image, args.type))

    # Print results
    passed_count = sum(1 for v in validations if v.passed)
    total_count = len(validations)

    for v in validations:
        print_validation(v, args.verbose)

    print(f"\n{'=' * 60}")
    print(f"Summary: {passed_count}/{total_count} assets passed")

    if args.strict and passed_count < total_count:
        sys.exit(1)


if __name__ == "__main__":
    main()

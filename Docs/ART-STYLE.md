# Art Style Guide - 0DaySiege

This document defines the visual approach and technical requirements for all game art. The goal is consistency across all assets so the game feels cohesive.

## Rendering Approach: Fake 2D

The game uses a **2D orthographic camera** but the art is drawn to simulate a 3D perspective. The engine does not tilt or rotate anything—all depth and perspective is **baked into the sprites** by the artist.

```
What the engine sees:        What the player perceives:

┌──────────────────┐         ┌──────────────────┐
│                  │         │ ╱──────────────╲ │
│   flat sprites   │    →    │╱   3D-looking   ╲│
│                  │         │    game world    │
└──────────────────┘         └──────────────────┘
```

This means:
- Every sprite must be drawn from the **same camera angle**
- Lighting and shadows are **painted into** each sprite
- Depth is communicated through **size scaling** (far = small, near = large)

---

## Camera Angle

All assets are drawn as if viewed from a camera positioned **in front of and above** the play area, looking down at approximately **45 degrees from horizontal**.

```
Side view of virtual camera setup:

        [Camera]
            ╲
             ╲  45°
              ╲
               ▼
    ════════════════════  ← Play surface
```

### Reference Angles

| Asset Type | Viewer Sees |
|------------|-------------|
| Play area | Top surface dominant, front edge visible |
| Towers | Equal front face + top visible, like looking down at a chess piece |
| Enemies | Front and top equally visible, moving toward camera |
| Firewall bar | Front face and top edge equally visible |
| UI frames | Facing camera with moderate downward tilt |

### Consistency Check

Place assets side by side. If one looks like it's from a different angle, it will break the illusion. All horizon lines and surface angles must match.

```
✓ Correct (same angle)        ✗ Wrong (mixed angles)

  ╱─╲    ╱─╲    ╱─╲            ╱─╲    │ │    ╱─╲
 │   │  │   │  │   │          │   │   │ │   ╱   ╲
 └───┘  └───┘  └───┘          └───┘   └─┘   └───┘
```

---

## Lighting

All assets share a **single global light source** positioned at **top-left**, approximately 45 degrees from vertical.

```
    ☀ Light source
     ╲
      ╲  45°
       ╲
        ▼
      ┌───┐
      │   │ ← Object
      └───┘
         ▓ ← Shadow falls bottom-right
```

### Lighting Rules

1. **Highlights** appear on top-left surfaces
2. **Shadows** fall toward bottom-right
3. **Cast shadows** extend to bottom-right of objects
4. **Ambient light** comes from below (subtle rim light on bottom edges for cyber glow)

### Material Response

| Material | Highlight | Shadow | Notes |
|----------|-----------|--------|-------|
| Metal | Sharp, bright | Dark with slight reflection | Chrome has environment reflection |
| Glass/Hologram | Soft glow | Minimal | Emits own light |
| Organic (enemies) | Soft gradient | Medium | Subsurface scattering feel |
| Energy/Laser | None (self-lit) | None | Additive blend, no shadows |

---

## Depth and Scale

Depth is faked by **scaling sprites** based on their Y position. The programmer handles the scaling—the artist provides size variants or a single asset at the "near" size.

### Scale Zones

```
Y Position        Scale Factor    Visual Size
──────────────────────────────────────────────
Top (spawn)       0.5x - 0.6x     Small (far away)
Middle            0.7x - 0.9x     Medium
Bottom (firewall) 1.0x - 1.2x     Large (close up)
```

### Artist Responsibility

**Option A:** Provide a single high-resolution sprite. Programmer scales it down for distance. Works for simple shapes.

**Option B:** Provide 2-3 size variants (small, medium, large) with adjusted detail levels. Better for complex enemies where scaling artifacts matter.

### Size Variant Guidelines

| Variant | Detail Level | Use Case |
|---------|--------------|----------|
| Small (far) | Simplified silhouette, fewer details | Top 1/3 of screen |
| Medium | Standard detail | Middle of screen |
| Large (near) | Full detail, subtle extras | Bottom 1/3 of screen |

---

## Color Palette

The game uses a **dark cyberpunk** palette with **high-contrast neon accents**.

### Base Tones

| Role | Hex | Usage |
|------|-----|-------|
| Deep Black | `#0a0a0f` | Backgrounds, shadows |
| Dark Gray | `#1a1a24` | Panels, frames |
| Mid Gray | `#2d2d3a` | Secondary surfaces |
| Light Gray | `#4a4a5a` | Highlights on dark metal |

### Accent Colors

| Role | Hex | RGB | Usage |
|------|-----|-----|-------|
| Cyan (Player) | `#00ffff` | 0, 255, 255 | Towers, friendly effects, UI highlights |
| Green (System) | `#00ff66` | 0, 255, 102 | Health, currency, positive feedback |
| Red (Threat) | `#ff3344` | 255, 51, 68 | Enemies, damage, warnings |
| Orange (Caution) | `#ff8833` | 255, 136, 51 | Damaged state, mid-priority alerts |
| Purple (Special) | `#aa44ff` | 170, 68, 255 | Rare items, special abilities |

### Color Rules

1. **Player-controlled elements** use cyan family
2. **Enemy elements** use red/orange family
3. **Neutral UI** uses gray + green accents
4. **Never mix** player and enemy colors on the same element
5. **Glow colors** are the accent color at 100% saturation
6. **Dim versions** reduce brightness, not saturation

### Glow and Emission

Glowing elements need **two layers**:
1. **Core**: Solid accent color, slightly overexposed
2. **Glow**: Same color, lower opacity, gaussian blur applied in-engine via bloom

```
Export as separate layers or provide:
  element_base.png   ← The solid object
  element_glow.png   ← White/bright mask for bloom areas
```

---

## Play Area

The play area should feel like **you are inside the network**, not looking at it from outside. The player is floating in digital cyberspace, defending the firewall from within.

### Visual Approach

- **No grid lines** — the environment is an open digital void, not a structured display
- **Depth through atmosphere** — use subtle particle effects, distant glowing nodes, or ambient data streams to suggest infinite digital space
- **Immersive darkness** — deep black background with soft cyber-tinted ambient lighting
- **No physical boundaries** — the space extends to screen edges without frames or bezels

### Background Elements (Non-Structural)

| Element | Purpose | Implementation |
|---------|---------|----------------|
| Distant nodes | Depth perception | Small glowing points at varying brightness |
| Data streams | Movement, life | Subtle flowing particles in background layers |
| Ambient haze | Atmosphere | Soft fog with cyan/blue tint at low opacity |
| Floating debris | Scale reference | Small geometric shapes drifting slowly |

### Scan Line Effect (Optional)

Very subtle horizontal scan lines can add texture:
- 1px lines every 3-4px
- Alternating 97% and 100% brightness
- Must be barely noticeable—if obvious, reduce or remove

---

## Screen Edge Handling

The play area extends **edge-to-edge** with no decorative frame or bezel. This maintains the immersive "inside the network" feeling.

### Aspect Ratio Range

The game must support aspect ratios from **4:3 (iPad)** to **20:9 (modern Android)**:

| Device Type | Aspect Ratio | Example |
|-------------|--------------|---------|
| iPad | 4:3 (1.33:1) | Wider, shorter |
| iPhone (older) | 16:9 (1.78:1) | Traditional |
| iPhone (modern) | 19.5:9 (2.17:1) | Taller |
| Android flagship | 20:9 (2.22:1) | Tallest |

### Background Dimensions

To handle all aspect ratios without letterboxing:

| Asset | Dimensions (@2x) | Dimensions (@3x) | Notes |
|-------|------------------|------------------|-------|
| Core gameplay | 1080 × 2400 | 1620 × 3600 | Safe area for all devices |
| Background | 1620 × 2400 | 2430 × 3600 | Extended for iPad width (4:3) |

```
┌─────────────────────────┐
│ ░░░░░░░░░░░░░░░░░░░░░░░ │  ← Extended background (1620w)
│ ░░┌─────────────────┐░░ │
│ ░░│                 │░░ │
│ ░░│  CORE GAMEPLAY  │░░ │  ← Safe area (1080w)
│ ░░│    (1080w)      │░░ │
│ ░░│                 │░░ │
│ ░░└─────────────────┘░░ │
│ ░░░░░░░░░░░░░░░░░░░░░░░ │
└─────────────────────────┘
```

- **Tall phones (20:9)**: Show full width, crop top/bottom of extended area
- **iPads (4:3)**: Show full height, reveal side extensions
- UI elements anchor to screen edges with safe margins

---

## Sprite Export Guidelines

### Target Devices (2024-2025)

| Platform | Resolution | Scale | Aspect Ratio |
|----------|------------|-------|--------------|
| iPhone 14/15/16 | 1179 × 2556 | @3x | ~19.5:9 |
| iPhone 16 Pro Max | 1320 × 2868 | @3x | ~19.5:9 |
| iPad Pro/Air 13" | 2048-2064 × 2732-2752 | @2x | ~4:3 |
| Android mid-range | 1080 × 2400 | 2.75-3x | 20:9 |
| Android flagship | 1440 × 3200 | 4x | 20:9 |

### Reference Resolution

- **Design resolution**: 1080 × 2400 (9:20 aspect ratio)
- This matches the most common mobile resolution globally
- Core gameplay fits within this area; backgrounds extend beyond for wider/shorter devices

### Export Scales

Export at **@2x and @3x only** — @1x is obsolete (no current devices use it):

| Scale | Target Devices | Sprite Multiplier |
|-------|----------------|-------------------|
| @2x | All iPads, budget Android | 1.0x (base) |
| @3x | All modern iPhones, mid/high-end Android | 1.5x |

**Workflow**: Create sprites at @3x resolution, export both @3x and downscaled @2x versions.

### Minimum Sizes

- Minimum detail size: **3px at @2x** (anything smaller won't read)
- Touch targets: **132 × 132 px at @3x** (44pt Apple minimum)

### Recommended Asset Sizes

Based on 1080 × 2400 reference resolution at @2x:

| Asset Type | Size (@2x) | Size (@3x) | Notes |
|------------|------------|------------|-------|
| Enemy (near/large) | 120-180px tall | 180-270px tall | Scales to ~60-90px when far |
| Enemy (far/small) | 60-90px tall | 90-135px tall | Pre-drawn variant preferred |
| Tower | 150-200px tall | 225-300px tall | Fixed position, no scaling |
| Projectile | 16-32px | 24-48px | Small, fast-moving |
| Firewall bar | 1080 × 80px | 1620 × 120px | Full width of safe area |
| UI button | 88-120px | 132-180px | Meets touch target minimum |
| Card (selection) | 280 × 400px | 420 × 600px | Prominent during selection |

### File Format

| Type | Format | Notes |
|------|--------|-------|
| Sprites | PNG-32 | Premultiplied alpha |
| Backgrounds | PNG-24 or JPG | No transparency needed |
| Glow masks | PNG-8 grayscale | White = glow, black = no glow |
| Animations | PNG sequence | `name_001.png`, `name_002.png`, etc. |

### Naming Convention

```
[category]_[name]_[variant]_[state].png

Examples:
  enemy_virus_small_idle.png
  enemy_virus_small_move_001.png
  tower_scanner_base.png
  tower_scanner_turret_firing.png
  ui_button_pause.png
  fx_laser_cyan.png
```

### Pivot Points

- **Enemies**: Bottom center (feet/base)
- **Towers**: Bottom center (base)
- **Projectiles**: Center
- **Effects**: Center or emission point

Document pivot points in a sprite sheet or metadata file if not using Unity's sprite editor.

### Unity Import Settings

| Setting | Value | Reason |
|---------|-------|--------|
| Texture Type | Sprite (2D and UI) | Proper sprite handling |
| Sprite Mode | Single or Multiple | Depends on atlas usage |
| Generate Mip Maps | **OFF** | Not needed for 2D sprites |
| Read/Write | **OFF** | Halves memory footprint |
| Compression | ASTC 5×5 or 6×6 | Best quality/size for mobile |
| Max Size | 2048 (sprites), 4096 (backgrounds) | Balance quality and memory |

### Texture Size Guidelines

Use **power-of-two** dimensions for optimal GPU performance:

| Asset Type | Recommended Size | Max Atlas Size |
|------------|------------------|----------------|
| Small sprites (projectiles, FX) | 64-256px | 1024×1024 |
| Medium sprites (enemies, towers) | 256-512px | 2048×2048 |
| Large sprites (bosses) | 512-1024px | 2048×2048 |
| Backgrounds | 2048×4096 | N/A (single texture) |

**Memory reference**: 2K atlas = ~2MB, 4K atlas = ~8MB in Unity

---

## AI Image Generation (GPT Image 1.5)

Assets are generated using **GPT Image 1.5**. Use consistent prompt structure to maintain visual coherence across all assets.

### Base Prompt Template

```
[ASSET DESCRIPTION], viewed from 45 degrees above, top-left lighting with shadows falling bottom-right, dark cyberpunk aesthetic, deep black and dark blue-gray color scheme (#0a0a0f, #1a1a24), [ACCENT COLOR] neon accents, military-grade data center style, stylized digital art, high contrast, matte metal finish, game asset, 2:3 aspect ratio
```

### Standard Negative Prompt

```
bright colors, daylight, outdoor, natural lighting, realistic photo, centered lighting, flat top-down view, cartoon, anime, watermark, text, blurry, low quality
```

### Asset-Specific Prompts

**Backgrounds:**
```
Inside a digital cyberspace void viewed from 45 degrees above, immersive network environment. Deep black infinite space (#0a0a0f) with distant floating data nodes glowing faintly cyan at varying depths. Subtle ambient digital haze with blue-gray tint (#1a1a24). Faint particle streams suggesting data flow in background. No grid lines, no floor texture—pure digital void atmosphere. Very subtle horizontal scan lines at low opacity. Top-left lighting casting soft ambient glow. Ethereal, otherworldly digital space. No characters or objects, empty environment only. 2:3 aspect ratio. Game asset, stylized digital art, cyberpunk aesthetic, high contrast, immersive depth.
```

**Enemies (replace [TYPE] and [COLOR]):**
```
[TYPE] malware creature viewed from 45 degrees above, cyberpunk digital monster, [COLOR] glowing accents (#ff3344), dark metallic body with circuit patterns, menacing stance facing camera, top-left lighting with shadows bottom-right, deep black background (#0a0a0f), stylized digital art, game sprite, transparent background PNG
```

**Towers (replace [TYPE]):**
```
[TYPE] defensive security tower viewed from 45 degrees above, cyberpunk turret design, cyan glowing accents (#00ffff), dark metallic construction, mounted on hexagonal base, top-left lighting with shadows bottom-right, deep black background (#0a0a0f), military hardware aesthetic, stylized digital art, game sprite, transparent background PNG
```

### Color Keywords by Asset Type

| Asset Type | Primary Accent | Hex |
|------------|----------------|-----|
| Player/Towers | Cyan | `#00ffff` |
| Enemies | Red | `#ff3344` |
| System/Health | Green | `#00ff66` |
| Warnings | Orange | `#ff8833` |
| Special/Rare | Purple | `#aa44ff` |

### Generation Settings

GPT Image 1.5 supports only **3:2**, **1:1**, and **2:3** aspect ratios.

| Parameter | Recommended Value |
|-----------|-------------------|
| Aspect Ratio | **2:3** (portrait) for backgrounds, **1:1** for sprites |
| Quality | High |
| Style | Vivid or Natural (test both) |

**Note**: 2:3 (~0.67:1) is less tall than our target 9:20 (~0.45:1). Generated backgrounds will need to be extended vertically in post-processing or tiled.

### Post-Processing Validation

**Automated validation script**: `Tools/validate_asset.py`

```bash
# Install dependencies
cd Tools && pip install -r requirements.txt

# Validate single asset
python validate_asset.py enemy_virus.png --type sprite
python validate_asset.py background.png --type background

# Batch validate
python validate_asset.py --batch Assets/Sprites/ --type sprite --strict
```

The script performs all **[Auto]** checks below. Items marked **[Manual]** require visual inspection.

#### Camera Angle Validation [Semi-Auto]

*Not yet automated — requires manual ellipse measurement or ML model*

The 45° camera angle creates measurable ellipse ratios. Any circular element (tower base, barrel top) viewed at 45° becomes an ellipse:

```
height/width ratio = cos(45°) ≈ 0.707
```

| Measured Ratio | Verdict | Issue |
|----------------|---------|-------|
| 0.65 – 0.75 | ✓ Pass | Correct ~45° angle |
| < 0.55 | ✗ Fail | Camera too high (top-down) |
| > 0.85 | ✗ Fail | Camera too low (side view) |

**Prompt tip**: Include "mounted on circular base" to get a measurable reference element.

#### Lighting Direction Validation [Auto] ✓

*Implemented in `validate_asset.py`*

With top-left lighting, the image should be brighter in the top-left quadrant:

```
Quadrant brightness test:
┌─────┬─────┐
│ TL  │ TR  │   Expected: TL > TR > BL > BR
│ 100 │ 85  │   (relative brightness)
├─────┼─────┤
│ BL  │ BR  │
│ 70  │ 55  │
└─────┴─────┘
```

| Check | Pass Condition |
|-------|----------------|
| TL vs BR | TL brightness > BR brightness by ≥15% |
| TL vs TR | TL brightness ≥ TR brightness |

#### Color Palette Validation [Auto] ✓

*Implemented in `validate_asset.py`*

Extract dominant colors and compare to target palette using Delta E (perceptual color distance):

| Target Color | Hex | Max Delta E |
|--------------|-----|-------------|
| Deep Black | `#0a0a0f` | 10 |
| Dark Gray | `#1a1a24` | 15 |
| Cyan (Player) | `#00ffff` | 20 |
| Red (Threat) | `#ff3344` | 20 |
| Green (System) | `#00ff66` | 20 |

**Pass**: All dominant colors within Delta E threshold of a palette color.

#### Other Checks

| Check | Type | Script | Method |
|-------|------|--------|--------|
| No text/watermarks | [Auto] | ✓ | OCR detection (Tesseract) — reject if text found |
| Correct dimensions | [Auto] | ✓ | Check aspect ratio matches 1:1 or 2:3 |
| Has transparency | [Auto] | ✓ | Verify alpha channel exists (sprites only) |
| Style consistency | [Semi-Auto] | — | Perceptual hash similarity ≥80% to reference assets |
| Visual quality | [Manual] | — | No artifacts, blur, or deformities |

---

## Animation Principles

### Frame Rates

| Animation Type | FPS | Notes |
|----------------|-----|-------|
| Idle loops | 8-12 | Subtle movement, low priority |
| Movement | 12-16 | Smooth but efficient |
| Attacks | 16-24 | Snappy, impactful |
| Effects | 24-30 | Smooth energy/particle feel |

### Depth Consistency

Animations must maintain the same camera angle throughout. A turning character should still appear viewed from 45° above.

### Glow Pulsing

Emissive elements should pulse subtly:
- Cycle: 1-2 seconds
- Range: 80% to 100% brightness
- Ease: Sine wave (smooth)

This can be handled by artists (baked into animation) or programmers (shader parameter).

---

## Common Mistakes to Avoid

| Mistake | Problem | Detection | Solution |
|---------|---------|-----------|----------|
| Wrong camera angle | Assets look like different games | Ellipse ratio ≠ 0.65–0.75 | Regenerate with angle reference |
| Centered lighting | Looks flat, no depth | TL quadrant not brightest | Always light from top-left |
| Pure black shadows | Looks like holes | Color check finds `#000000` | Use very dark blue/purple (`#0a0a14`) |
| Off-palette colors | Clashes with game style | Delta E > threshold | Adjust hue/saturation to match palette |
| Too much detail on small sprites | Becomes noise when scaled | [Manual] | Simplify far/small variants |
| Hard edges on glow | Looks like solid shapes | [Manual] | Glow must have soft falloff |
| Mixing pixel sizes | Inconsistent art style | [Manual] | Maintain consistent stroke/detail scale |
| Text/watermarks | Unprofessional, unusable | OCR detects text | Regenerate or inpaint to remove |

---

## Reference Images

When creating new assets, cross-reference existing approved assets:

| Check | Deterministic Test |
|-------|-------------------|
| Camera angle matches | Ellipse ratio 0.65–0.75 |
| Lighting direction matches | TL quadrant brightness > BR |
| Colors from palette | Delta E < threshold |
| Scale consistent | Compare pixel dimensions |
| Glow treatment matches | [Manual] soft falloff check |

If something looks "off" when placed in-game, one of these five things is usually wrong.

---

## Checklist Before Delivery

### Automated Checks (Script-Verifiable)

- [ ] **Camera angle**: Circular elements have ellipse ratio 0.65–0.75
- [ ] **Lighting**: Top-left quadrant is brightest
- [ ] **Colors**: Dominant colors within Delta E of palette
- [ ] **Dimensions**: Matches target (@2x or @3x)
- [ ] **Aspect ratio**: 1:1 (sprites) or 2:3 (backgrounds)
- [ ] **Transparency**: Alpha channel present (sprites only)
- [ ] **No text**: OCR returns no detected text
- [ ] **File naming**: Follows `[category]_[name]_[variant]_[state].png`

### Manual Checks

- [ ] Glow areas have soft edges (no hard cutoffs)
- [ ] Shadows fall bottom-right
- [ ] Pivot point documented
- [ ] Asset looks correct when placed next to existing assets
- [ ] No visual artifacts, blur, or deformities

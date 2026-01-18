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

All assets are drawn as if viewed from a camera positioned **in front of and above** the play area, looking down at approximately **30 degrees from horizontal**.

```
Side view of virtual camera setup:

        [Camera]
            ╲  30°
             ╲
              ╲
               ▼
    ════════════════════  ← Play surface
```

### Reference Angles

| Asset Type | Viewer Sees |
|------------|-------------|
| Play area / grid | Top surface visible, slight front edge |
| Towers | Front face + top, like looking at a chess piece |
| Enemies | Front/top, moving toward camera |
| Firewall bar | Front face dominant, top edge visible |
| UI frames | Facing camera with slight downward tilt |

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

## Grid and Play Area

The play area resembles a **digital monitor or scanner display**.

### Grid Specifications

- Grid lines **converge toward top** (vanishing point at top-center)
- Line color: Cyan (`#00ffff`) at 15-25% opacity
- Line weight: 1-2px at base resolution
- Cell shape: Trapezoids (wider at bottom, narrower at top)

```
Flat grid (wrong):         Perspective grid (correct):

│ │ │ │ │ │ │ │            ╲ │ │ │ │ │ ╱
─────────────────            ─────────────
│ │ │ │ │ │ │ │              ╲│ │ │ │ │╱
─────────────────              ───────────
│ │ │ │ │ │ │ │                ╲│ │ │ │╱
─────────────────                ─────────
```

### Scan Line Effect

Optional subtle horizontal scan lines:
- 1px lines every 2-4px
- Alternating 95% and 100% brightness
- Very subtle—should be barely noticeable

---

## Frame and Bezel

The game area is framed by a **metallic control panel bezel** that extends to screen edges.

### Frame Characteristics

- Material: Brushed dark metal with subtle wear
- Perspective: Matches 30° camera angle
- Contains: Mounting brackets, screws, panel seams
- Should feel like a **physical arcade cabinet** or **military hardware**

### Safe Zone

```
┌─────────────────────────────┐
│ ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓ │  ← Frame/bezel (extends to edges)
│ ▓ ┌─────────────────────┐ ▓ │
│ ▓ │                     │ ▓ │
│ ▓ │    PLAY AREA        │ ▓ │  ← Gameplay happens here
│ ▓ │    (safe zone)      │ ▓ │
│ ▓ │                     │ ▓ │
│ ▓ └─────────────────────┘ ▓ │
│ ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓ │
└─────────────────────────────┘
```

The frame provides visual polish AND solves aspect ratio issues—different screen shapes just show more or less of the decorative frame.

---

## Sprite Export Guidelines

### Resolution

- Base resolution: **1080p portrait** (1080 x 1920)
- Sprites: Create at **2x** intended display size, export at 1x and 2x
- Minimum detail size: **2px at 1x** (anything smaller won't read)

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
  ui_frame_monitor.png
  fx_laser_cyan.png
```

### Pivot Points

- **Enemies**: Bottom center (feet/base)
- **Towers**: Bottom center (base)
- **Projectiles**: Center
- **Effects**: Center or emission point

Document pivot points in a sprite sheet or metadata file if not using Unity's sprite editor.

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

Animations must maintain the same camera angle throughout. A turning character should still appear viewed from 30° above.

### Glow Pulsing

Emissive elements should pulse subtly:
- Cycle: 1-2 seconds
- Range: 80% to 100% brightness
- Ease: Sine wave (smooth)

This can be handled by artists (baked into animation) or programmers (shader parameter).

---

## Common Mistakes to Avoid

| Mistake | Problem | Solution |
|---------|---------|----------|
| Inconsistent angles | Assets look like they're from different games | Use reference grid overlay while drawing |
| Centered lighting | Looks flat, no depth | Always light from top-left |
| Pure black shadows | Looks like holes | Use very dark blue/purple (`#0a0a14`) |
| Over-saturated colors | Eye strain, looks cheap | Keep backgrounds desaturated, accents saturated |
| Too much detail on small sprites | Becomes noise when scaled | Simplify far/small variants |
| Hard edges on glow | Looks like solid shapes | Glow must have soft falloff |
| Mixing pixel sizes | Inconsistent art style | Maintain consistent stroke/detail scale |

---

## Reference Images

When creating new assets, cross-reference existing approved assets:

1. Check the **camera angle** matches
2. Check the **lighting direction** matches
3. Check **colors** are from the palette
4. Check **scale** feels consistent with neighbors
5. Check **glow treatment** matches other emissive elements

If something looks "off" when placed in-game, one of these five things is usually wrong.

---

## Checklist Before Delivery

- [ ] Camera angle matches reference (30° top-down)
- [ ] Light source is top-left
- [ ] Shadows fall bottom-right
- [ ] Colors are from approved palette
- [ ] Glow areas have soft edges
- [ ] Exported at correct resolution (1x and 2x)
- [ ] File naming follows convention
- [ ] Pivot point documented
- [ ] Transparency is premultiplied
- [ ] Asset looks correct when placed next to existing assets

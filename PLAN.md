# 0DaySiege - Implementation Plan

## Overview

This document outlines the implementation plan for 0DaySiege, a tower defense game where players defend a Firewall from waves of malware. The plan is organized into epics representing major feature areas.

**Engine:** Unity 6 (6000.2.7f2)

**Target Platforms:** iOS, Android, macOS, Windows

---

## Epic 1: Core Game Framework

**Goal:** Establish the foundational game loop and scene structure.

**Scope:**
- Main game scene setup with proper screen layout
- Game state management (Menu, Playing, Paused, Card Selection, Game Over)
- Basic run flow (start, play, end)
- Pause/Resume/Restart/Quit controls
- Wave transition system (auto-advance with brief pause)

**Dependencies:** None (foundation epic)

---

## Epic 2: Firewall System

**Goal:** Implement the defensive wall that players protect.

**Scope:**
- Firewall entity with HP (base 2000)
- Damage handling and HP display
- Visual damage feedback (glitch effects, flickering, corruption)
- Game over trigger when HP reaches zero
- Low health warning (red vignette at ≤25%)

**Dependencies:** Epic 1

---

## Epic 3: Enemy System

**Goal:** Create enemies that travel toward and attack the Firewall.

**Scope:**
- Base enemy entity with movement, HP, and attack behavior
- Three enemy types: Basic (Virus), Fast (Worm), Boss (Ransomware)
- Spawn system with configurable X position and timing
- Wall attack behavior (stop, wait for cooldown, attack repeatedly)
- Health bars above enemies
- HP scaling formula: `HP = base × difficulty × (1 + (wave - 1) × 0.10)`
- Speed values: Basic 0.1, Fast 0.16, Boss 0.06 units/s

**Dependencies:** Epic 2

---

## Epic 4: Tower System - Core

**Goal:** Implement the tower placement and basic tower functionality.

**Scope:**
- 5 fixed tower slots (middle slot 3 for Basic Tower, outer slots 1,2,4,5 for others)
- Tower entity base class with damage, fire rate, range, targeting
- Targeting priority system (Attacking Wall → Boss → Closest → Highest HP → First Spawned)
- Projectile system with travel and hit detection
- Base Tower (Antivirus Turret) - single-target, 50 damage, 1.0/s, 0.9 range
- Tower tooltips showing stats
- Range indicator on hover

**Dependencies:** Epic 3

---

## Epic 5: Tower System - Basic Towers

**Goal:** Implement all four Basic Tower types.

**Scope:**
- AOE Tower (Logic Bomb) - 40 damage, splash radius 0.12, 50% edge falloff
- Burst Tower (Zero-Day Striker) - 150 damage, 0.33/s fire rate
- Piercing Tower (Traceroute Cannon) - hitscan, pierces all enemies in line
- Critical hit system (5% base chance, 1.5x multiplier)
- Distinct projectile types and behaviors per tower

**Dependencies:** Epic 4

---

## Epic 6: Tower System - Advanced Towers

**Goal:** Implement Advanced Tower type(s).

**Scope:**
- Brute Force Node - 3-shot burst, 18 damage per shot
- Credential Stuffing mechanic (+20%/+40% consecutive hit bonus)
- Burst timing (0.1s between shots, 1.2s reload)

**Dependencies:** Epic 4

---

## Epic 7: Score & Card System

**Goal:** Implement the score-based progression and card selection.

**Scope:**
- Score tracking (Basic: 10, Fast: 15, Boss: 100)
- Score threshold system (8 thresholds: 50, 120, 220, 350, 520, 730, 1000, 1350)
- Card selection UI (3 cards presented)
- Card types: Place Tower, Tower Upgrade, Wall Repair (30% heal, 15% chance)
- Tower upgrade cards: Damage+ (T1: +25%, T2: +50%), Fire Rate+ (T1: +25%, T2: +50%)
- Card pool management and exhaustion handling

**Dependencies:** Epic 4, Epic 5, Epic 6

---

## Epic 8: Stage & Wave System

**Goal:** Implement the stage loading and wave progression.

**Scope:**
- Stage file format (JSON) with waves and enemy definitions
- Wave progression (20 waves per stage)
- Boss wave handling (isBoss flag)
- Wave indicator UI
- Difficulty selection (Normal: 1.0x HP/Speed, Hard: 1.5x HP, 1.2x Speed)
- Chapter 1 stages (1-1 through 1-5)

**Dependencies:** Epic 3, Epic 7

---

## Epic 9: Meta-Progression - Currency & Unlocks

**Goal:** Implement persistent progression between runs.

**Scope:**
- Data Shards currency
- Currency sources (wave: 5, Basic kill: 1, Fast kill: 2, Boss kill: 15, clear: 50, perfect: +25)
- Difficulty reward multiplier (Hard: 1.5x)
- Tower unlock system with costs
- Persistent data storage

**Dependencies:** Epic 8

---

## Epic 10: Meta-Progression - Tower Mastery

**Goal:** Implement the tower mastery upgrade system.

**Scope:**
- 5 mastery levels per tower (+10/20/30/40/50% damage)
- Mastery costs (escalating: 75, 150, 300, 600, 1200 for Base Tower)
- Level 5 special abilities:
  - Base Tower: Overclocked Processor (20% crit, 2x multiplier)
  - AOE Tower: Firewall Cascade (burning ground, 2s, 25% damage/tick)
  - Burst Tower: Precision Strike (+50% to enemies above 50% HP)
  - Piercing Tower: Network Breach (+15% damage taken, 2 stacks max)
  - Brute Force Node: Dictionary Attack (4th shot, +25% consecutive bonus)

**Dependencies:** Epic 9

---

## Epic 11: Battle Profiles

**Goal:** Implement pre-run loadout configuration.

**Scope:**
- Profile creation and management (6 profiles max)
- Basic Tower selection per profile
- Profile selection before run start
- Profile locked during run

**Dependencies:** Epic 10

---

## Epic 12: Gear System - Core

**Goal:** Implement the gear equipment system.

**Scope:**
- 5 gear slots (Firmware, Protocol, Targeting, Network, Utility)
- Gear rarity system (Common→Legendary)
- Socket system (0-4 sockets based on rarity)
- Gear effects application during runs
- Starting unlocks (all Common gear)

**Dependencies:** Epic 11

---

## Epic 13: Gear System - Chips

**Goal:** Implement the chip socket system.

**Scope:**
- Chip types (Offensive, Defensive, Economy, Utility)
- Chip stacking with caps
- Chip inventory management
- Chip acquisition sources
- Slotting/unslotting between runs

**Dependencies:** Epic 12

---

## Epic 14: Gear System - Full Implementation

**Goal:** Implement all gear pieces and legendary mechanics.

**Scope:**
- All gear pieces (45 total across 5 slots)
- Legendary gear with drawbacks
- Legendary acquisition (achievements, Hard mode clears)
- Gear unlock costs by rarity
- Triggered effects (failsafe heal, immortal process, etc.)

**Dependencies:** Epic 13

---

## Epic 15: Stage Progression & Unlocks

**Goal:** Implement stage unlock flow and rewards.

**Scope:**
- Stage unlock progression (complete to unlock next)
- Hard mode unlock (per-stage after Normal clear)
- Run failure rewards (partial based on progress)
- Failure/Victory screens with retry flow
- Daily/Retention bonuses (login, first win, challenges, streaks)

**Dependencies:** Epic 8, Epic 9

---

## Epic 16: Status Effects System

**Goal:** Implement status effects and their visuals.

**Scope:**
- Burn effect (ground DOT, 2s duration, 0.5s ticks)
- Breach effect (damage amp, 2 stacks max, 5s duration)
- Slow effect (from gear/future towers)
- Status visual indicators (🔥 orange, ⚡ green, ❄ blue)
- Stacking and refresh rules

**Dependencies:** Epic 10

---

## Epic 17: Visual Feedback & Polish

**Goal:** Implement visual feedback systems for clarity and game feel.

**Scope:**
- Damage numbers (floating, fade out)
- Critical hit styling (larger, red, "!" suffix)
- Attack indicators (projectile trails/beams)
- Screen shake (boss spawn, wall damage, explosions)
- Particle systems (tower fire, enemy death, impacts)
- Card threshold progress bar

**Dependencies:** Epic 7

---

## Epic 18: UI/UX Systems

**Goal:** Implement all menu and HUD interfaces.

**Scope:**
- Main menu
- Stage selection screen
- Battle Profile editor
- Chip inventory UI
- In-run HUD (score, wave, wall HP, gear icons)
- Pause menu
- Card selection overlay
- Victory/Defeat screens

**Dependencies:** Epic 15

---

## Epic 19: Audio System

**Goal:** Implement sound effects and music.

**Scope:**
- Tower firing sounds
- Enemy spawn/death sounds
- Wall damage/heal sounds
- UI interaction sounds
- Background music per stage/menu
- Alert tones for low health, card available

**Dependencies:** Epic 17

---

## Epic 20: Balancing & Content

**Goal:** Create and tune all stage content.

**Scope:**
- Stage files for 1-1 through 1-5 (100 waves total)
- Wave composition and difficulty curves
- Playtesting and balance adjustments
- Tutorial/onboarding (if needed)

**Dependencies:** All previous epics

---

## Implementation Order Recommendation

```
Phase 1 - Core Loop (Epics 1-4)
├── Playable prototype with one tower, one enemy type, basic wave

Phase 2 - Tower Variety (Epics 5-6)
├── All tower types functional

Phase 3 - Progression During Run (Epics 7-8)
├── Cards, upgrades, full stage system

Phase 4 - Meta-Progression (Epics 9-11)
├── Persistent unlocks, mastery, profiles

Phase 5 - Gear Depth (Epics 12-14)
├── Full gear and chip systems

Phase 6 - Polish & Content (Epics 15-20)
├── Stage unlocks, VFX, UI, audio, balancing
```

---

## Notes

- **MVP Scope:** Epics 1-10, 15, 17-18 constitute the minimum viable product
- **Post-MVP:** Epic 14 (full gear), Epic 16 (status effects), Epic 19 (audio) can be deferred
- **Future Content:** Special Towers (Section 5.3), Damage Types (Appendix D), Random Mode (Section 8.8) are explicitly post-MVP

# Balance Guidelines

> This content was previously Appendix B of the [Game Design Document](GDD.md).

## Tower Stats Reference

This is the authoritative source for all tower statistics. For tower mechanics and special abilities, see Section 5 of the GDD.

Towers have distinct stat profiles while maintaining similar overall DPS. The Basic Tower (Antivirus Turret) and most Advanced Towers have full range (1.0), while some Advanced Towers have moderate range.

### Basic Tower

| Tower | Damage | Fire Rate | Range | DPS | Unlock Cost |
|-------|--------|-----------|-------|-----|-------------|
| Antivirus Turret | 50 | 1.0/s | 1.0 | 50 | Always unlocked |

### Advanced Towers

| Tower | Damage | Fire Rate | Range | DPS | Unlock Cost |
|-------|--------|-----------|-------|-----|-------------|
| Logic Bomb | 40 | 1.2/s | 0.9 | 48 | 200 shards |
| Zero-Day Striker | 150 | 0.33/s | 0.9 | 50 | 250 shards |
| Traceroute Cannon | 50 | 1.0/s | 0.9 | 50 | 300 shards |
| Brute Force Node | 18×3 | 0.83 bursts/s | 0.85 | 45 | 400 shards |

**Brute Force Node Details:**

| Property | Value |
|----------|-------|
| Burst Count | 3 shots (4 with Mastery 5) |
| Burst Interval | 0.1s (fires in ~0.3s) |
| Reload Time | 1.2s between bursts |
| Projectile Speed | 2.0 units/s |
| Credential Stuffing | +20%/+40% damage on consecutive hits (64 total vs 54 base) |
| Dictionary Attack (Mastery 5) | 4th shot, +25%/+50%/+75% per hit (100 total vs 72 base) |

## Tower DPS with Upgrades

| Upgrade State | DPS Multiplier | Example (Antivirus Turret) |
|---------------|----------------|----------------------|
| No upgrades | 1.0x | 50 DPS |
| Tier 1 Damage OR Fire Rate | 1.25x | 62.5 DPS |
| Tier 2 Damage OR Fire Rate | 1.5x | 75 DPS |
| Tier 2 Damage AND Fire Rate | 2.25x | 112.5 DPS |

## Enemy HP Scaling

Formula: `HP = base × difficulty × (1 + (wave - 1) × 0.10)`

**Basic Enemy (base 100 HP):**

| Wave | Normal (1.0x) | Hard (1.5x) |
|------|---------------|-------------|
| 1 | 100 | 150 |
| 10 | 190 | 285 |
| 20 | 290 | 435 |

**Fast Enemy (base 60 HP):**

| Wave | Normal (1.0x) | Hard (1.5x) |
|------|---------------|-------------|
| 1 | 60 | 90 |
| 10 | 114 | 171 |
| 20 | 174 | 261 |

**Boss Enemy (base 500 HP):**

| Wave | Normal (1.0x) | Hard (1.5x) |
|------|---------------|-------------|
| 20 | 1450 | 2175 |

*Scaling increased from 8% to 10% per wave to improve late-game challenge.*

## DPS vs HP Balance Check

Time to kill with 5 fully upgraded towers (~112.5 DPS each = 562.5 DPS total):
- Wave 20 Basic (Hard): 435 HP / 562.5 DPS = **0.77 seconds**
- Wave 20 Fast (Hard): 261 HP / 562.5 DPS = **0.46 seconds**
- Wave 20 Boss (Hard): 2175 HP / 562.5 DPS = **3.9 seconds**

## Wave Difficulty Curve

| Wave Range | Enemy HP | Enemy Count | Composition |
|------------|----------|-------------|-------------|
| 1-5 | Low | Few | Basic only |
| 6-10 | Medium | Moderate | Basic + Fast mixed |
| 11-15 | High | Many | Basic + Fast mixed |
| 16-19 | Very High | Many | Basic + Fast mixed |
| 20 | Boss | 1 + support | Boss + Basic + Fast |

## Score Balance

### Score Caps

| Cap Type | Maximum Value | Rationale |
|----------|---------------|-----------|
| Per-enemy score | 500 | Prevents overflow exploits |
| Kill streak bonus | +50 per streak | Limits streak farming |
| Multi-kill bonus | +100 per instance | Caps AOE exploitation |

### Score Calculation Examples

**Stage 1-5 Hard, Perfect Clear:**

| Component | Calculation | Score |
|-----------|-------------|-------|
| Basic kills (180) | 180 × 10 | 1,800 |
| Fast kills (40) | 40 × 15 | 600 |
| Boss kill (1) | 1 × 200 | 200 |
| Streak bonuses (est.) | ~100 | 100 |
| **Subtotal** | | 2,700 |
| Difficulty bonus | × 1.5 | 4,050 |
| No-Leak bonus | × 1.25 | 5,062 |
| Full Clear bonus | + 500 | **5,562** |

**Stage 1-1 Normal, Wall Damaged:**

| Component | Calculation | Score |
|-----------|-------------|-------|
| Basic kills (120) | 120 × 10 | 1,200 |
| Fast kills (20) | 20 × 15 | 300 |
| Boss kill (1) | 1 × 200 | 200 |
| **Subtotal** | | 1,700 |
| Difficulty bonus | × 1.0 | 1,700 |
| Full Clear bonus | + 500 | **2,200** |

*Note: No No-Leak bonus due to wall damage.*

# Economy Balancing Reference

> This content was previously Appendix H of the [Game Design Document](GDD.md).

This document consolidates all economy tuning parameters into a single reference. Use this for balance adjustments and to understand expected player progression rates.

## H.1 Primary Tuning Levers

| Lever | Value | Affects | Source |
|-------|-------|---------|--------|
| `SHARD_PER_BASIC_KILL` | 1 | Bulk income rate | §7.1 |
| `SHARD_PER_FAST_KILL` | 2 | Fast enemy incentive | §7.1 |
| `SHARD_PER_BOSS_KILL` | 15 | Boss value | §7.1 |
| `SHARD_WAVE_BONUS` | 5 | Wave survival reward | §7.1 |
| `SHARD_CLEAR_BONUS_NORMAL` | 50 | Victory incentive (Normal) | §7.1 |
| `SHARD_CLEAR_BONUS_HARD` | 75 | Victory incentive (Hard) | §7.1 |
| `SHARD_PERFECT_BONUS` | 25 | Perfect play reward | §7.1 |
| `DIFFICULTY_MULTIPLIER_HARD` | 1.5× | Hard mode shard multiplier | §7.1, §8.5 |
| `KEY_PER_BOSS_KILL` | 1 | Key acquisition rate | §7.1 |
| `KEY_PERFECT_BONUS` | 1 | Perfect clear key bonus | §7.1 |
| `KEY_HARD_BONUS` | 1 | Hard clear key bonus | §7.1 |
| `KEY_DAILY_LOGIN` | 1 | Daily retention | §7.1 |
| `CHIP_DROP_CHANCE_BOSS` | 15% | Chip acquisition rate | §9.4 |
| `REROLL_COST_KEYS` | 1 | Reroll accessibility | §6.5 |
| `KEY_SHOP_PRICE_SHARDS` | 200 | Shard→Key conversion rate | §10.4 |
| `CHIP_RANDOM_PRICE` | 50 | Chip accessibility | §10.4 |
| `CHIP_TARGETED_PRICE` | 150 | Chip targeting premium (3×) | §10.4 |
| `STREAK_BONUS_2DAY` | +10% | Short retention incentive | §7.1 |
| `STREAK_BONUS_5DAY` | +20% | Medium retention incentive | §7.1 |
| `STREAK_BONUS_7DAY` | +30% | Weekly retention incentive | §7.1 |
| `STREAK_BONUS_14DAY` | +50% | Long-term retention cap | §7.1 |

## H.2 Expected Run Earnings

**Baseline Assumptions (Stage 1-3, Mid-Chapter):**
- 180 Basic enemies killed
- 30 Fast enemies killed
- 1 Boss killed
- 20 waves completed
- Victory achieved

| Scenario | Calculation | Total Shards |
|----------|-------------|--------------|
| **Normal, Wall Damaged** | (180×1)+(30×2)+(1×15)+(20×5)+50 | **405** |
| **Normal, Perfect** | 405 + 25 | **430** |
| **Hard, Wall Damaged** | 405 × 1.5 | **607** |
| **Hard, Perfect** | (405+25) × 1.5 | **645** |

**With Streak Multipliers:**

| Streak | Normal Perfect | Hard Perfect |
|--------|----------------|--------------|
| None | 430 | 645 |
| 2-day (+10%) | 473 | 709 |
| 5-day (+20%) | 516 | 774 |
| 7-day (+30%) | 559 | 838 |
| 14-day (+50%) | 645 | 967 |

**Key Earnings Per Run:**
- Normal clear: 1 key (boss)
- Normal perfect: 2 keys (boss + perfect)
- Hard clear: 2 keys (boss + hard)
- Hard perfect: 3 keys (boss + hard + perfect)

## H.3 Stack Caps & Synergy Limits

All bonuses have hard caps to prevent degenerate builds.

### Hard Caps (Cannot Exceed)

| Stat | Cap | Max Chips | Rationale |
|------|-----|-----------|-----------|
| Chip Damage Bonus | +20% | 5 DMG | Prevents trivializing HP scaling |
| Chip Fire Rate Bonus | +20% | 5 SPD | Maintains tower identity |
| Chip Crit Chance | +15% | 5 CRIT | Keeps crits exciting, not expected |
| Chip Crit Multiplier | +0.6× | 4 CRIT-X | Caps burst potential |
| Chip Wall HP | +30% | 6 HP | Prevents immortal builds |
| Chip Damage Reduction | -15% | 5 ARMOR | Maintains threat tension |
| Chip Shard Gain | +30% | 6 SHARD | Prevents runaway progression |
| Chip Score Gain | +30% | 6 SCORE | Maintains leaderboard integrity |
| Chip Card Threshold | -15% | 5 THRESH | Maintains pacing |
| Chip Range | +15% | 5 RANGE | Preserves positioning decisions |

### Soft Caps (Hard Limits, Not Diminishing)

| System | Cap | Behavior At Cap |
|--------|-----|-----------------|
| Kill Streak Bonus | +50/streak | No additional score beyond cap |
| Multi-Kill Bonus | +100/instance | No additional score beyond cap |
| Decrypt Keys Held | 99 | Cannot acquire more until spent |
| Per-Enemy Score | 500 | Overflow protection |

### Score Decay Thresholds

| Run Duration | Score Multiplier |
|--------------|------------------|
| ≤ 2× expected | 1.0× (no penalty) |
| 2.5× expected | 0.95× |
| 3× expected | 0.9× |
| 5× expected | 0.7× |
| 7×+ expected | 0.5× (minimum) |

*Expected time ≈ 5 minutes (20 waves × 15s average)*

## H.4 Synergy Calculations

**Stacking Rules:**
- Chip bonuses: Additive within same stat type
- Gear + Chips: Additive (gear effect + sum of chip effects)
- Mastery: Multiplicative with other bonuses
- Card Upgrades: Multiplicative with base damage

**Maximum Theoretical Damage:**

| Source | Multiplier |
|--------|------------|
| Base Damage | 1.0× |
| Mastery Level 5 | ×1.5 |
| Chip Damage (+20%) | ×1.2 |
| Card Tier 2 Damage (+50%) | ×1.5 |
| Gear (apt_payload.dll, +20%) | ×1.2 |
| **Combined** | **3.24×** |

*Example: Base Tower (50 dmg) → 50 × 3.24 = 162 damage per shot*

**Maximum Theoretical Shard Gain:**

| Source | Multiplier |
|--------|------------|
| Base Rate | 1.0× |
| Hard Mode | ×1.5 |
| 14-Day Streak | ×1.5 |
| Chip Shard (+30%) | ×1.3 |
| Gear (quantum_compute.bat, +25%) | ×1.25 |
| **Combined** | **3.66×** |

*Example: Hard perfect run base 430 → 430 × 2.44 (excluding streak) = 1,049 shards*

## H.5 Progression Milestones

| Milestone | Shards Needed | Target Runs | Target Hours |
|-----------|---------------|-------------|--------------|
| First tower unlock | 200 | 1-2 | 0.5-1 |
| All Basic towers | 750 | 3-5 | 1.5-2.5 |
| First Mastery 5 | 2,325 | 8-12 | 4-6 |
| One tower fully mastered | 2,325 | 8-12 | 4-6 |
| All Uncommon gear | 1,350 | 5-8 | 2.5-4 |
| All Rare gear | 3,600 | 12-18 | 6-9 |
| All Epic gear | 7,200 | 20-30 | 10-15 |
| All towers mastered | ~17,000 | 50-70 | 25-35 |

*Assumes average 400-500 shards per run, 30 minutes per run including menus.*

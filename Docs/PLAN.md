# 0Day Siege - Implementation Plan

This document organizes the remaining implementation work into 20 epics, ordered by dependency and priority.

---

## Implementation Status Legend

- ✅ Complete
- 🔄 In Progress / Partial
- ⬜ Not Started

---

## Epic Overview

| Epic | Name | Status | Priority |
|------|------|--------|----------|
| 1 | Core Framework | ✅ | Critical |
| 2 | Enemy System | ✅ | Critical |
| 3 | Tower System | ✅ | Critical |
| 4 | Firewall System | ✅ | Critical |
| 5 | Wave Spawning | ✅ | Critical |
| 6 | Score System | ✅ | Critical |
| 7 | Basic UI | ✅ | Critical |
| 8 | Card System | ✅ | Critical |
| 9 | Tower Upgrades | ✅ | High |
| 10 | Victory & Defeat | ✅ | High |
| 11 | Stage Data | ✅ | High |
| 12 | Difficulty System | ⬜ | High |
| 13 | Battle Profiles | ⬜ | Medium |
| 14 | Currency System | ⬜ | Medium |
| 15 | Tower Unlocks | ⬜ | Medium |
| 16 | Mastery System | ⬜ | Medium |
| 17 | Gear System | ⬜ | Low |
| 18 | Chip System | ⬜ | Low |
| 19 | Shop System | ⬜ | Low |
| 20 | Polish & Audio | ⬜ | Low |

---

## Epic 1: Core Framework ✅

Foundation systems for game state management and runtime initialization.

### Completed
- [x] GameBootstrap with `[RuntimeInitializeOnLoadMethod]`
- [x] GameManager singleton with state machine (Menu, Playing, Paused, CardSelection, GameOver)
- [x] WaveManager singleton with wave states (Idle, InProgress, Transitioning)
- [x] GameLayout with screen boundaries and coordinate conversion
- [x] ScreenController for resolution and orientation handling
- [x] EventSystem for UI input (new Input System)
- [x] DebugControls for development testing

### References
- GDD Section 3.5: Run Flow
- GDD Section 3.7: Run Controls

---

## Epic 2: Enemy System ✅

Enemy entities with movement, health, and wall attack behavior.

### Completed
- [x] Enemy entity with types (Virus, Worm, Ransomware)
- [x] EnemyManager singleton for spawning and tracking
- [x] Enemy movement (spawn to firewall)
- [x] Enemy health and damage handling
- [x] Wall attack behavior with cooldowns
- [x] Enemy health bars
- [x] HP scaling formula: `base × difficulty × (1 + (wave - 1) × 0.10)`

### References
- GDD Section 4: Enemies
- [Balance Guidelines](BALANCE_GUIDELINES.md): Enemy HP Scaling

---

## Epic 3: Tower System ✅

Tower entities with targeting, projectiles, and damage dealing.

### Completed
- [x] Tower entity with types (BaseTower, AOETower, BurstTower, PiercingTower, BruteForceNode)
- [x] TowerManager singleton for placement and tracking
- [x] TowerData with stats (Damage, FireRate, Range, ProjectileSpeed)
- [x] TargetingSystem with priority (Attacking Wall > Boss > Closest > Highest HP > First Spawned)
- [x] Projectile system with travel and hit detection
- [x] PiercingRail for piercing tower
- [x] Critical hit system (5% base chance, 1.5x multiplier)
- [x] 5 tower slots with middle slot for starting tower

### References
- GDD Section 5: Towers
- GDD Section 5.4: Tower Targeting Priority
- GDD Section 5.5: Critical Hit System

---

## Epic 4: Firewall System ✅

Defensive wall with HP tracking and visual feedback.

### Completed
- [x] Firewall singleton with HP (base 2000)
- [x] Health states (Healthy, Damaged, Critical, Destroyed)
- [x] Visual feedback (color changes based on health)
- [x] Events for HP changes and state changes
- [x] TakeDamage, Heal, HealPercent methods
- [x] Game over trigger when HP reaches 0

### References
- GDD Section 3.1: The Firewall

---

## Epic 5: Wave Spawning ✅

Timed enemy spawning based on wave data.

### Completed
- [x] WaveSpawner component
- [x] WaveData structure for wave definitions
- [x] 20 waves for Stage 1 (hardcoded)
- [x] Timed enemy spawning within waves
- [x] Wave state transitions (InProgress → Transitioning → InProgress)
- [x] 1.0s inter-wave delay

### References
- GDD Section 8: Stage & Wave Design
- GDD Section 8.3: Wave Definition

---

## Epic 6: Score System ✅

Score tracking with bonuses and card thresholds.

### Completed
- [x] ScoreManager singleton
- [x] Base score per enemy type (Virus: 10, Worm: 15, Ransomware: 200)
- [x] Kill streak bonus (+1 per kill within 2s, max +50)
- [x] Multi-kill bonus (+5 per additional enemy same frame, max +100)
- [x] Card thresholds: 50, 120, 220, 350, 520, 730, 1000, 1350
- [x] OnCardThresholdReached event

### Not Implemented
- [ ] Overkill bonus (+10% of excess damage)
- [ ] End-of-run multipliers (No-Leak, Difficulty, Full Clear)
- [ ] Time-based score decay (anti-exploit)
- [ ] Kill rate minimum check

### References
- GDD Section 3.6: Score System
- GDD Section 3.10: Anti-Exploit Measures

---

## Epic 7: Basic UI ✅

Essential HUD elements for gameplay.

### Completed
- [x] RunUI - Wave counter display
- [x] FirewallUI - Health bar with color states
- [x] ScoreUI - Score display with card threshold progress
- [x] PauseUI - Pause button and overlay with Resume/Restart/Quit
- [x] VignetteOverlay - Red pulse at ≤25% HP
- [x] ConfirmationDialog - Modal for destructive actions
- [x] MenuUI - Start button, state-based visibility
- [x] DamageNumbers - Floating damage text with crit styling
- [x] UIFactory for programmatic UI creation
- [x] UIConstants for consistent styling

### References
- GDD Section 3.3: Visual Feedback Elements

---

## Epic 8: Card System ✅

Score-triggered card selection for tower placement and upgrades.

### Completed
- [x] CardManager singleton with CardPool integration
- [x] CardData struct (Id, Category, TowerType, UpgradeTier, UpgradeType, TargetTowerSlot, HealPercent, DisplayName, Description)
- [x] CardCategory enum (PlaceTower, TowerUpgrade, WallRepair)
- [x] UpgradeType enum (None, Damage, FireRate)
- [x] Card threshold integration with ScoreManager
- [x] GameState.CardSelection state
- [x] **Card Selection UI** - CardSelectionUI with 3 card display
- [x] **CardUI component** - Individual card visuals per category
- [x] **Random card generation** - CardPool with pool-based selection
- [x] **Card weighting** - 15% wall repair roll when damaged
- [x] **Slot selection for PlaceTower** - SlotSelectionModal with 5 slot buttons
- [x] **Tower upgrade cards** - Damage+/FireRate+ Tier 1 (+25%) and Tier 2 (+50%)
- [x] **Card pool exhaustion** - Shows fewer cards when pool limited
- [x] **Decrypt Key reroll system** - DecryptKeyManager with 1 key per reroll

### References
- GDD Section 6: Card System
- GDD Section 6.2: Card Types
- GDD Section 6.4: Card Selection Rules
- GDD Section 6.5: Card Rerolls

---

## Epic 9: Tower Upgrades ✅

In-run tower upgrades through card selection.

### Completed
- [x] Add upgrade tracking to Tower entity (DamageTier, FireRateTier)
- [x] Implement Tier 1 Damage+ (+25% damage multiplier)
- [x] Implement Tier 2 Damage+ (+50% damage, replaces Tier 1)
- [x] Implement Tier 1 Fire Rate+ (+25% attack speed)
- [x] Implement Tier 2 Fire Rate+ (+50% attack speed, replaces Tier 1)
- [x] Generate upgrade cards targeting specific placed towers
- [x] Prevent duplicate upgrade cards (only available upgrades in pool)

### Not Implemented
- [ ] Update tower tooltip to show applied upgrades (deferred to Epic 20: Polish)

### References
- GDD Section 6.3: Tower Upgrade Options

---

## Epic 10: Victory & Defeat ✅

End-of-run screens with rewards and retry options.

### Completed
- [x] Create unified GameOverUI screen (Victory/Defeat based on outcome)
   - Display: Wave reached, enemies defeated, final score
   - Victory: "VICTORY" title (cyan), personal best tracking
   - Defeat: "FIREWALL BREACHED" title (red)
   - Buttons: Restart/Retry, Menu
- [x] Create RunStats singleton to track run statistics
   - Enemies defeated (total and by type)
   - Wave reached, perfect wall tracking
   - Final score capture
- [x] Implement victory condition check (wave 20 + all enemies dead)
   - WaveManager now waits for OnAllEnemiesDefeated before victory
- [x] Track personal best scores per stage (PlayerPrefs)

### Deferred to Future Epics
- [ ] Currency earned display (Epic 14: Currency System)
- [ ] Score breakdown with multipliers (Epic 12: Difficulty System)
- [ ] Change Stage button (Epic 11: Stage Data)

### References
- GDD Section 3.8: Run Failure & Rewards
- GDD Section 3.9: Victory Conditions

---

## Epic 11: Stage Data ✅

Data-driven stage definitions with JSON files.

### Completed
- [x] Create StageData class and JSON loader (`Core/StageData.cs`)
- [x] Define stage file schema (stageId, stageName, waves, rewards)
- [x] Create Stage 1-1 "Entry Point" data file (`Resources/Stages/stage_1_1.json`)
- [x] Create Stage 1-2 "Packet Storm" data file (`Resources/Stages/stage_1_2.json`)
- [x] Create Stage 1-3 "Payload Delivery" data file (`Resources/Stages/stage_1_3.json`)
- [x] Create Stage 1-4 "Privilege Escalation" data file (`Resources/Stages/stage_1_4.json`)
- [x] Create Stage 1-5 "Root Access" data file (`Resources/Stages/stage_1_5.json`)
- [x] Create StageManager singleton (`Core/StageManager.cs`) for stage selection, loading, and unlock tracking
- [x] Update WaveSpawner to load from StageManager instead of hardcoded waves
- [x] Create StageSelectUI (`UI/StageSelectUI.cs`) with 5 stage buttons and lock/unlock status
- [x] Update MenuUI to navigate to stage selection before starting runs
- [x] Implement stage unlock progression (victory unlocks next stage, saved to PlayerPrefs)
- [x] Update GameOverUI to show stage info and track per-stage personal bests

### References
- GDD Section 8: Stage & Wave Design
- GDD Section 8.7: Example Stage File
- GDD Section 8.9: Stage Unlock System
- GDD Appendix A: Stage Names

---

## Epic 12: Difficulty System ⬜

Normal/Hard difficulty selection with stat modifiers.

### Tasks
1. Create DifficultyLevel enum (Normal, Hard)
2. Store selected difficulty in GameManager
3. Apply HP multiplier (Normal: 1.0x, Hard: 1.5x)
4. Apply speed multiplier (Normal: 1.0x, Hard: 1.2x)
5. Apply reward multiplier (Normal: 1.0x, Hard: 1.5x)
6. Add difficulty selector to stage select UI
7. Track Hard mode unlock per stage (requires Normal clear)
8. Display difficulty badge on stage buttons

### References
- GDD Section 8.5: Difficulty Selection

---

## Epic 13: Battle Profiles ⬜

Pre-run loadout configuration.

### Tasks
1. Create BattleProfile data structure
   - Starting tower selection
   - Gear slots (5)
   - Profile name
2. Create ProfileManager for saving/loading profiles
3. Implement 6 profile slots
4. Create ProfileEditorUI
5. Add starting tower selector (unlocked Basic towers)
6. Integrate profile selection into run start flow
7. Apply profile's starting tower on run start

### References
- GDD Section 3.4: Battle Profiles

---

## Epic 14: Currency System ⬜

Data Shards and Decrypt Keys tracking.

### Tasks
1. Create CurrencyManager singleton
2. Implement Data Shards tracking
   - Wave completion: 5 shards
   - Enemy kills: Virus 1, Worm 2, Ransomware 15
   - Stage clear bonus: 50 (Normal), 75 (Hard)
   - Perfect clear bonus: +25
3. Implement Decrypt Keys tracking
   - Boss kill: 1 key
   - Perfect clear: +1 key
   - Hard mode clear: +1 key
4. Apply difficulty multiplier to shard rewards
5. Save/load currency to PlayerPrefs or file
6. Create currency display in main menu header
7. Implement key cap (max 99)

### References
- GDD Section 7.1: Progression Currencies

---

## Epic 15: Tower Unlocks ⬜

Permanent tower unlocks with currency.

### Tasks
1. Create TowerUnlockManager
2. Track unlock state per tower type
3. Implement unlock costs:
   - Logic Bomb: 200 shards
   - Zero-Day Striker: 250 shards
   - Traceroute Cannon: 300 shards
   - Brute Force Node: 400 shards
4. Filter card pool to only include unlocked towers
6. Save/load unlock state
7. Create unlock UI in shop Arsenal tab

### References
- GDD Section 7.2: Tower Unlocks
- GDD Section 10.2: Arsenal

---

## Epic 16: Mastery System ⬜

Permanent tower damage upgrades with Level 5 abilities.

### Tasks
1. Create MasteryManager
2. Track mastery level per tower (0-5)
3. Implement mastery costs per tower:
   - Antivirus Turret: 75, 150, 300, 600, 1200
   - Logic Bomb: 100, 200, 400, 800, 1600
   - Zero-Day Striker: 125, 250, 500, 1000, 2000
   - Traceroute Cannon: 150, 300, 600, 1200, 2400
   - Brute Force Node: 100, 200, 400, 800, 1600
4. Apply mastery damage bonus (+10/20/30/40/50%)
5. Implement Level 5 abilities:
   - Antivirus Turret: Overclocked Processor (+15% crit chance, +0.5x crit multiplier)
   - Logic Bomb: Firewall Cascade (burning ground)
   - Zero-Day Striker: Precision Strike (+50% damage to >50% HP enemies)
   - Traceroute Cannon: Network Breach (mark for +15% damage, stacks to 2)
   - Brute Force Node: Dictionary Attack (4th shot, +25% per consecutive hit)
6. Create Mastery UI in shop
7. Display mastery level on tower tooltips

### References
- GDD Section 7.3: Tower Mastery
- GDD Section 5.6: Status Effects

---

## Epic 17: Gear System ⬜

Equipment with passive bonuses and triggered effects.

### Tasks
1. Create GearData scriptable objects
2. Define 5 gear slots (Firmware, Protocol, Targeting, Network, Utility)
3. Implement gear rarities (Common, Uncommon, Rare, Epic, Legendary)
4. Create gear inventory and equipped state
5. Implement all Common gear effects
6. Implement all Uncommon gear effects
7. Implement Rare gear with triggered effects
8. Implement Epic gear with powerful effects
9. Implement Legendary gear with drawbacks
10. Create gear unlock costs and save state
11. Integrate gear into Battle Profile editor
12. Apply gear effects at run start

### References
- GDD Section 9: Gear System
- GDD Section 9.5: Gear List

---

## Epic 18: Chip System ⬜

Socketed stat modules for gear customization.

### Tasks
1. Create ChipData structure
2. Implement chip types:
   - Offensive: DMG, SPD, CRIT, CRIT-X, BURST, EXECUTE
   - Defensive: HP, ARMOR, REGEN, EMERGENCY
   - Economy: SHARD, SCORE, THRESH, BONUS
   - Utility: RANGE, PROJECTILE, SPLASH, SLOW
3. Implement stack caps per chip type
4. Create chip inventory
5. Implement socket system for gear
6. Create chip slot/unslot UI
7. Implement chip acquisition:
   - Wave 10 reached: 1 random chip
   - Wave 20 cleared: 2 random chips
   - Boss kill: 15% chance drop
   - Perfect clear: 1 guaranteed + 1 random
   - Shop purchase
8. Apply chip bonuses at run start

### References
- GDD Section 9.3: Chip Types
- GDD Section 9.4: Chip Acquisition

---

## Epic 19: Shop System ⬜

Between-runs shop for purchases and upgrades.

### Tasks
1. Create ShopUI with tabbed interface
2. Implement Arsenal tab (tower unlocks)
3. Implement Mastery tab (tower upgrades)
4. Implement Supply tab (chips and consumables)
5. Implement chip purchasing (random: 50, pack: 120, targeted: 150)
6. Implement Decrypt Key purchasing (200 shards per key)
7. Implement Black Market tab with rotating stock
8. Add daily refresh timer for Black Market
9. Implement discounted items (20-40% off)
10. Implement Cosmetics tab (visual customization)
11. Add purchase confirmation and currency deduction

### References
- GDD Section 10: Shop System

---

## Epic 20: Polish & Audio ⬜

Visual effects, audio, and game feel improvements.

### Tasks
1. **Visual Effects**
   - Screen shake for impacts
   - Particle systems for tower placement, enemy deaths
   - Card threshold progress bar
   - Enemy attack animations
   - Tower firing effects
   - Glitch effects on Firewall damage

2. **Audio System**
   - Create AudioManager singleton
   - Tower firing sounds
   - Enemy spawn sounds
   - Damage sounds
   - UI button sounds
   - Background music
   - Victory/defeat stingers

3. **Tower Tooltips**
   - Show stats on tower hover
   - Display applied upgrades
   - Show mastery level and ability
   - Show range indicator circle

4. **Visual Identity**
   - Implement cybersecurity color palette
   - Terminal/console UI aesthetic
   - Scan line effects

### References
- GDD Section 2: Theme & Setting
- GDD Section 3.3: Visual Feedback Elements

---

## Dependency Graph

```
Epic 1 (Core) ─────────┬──────────────────────────────────────────────────────┐
                       │                                                       │
Epic 2 (Enemy) ────────┤                                                       │
                       │                                                       │
Epic 3 (Tower) ────────┼─── Epic 5 (Waves) ─── Epic 11 (Stage Data) ──────────┤
                       │                              │                        │
Epic 4 (Firewall) ─────┤                              └─── Epic 12 (Difficulty)│
                       │                                                       │
Epic 6 (Score) ────────┼─── Epic 8 (Cards) ─── Epic 9 (Upgrades) ─────────────┤
                       │                                                       │
Epic 7 (UI) ───────────┼─── Epic 10 (Victory/Defeat) ─────────────────────────┤
                       │                                                       │
                       └─── Epic 14 (Currency) ─── Epic 15 (Unlocks) ─────────┤
                                    │                     │                    │
                                    │                     └─── Epic 16 (Mastery)
                                    │                                          │
                                    └─── Epic 17 (Gear) ─── Epic 18 (Chips) ──┤
                                                │                              │
                                                └─── Epic 19 (Shop) ───────────┤
                                                                               │
Epic 13 (Profiles) ────────────────────────────────────────────────────────────┤
                                                                               │
Epic 20 (Polish) ──────────────────────────────────────────────────────────────┘
```

---

## MVP Milestone

Minimum viable product for internal testing:

- [x] Epics 1-7 (Core gameplay loop)
- [x] Epic 8: Card System (complete)
- [x] Epic 9: Tower Upgrades
- [x] Epic 10: Victory & Defeat screens
- [x] Epic 11: Stage Data (all 5 stages complete)

---

## Launch Milestone

Required for public release:

- [ ] All MVP items
- [ ] Epic 11: All 5 stages
- [ ] Epic 12: Difficulty System
- [ ] Epic 14: Currency System
- [ ] Epic 15: Tower Unlocks
- [ ] Epic 16: Mastery System
- [ ] Epic 20: Core audio and visual polish

---

## Post-Launch Content

Lower priority features for future updates:

- Epic 13: Battle Profiles
- Epic 17: Gear System
- Epic 18: Chip System
- Epic 19: Full Shop System
- Deployables (Barrier, Mine, Warp Trap, Disruptor)
- Chapter 2+ stages
- Random Mode
- Leaderboards
- Daily challenges
- Cosmetics

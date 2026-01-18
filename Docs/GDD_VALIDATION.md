# GDD vs Implementation Validation Report

**Generated**: 2026-01-17
**Comparing**: `Docs/GDD.md` against current codebase implementation

---

## Summary

The core gameplay systems are implemented but several balance values diverge from the GDD specifications. Key systems like Card Selection, Score/Currency, Gear, and Stage data are not yet implemented.

---

## Implemented Systems

| System | Status | Notes |
|--------|--------|-------|
| GameManager State Machine | Complete | States: Menu, Playing, Paused, CardSelection, GameOver |
| WaveManager | Complete | Idle → InProgress → Transitioning (1s) |
| Firewall | Complete | 2000 HP, health states, visual feedback |
| Enemy System | Complete | 3 types with movement & wall attacks |
| Tower System | Complete | 5 types with targeting, projectiles |
| UI Framework | Complete | Wave display, health bar, pause menu |
| Debug Controls | Complete | F1-F10, T, Shift+T shortcuts |

---

## Not Implemented (Per GDD)

| System | GDD Section | Priority |
|--------|-------------|----------|
| Card System | Section 6 | High |
| Score System | Section 3.6 | High |
| Data Shards/Decrypt Keys | Section 7.1 | Medium |
| Battle Profiles | Section 3.4 | Medium |
| Gear & Chips | Section 9 | Medium |
| Shop System | Section 10 | Low |
| Stage Files (JSON) | Section 8.7 | High |
| Wave Spawner | — | High |
| Damage Numbers | Section 3.3 | Medium |
| Tower Tooltips | Section 3.3 | Low |
| Mastery System | Section 7.3 | Low |
| Menu Screen | — | Medium |

---

## Value Discrepancies

### Enemy Stats

| Enemy | Property | GDD | Implementation | Issue |
|-------|----------|-----|----------------|-------|
| Virus | Speed | 0.1 | 0.2 | 2× faster than GDD |
| **Worm** | **Speed** | **0.16** | **1.3** | **~8× faster than GDD** |
| Ransomware | Speed | 0.06 | 0.2 | ~3× faster than GDD |
| **Ransomware** | **Score** | **200** | **100** | **Half the GDD value** |

*All HP, wall damage, attack cooldown values match GDD.*

**Source**: `Assets/Scripts/Enemies/EnemyData.cs`

### Tower Stats

| Tower | Property | GDD | Implementation | Issue |
|-------|----------|-----|----------------|-------|
| Base Tower | Fire Rate | 1.0/s | 0.5/s | Half GDD rate (50 DPS vs 25 DPS) |
| Base Tower | Range | 0.9 | 1.2 | 33% larger than GDD |
| Base Tower | Projectile Speed | 1.5 | 10.0 | Different scale |
| AOE Tower | Fire Rate | 1.2/s | 0.6/s | Half GDD rate |
| Burst Tower | Fire Rate | 0.33/s | 0.165/s | Half GDD rate |
| Piercing Tower | Fire Rate | 1.0/s | 0.5/s | Half GDD rate |
| Brute Force | Fire Rate | 0.83/s | 0.415/s | Half GDD rate |

*All damage values, AOE splash radius/falloff, and range (except Base Tower) match GDD.*

**Source**: `Assets/Scripts/Towers/TowerData.cs`

### DPS Implications

| Tower | GDD DPS | Implementation DPS | Difference |
|-------|---------|-------------------|------------|
| Base Tower | 50 | 25 | -50% |
| AOE Tower | 48 | 24 | -50% |
| Burst Tower | 50 | 24.75 | -50% |
| Piercing Tower | 50 | 25 | -50% |
| Brute Force | 45 | 22.5 | -50% |

**All towers deal half the intended DPS.** This significantly impacts game balance.

---

## Targeting System

| Priority | GDD Order | Implementation | Match |
|----------|-----------|----------------|-------|
| 1 | Attacking Wall | Attacking Wall | ✓ |
| 2 | Boss (Ransomware) | Ransomware | ✓ |
| 3 | Closest to Wall | Closest to Firewall | ✓ |
| 4 | Highest Health | Highest HP | ✓ |
| 5 | First Spawned | SpawnOrder tiebreaker | ✓ |

*Targeting priority matches GDD specification.*

**Source**: `Assets/Scripts/Towers/TargetingSystem.cs`

---

## Critical Hit System

| Property | GDD | Implementation | Match |
|----------|-----|----------------|-------|
| Base Crit Chance | 5% | 5% (hardcoded) | ✓ |
| Base Crit Multiplier | 1.5× | 1.5× (hardcoded) | ✓ |
| Visual Feedback | Red color, "!" suffix | Different colored projectile | Partial |

*Base values implemented, but mastery/gear bonuses are not.*

**Source**: `Assets/Scripts/Towers/Tower.cs`

---

## Layout & Coordinates

| Element | GDD | Implementation | Match |
|---------|-----|----------------|-------|
| Spawn Line Y | (top of screen) | Y = 8 | ✓ |
| Firewall Y | (above tower slots) | Y = -4 | ✓ |
| Tower Slots Y | (bottom) | Y = -6 | ✓ |
| Tower Slot Count | 5 (indices 0-4) | 5 slots | ✓ |
| Middle Slot | Slot 3 (index 2) | Index 2 | ✓ |
| Play Width | — | 10 units | ✓ |

**Source**: `Assets/Scripts/Core/GameLayout.cs`

---

## Run Flow

| Phase | GDD | Implementation | Match |
|-------|-----|----------------|-------|
| Starting Tower | Middle slot auto-place | Places StartingTowerType | ✓ |
| Wave Count | 20 waves | TotalWaves = 20 | ✓ |
| Inter-wave Break | 1.0s | WaveTransitionDelay = 1.0s | ✓ |
| Victory Condition | All 20 waves + all enemies dead | Checks wave > 20 | ✓ |
| Defeat Condition | Firewall HP = 0 | OnFirewallDestroyed triggers EndRun(false) | ✓ |

**Source**: `Assets/Scripts/Core/GameManager.cs`, `Assets/Scripts/Core/WaveManager.cs`

---

## Recommendations

### Critical Priority

1. **Fix Fire Rates**: All tower fire rates are half the GDD values. This makes the game significantly harder than intended.
   - File: `Assets/Scripts/Towers/TowerData.cs`
   - Change: Double all `fireRate` values

2. **Fix Enemy Speeds**: Worm speed of 1.3 is ~8× the GDD value of 0.16. Ransomware score is 100 vs GDD 200.
   - File: `Assets/Scripts/Enemies/EnemyData.cs`
   - Change: Adjust speed values and Ransomware score

3. **Implement Wave Spawner**: No system exists to spawn enemies according to wave definitions. Currently only debug spawning works.

### High Priority

4. **Implement Card System**: The CardSelection state exists but no card UI or selection logic.

5. **Implement Score System**: No score tracking despite GDD having detailed score mechanics.

### Medium Priority

6. **Verify Coordinate Systems**: Projectile speeds and movement speeds may use different unit scales than GDD's normalized coordinates.

7. **Add Damage Numbers**: Floating damage text per GDD Section 3.3.

8. **Create Menu Screen**: No menu UI exists for the Menu state.

---

## Files Reviewed

- `Assets/Scripts/Core/GameBootstrap.cs`
- `Assets/Scripts/Core/GameManager.cs`
- `Assets/Scripts/Core/WaveManager.cs`
- `Assets/Scripts/Core/GameLayout.cs`
- `Assets/Scripts/Firewall/Firewall.cs`
- `Assets/Scripts/Enemies/Enemy.cs`
- `Assets/Scripts/Enemies/EnemyManager.cs`
- `Assets/Scripts/Enemies/EnemyData.cs`
- `Assets/Scripts/Towers/Tower.cs`
- `Assets/Scripts/Towers/TowerManager.cs`
- `Assets/Scripts/Towers/TowerData.cs`
- `Assets/Scripts/Towers/TargetingSystem.cs`
- `Assets/Scripts/Towers/Projectile.cs`
- `Assets/Scripts/UI/*.cs`

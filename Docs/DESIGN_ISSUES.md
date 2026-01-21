# Known Design Issues & Gaps

> This content was previously Appendix F of the [Game Design Document](GDD.md).

This document tracks known design limitations and gaps that may require future attention. Issues are organized into **Active** (unresolved) and **Resolved** sections.

---

## Active Issues

### F.6 Tower Overkill / Wasted Shots

Multiple towers sometimes fire at the same enemy when a single shot would have been sufficient, wasting damage potential. This is most noticeable when several towers target a low-health enemy simultaneously.

**Potential Solutions:**

1. **Damage Reservation** - Towers calculate if their shot will kill the target before firing. If so, mark the target with "reserved damage" so other towers prefer different targets.
   - *Complication:* Critical hits (5% chance, 1.5x damage) make damage prediction unreliable
   - *Complication:* Projectile travel time means conditions can change before impact
   - *Complication:* AOE and Piercing towers hit multiple targets, making reservation complex

2. **Soft Preference** - Track "incoming damage" on enemies as a hint rather than a hard lock. Towers prefer targets without enough incoming damage to kill, but still fire at marked targets if nothing else is available.
   - *Benefit:* Avoids the "everyone ignores the last enemy" edge case
   - *Benefit:* More forgiving of crit variance

3. **Balance Tuning** - Adjust tower fire rates, damage values, or enemy HP so overkill is less frequent naturally.
   - *Benefit:* Simplest solution, no code complexity
   - *Consideration:* May affect intended difficulty curve

**Status:** Pending investigation. May be resolved through balance tuning alone. Revisit after tower stat balancing pass.

**See Also:** Section 5.4 (Tower Targeting Priority) in GDD

---

## Resolved Issues

### ~~F.5 Critical Hit System Issues~~ *(Addressed)*

~~The critical hit system had multiple undocumented behaviors, missing UI elements, and inconsistent interactions.~~

*Resolution: Section 5.5 rewritten with unified crit formula. All issues addressed:*

| ID | Issue | Resolution |
|----|-------|------------|
| F.5.1 | Undocumented Base Crit | Documented in Section 5.5; CRIT line added to tooltip (Section 3.3) |
| F.5.2 | No Crit Upgrades | Marked "Not planned" in Appendix E; crit scaling via Gear/Chips instead |
| F.5.3 | No Tooltip Display | New CRIT line shows chance × multiplier with source breakdown |
| F.5.4 | Only Antivirus Turret Has Crit Mastery | Documented as intentional; new crit gear provides alternatives |
| F.5.5 | Splash Crit Inheritance | "Once per explosion" rule added; "!" only on impact point |
| F.5.6 | Two-Tier Crit Gap | New gear (critical_section.dll +5%, buffer_overflow.dll +8%, exception_handler.dll +10%) provides intermediate options |
| F.5.7 | Rounding Behavior | Changed to `Math.round()` in Section 5.5 |

---

### ~~F.1 Targeting Priority Creates Strategic Limitations~~ *(Addressed)*

~~The fixed targeting priority system removes tactical decision-making during gameplay.~~

*Resolution: The fixed system is an intentional design choice to keep gameplay focused on tower placement and upgrades rather than micro-management. Boss priority rule added to ensure high-value targets are focused. See Section 5.4.*

**Final Priority Order:**
1. Attacking Wall (highest)
2. Closest to Wall
3. Boss Priority (within same distance tier)
4. Highest Health
5. First Spawned (tiebreaker)

### ~~F.2 Difficulty Scaling Imbalance~~ *(Improved)*

~~The Hard difficulty compounds multiple multipliers but offers disproportionately low reward scaling.~~

*Spawn Rate multiplier was removed, simplifying the difficulty model:*

| Modifier | Normal | Hard |
|----------|--------|------|
| HP | 1.0x | 1.5x |
| Speed | 1.0x | 1.2x |
| **Combined Difficulty** | 1.0x | **~1.8x** |
| Reward | 1.0x | 1.5x |

*Players now face ~1.8x difficulty for 1.5x reward—closer to fair. See Section 8.5.*

**Remaining Consideration:**
- Could increase Hard reward to 1.75x for perfect parity
- Could add Easy difficulty (0.75x HP/Speed) for accessibility

### ~~F.3 Wave HP Scaling vs DPS~~ *(Resolved)*

~~The current HP scaling formula may result in trivial late-game encounters.~~

*Resolution: Per-wave scaling increased from 8% to 10%. See BALANCE_GUIDELINES.md.*

**Updated Time-to-Kill (5 fully upgraded towers @ 562.5 total DPS):**

| Target | HP (Hard) | Time to Kill |
|--------|-----------|--------------|
| Wave 20 Basic | 435 | 0.77 seconds |
| Wave 20 Boss | 2175 | 3.9 seconds |

Late-game is now ~15% more challenging. Further tuning can be done via stage files if needed.

### F.4 Missing Mechanics Documentation

The following mechanics are referenced but not fully specified:

#### ~~Currency Values~~ *(Resolved)*
- ~~Section 7.1 lists currency sources but no values are defined~~
- *Now defined in Section 7.1: Currency Sources, Difficulty Multipliers, Earning Examples*

#### ~~Stage Unlock Flow~~ *(Resolved)*
- ~~No documentation on how players unlock stages 1-2 through 1-5~~
- ~~Hard mode unlock requirements not specified~~
- *Now defined in Section 8.9: Stage Unlock System*

#### ~~Run Failure Rewards~~ *(Resolved)*
- ~~What happens when Wall HP reaches zero was not documented~~
- ~~Partial rewards, retry flow, and edge cases were undefined~~
- *Now defined in Section 3.8: Run Failure & Rewards*

#### ~~Score Values~~ *(Resolved)*
- ~~Section 3.6 states "Different enemy types award different score values" but no values are defined~~
- ~~Score thresholds for card selection are not documented~~
- *Now defined in Sections 3.6 and 6.1*

#### ~~Spawn Rate Multiplier~~ *(Resolved)*
- ~~Section 8.5 lists Spawn Rate Multiplier for difficulties~~
- ~~Unclear if this reduces `spawnTime` values or increases enemy count per wave~~
- *Spawn Rate multiplier removed from difficulty system. See Section 8.5.*

#### ~~Effect Stacking Rules~~ *(Resolved)*
- ~~Can multiple Utility Towers stack slow effects?~~
- ~~Does burning ground (Cascade ability) stack or refresh duration?~~
- ~~How do multiple Network Breach marks interact?~~
- *Now defined in Section 5.6: Status Effects*

#### ~~Tower Tooltip Format~~ *(Resolved)*
- ~~Section 3.3 mentions tooltips show "DMG, SPD, RNG, DPS"~~
- ~~DPS calculation formula not documented~~
- ~~How are mastery bonuses and in-run upgrades displayed?~~
- *Now defined in Section 3.3: Tower Tooltip Format*

#### ~~Card Pool Exhaustion~~ *(Resolved)*
- ~~Section 6.4 defines card generation rules~~
- ~~No fallback behavior defined when all cards exhausted~~
- *Now defined in Section 6.4: Card Pool Exhaustion*

#### ~~Status Effect Duration~~ *(Resolved)*
- ~~Slow effect duration not specified (per-hit refresh? Fixed duration?)~~
- ~~Burning ground tick rate not specified (every 0.5s? 1s?)~~
- ~~Network Breach mark duration (5s) documented, but refresh behavior unclear~~
- *Now defined in Section 5.6: Status Effects*

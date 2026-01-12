# Base Tower Defense - *Game Design Document*

---

## 1. Overview

Base Tower Defense is a tower defense game where players protect a defensive wall from waves of enemies. Players place defensive towers in 5 fixed slots to intercept waves of attackers before they reach the wall.

---

## 2. Theme & Setting

The game takes place in a stylized data center environment where players defend a network's Firewall against waves of malicious software. Visual elements reinforce the cybersecurity setting:

- **Environment**: Server racks, glowing data streams, circuit board patterns
- **Particle Effects**: Data packets, encryption fragments, digital sparks
- **The Firewall**: A protective barrier visualized as a glowing security perimeter
- **Enemies**: Viruses, ransomware, and other malware represented as hostile digital entities

### Visual Identity

| Element | Implementation |
|---------|----------------|
| Color Palette | Deep blues, neon greens, warning reds, data-stream cyan |
| UI Style | Terminal/console aesthetic, monospace fonts, scan lines |
| Effects | Glitch effects, pixelation on damage, data corruption visuals |
| Audio | Electronic/synthetic sounds, alert tones, digital impacts |

*Alternative theme options are documented in Appendix G for future reskins.*

### Art Pipeline

**Development Phases:**
- **Epics 1-4 (Placeholders):** TextMeshPro labels as stand-in visuals (`[V]` for Virus, `[T1]` for towers, etc.)
- **Epic 5+ (Final Art):** AI-generated high-resolution sprites using image generation tools

**Tools:** Gemini Imagen (Nano Banana Pro), Midjourney, DALL-E, or similar AI image generators

**Target Style:** High-resolution 2D art (512x512 or higher), clean vector/painted style, cyberpunk/neon aesthetic, dark backgrounds with glowing accents. Sprites should be crisp at all screen sizes for mobile and desktop.

### AI Art Prompts

Prompts for generating game assets. All sprites should be high resolution with transparent backgrounds for game use.

#### Enemies

| Entity | Placeholder | AI Prompt |
|--------|-------------|-----------|
| **Virus (Basic)** | `[V]` | "High resolution 2D game sprite, hostile computer virus entity, geometric crystalline shape with menacing digital face, glowing red energy core, dark purple translucent body, floating particles, transparent background, cyberpunk style, clean vector art, top-down perspective" |
| **Worm (Fast)** | `[W]` | "High resolution 2D game sprite, fast digital worm malware creature, sleek serpentine form, segmented body with neon green bioluminescent glow, speed motion trails, sharp angular design, transparent background, cyberpunk aesthetic, vector illustration" |
| **Ransomware (Boss)** | `[R]` | "High resolution 2D game sprite, massive ransomware boss entity, imposing hovering figure with skull-padlock hybrid head, chains of encrypted data orbiting around it, dark crimson armor with gold circuit patterns, ominous red eye glow, transparent background, detailed cyberpunk illustration, intimidating presence" |

#### Towers

| Entity | Placeholder | AI Prompt |
|--------|-------------|-----------|
| **Antivirus Turret (Base)** | `[T1]` | "High resolution 2D game sprite, sleek antivirus defense turret, modern security scanner design with rotating barrel, glowing cyan energy core, polished chrome and white armor plating, holographic shield emblem, transparent background, cyberpunk tech style, clean illustration" |
| **Logic Bomb (AOE)** | `[T2]` | "High resolution 2D game sprite, explosive logic bomb launcher tower, spherical mortar design, industrial warning stripes orange and black, glowing payload chamber, steam vents, heavy reinforced base, transparent background, cyberpunk military tech" |
| **Zero-Day Striker (Burst)** | `[T3]` | "High resolution 2D game sprite, precision zero-day sniper tower, long elegant barrel with advanced scope, electric blue charging coils, minimalist angular chrome design, energy capacitor glowing, transparent background, futuristic cyberpunk weapon" |
| **Traceroute Cannon (Piercing)** | `[T4]` | "High resolution 2D game sprite, powerful railgun tower, twin parallel magnetic rails with electric arc crackling between them, purple plasma energy core, heavy industrial frame, cooling fins, transparent background, cyberpunk sci-fi artillery" |
| **Brute Force Node (Advanced)** | `[T5]` | "High resolution 2D game sprite, rapid-fire brute force node, triple rotating barrel gatling-style design, spinning mechanism visible, overheated orange barrel glow, heavy armored ammunition feeds, smoke wisps, transparent background, cyberpunk aesthetic" |

#### Firewall & Environment

| Entity | Placeholder | AI Prompt |
|--------|-------------|-----------|
| **Firewall (Full Health)** | `████████` | "High resolution game asset, horizontal digital firewall barrier, luminous blue hexagonal honeycomb energy shield, pulsing light waves, holographic data streams flowing through it, pristine and powerful, seamless tileable, cyberpunk security aesthetic, dark background" |
| **Firewall (Damaged)** | `░░██░░██` | "High resolution game asset, damaged digital firewall barrier, cracked and fractured hexagonal grid, sections flickering red with corruption, glitch distortion effects, sparking broken edges, warning indicators, cyberpunk, dark background" |
| **Tower Slot (Empty)** | `[__]` | "High resolution 2D game sprite, circular tech platform for tower placement, metallic floor panel with subtle grid pattern, dim cyan edge lighting, holographic placement indicator, transparent background, clean cyberpunk design" |
| **Background** | — | "Game background, dark data center environment, server racks silhouettes, flowing data streams, circuit board floor pattern, deep blue and purple ambient lighting, subtle grid overlay, cyberpunk atmosphere, parallax-ready layers" |

#### Projectiles

| Entity | Placeholder | AI Prompt |
|--------|-------------|-----------|
| **Basic Projectile** | `•` | "High resolution game sprite, small cyan energy bullet, glowing orb with soft light trail, clean circular shape, transparent background, cyberpunk style" |
| **AOE Projectile** | `◉` | "High resolution game sprite, lobbed explosive orb projectile, orange plasma sphere, unstable energy shell, small warning glow, arc trail, transparent background, cyberpunk" |
| **Burst Projectile** | `▏` | "High resolution game sprite, high-velocity sniper round, elongated blue energy bolt, electric discharge particles, speed lines, transparent background, cyberpunk" |
| **Rail Beam** | `═══` | "High resolution game effect, instant hitscan energy beam, purple electric rail with lightning crackle, horizontal laser line, energy dissipation at edges, transparent background" |
| **Rapid Shot** | `···` | "High resolution game sprite, small rapid-fire tracer round, orange hot bullet, circular with motion blur, transparent background, simple clean design" |

#### UI Elements

| Element | Placeholder | AI Prompt |
|---------|-------------|-----------|
| **Health Bar Frame** | `[====]` | "High resolution UI element, horizontal health bar frame, sleek tech border with hexagonal end caps, brushed metal with cyan accent lighting, transparent background, cyberpunk HUD style, clean vector design" |
| **Card Frame** | text box | "High resolution UI element, rectangular upgrade card frame, circuit board pattern border, dark translucent center, neon blue edge glow, holographic sheen, cyberpunk trading card aesthetic, 3:4 aspect ratio" |
| **Data Shard (Currency)** | `◈` | "High resolution 2D icon, glowing data shard crystal currency, multifaceted geometric gem shape, cyan and white inner glow, floating light particles, transparent background, cyberpunk collectible, clean illustration" |

#### Status Effects

| Effect | Placeholder | AI Prompt |
|--------|-------------|-----------|
| **Burn** | `🔥` | "High resolution game icon, digital corruption fire effect, stylized orange and red flames with data glitch distortion, transparent background, cyberpunk style" |
| **Breach** | `⚡` | "High resolution game icon, security breach vulnerability mark, cracked shield with lightning bolt, electric green glow, transparent background, cyberpunk warning symbol" |
| **Slow** | `❄` | "High resolution game icon, system slowdown effect, frozen clock or ice crystal with blue cold glow, frost particles, transparent background, cyberpunk aesthetic" |

---

## 3. Core Mechanics

### 3.1 The Firewall

The Firewall is a protective barrier positioned just above the tower slots. Enemies travel toward it and attack it when they reach it. The Firewall visualizes damage through glitch effects, flickering segments, and corruption spreading across its surface. Game ends when Firewall HP reaches zero.

**Base Firewall HP:** 2000

**Enemy Interaction:**
- Enemies **stop moving** when they reach the wall
- Enemies **attack repeatedly** until killed (see Section 4.1 for damage values)
- First attack occurs after the enemy's attack cooldown (not immediately on contact)
- Multiple enemies at the wall compound damage rapidly

### 3.2 Tower Placement

- 5 fixed tower slots at the bottom of the screen, positioned behind the wall
- **Middle slot (slot 3)**: Reserved for the Basic Tower selected before the run
- **Outer slots (slots 1, 2, 4, 5)**: Available for Advanced and Special Towers; player chooses which slot to place each tower
- Each slot holds one active tower
- **Only one tower of each type can be deployed** - each tower type is unique per run
- Once placed, towers cannot be moved or swapped
- Advanced and Special Towers are placed via card selection (see Section 6: Card System)
- Towers are divided into three groups: Basic, Advanced, and Special (see Section 5: Towers)

### 3.3 Visual Feedback Elements

Visual feedback is prioritized to ensure gameplay information is clear for launch.

#### Must Have (Launch Critical)
Essential feedback for understanding game state:
- **Health Bars**: Enemies display health bars above them, depleting as they take damage
- **Damage Numbers**: Floating damage numbers appear above enemies when hit
- **Wall HP Display**: Prominent health bar or numeric display showing current/max Wall HP
- **Wave Indicators**: Clear display of current wave number and wave progress
- **Attack Indicators**: Basic visual indication when towers are firing (projectile trails/beams)
- **Score Updates**: Real-time score counter updates
- **Tower Tooltip**: Hovering over a tower shows current stats and applied upgrades (see format below)
- **Range Indicator**: Hovering over a tower shows its targeting range as a circular overlay

**Tower Tooltip Format:**
```
Antivirus Turret
DMG: 65 (+30%) (+25%)
SPD: 1.0/s (+0%)
RNG: 0.9
DPS: 81.25
─────────────
Upgrades: Tier 1 Damage
Mastery: Level 3 (+30% damage)
```

**DPS Calculation:** `DPS = BaseDamage × (1 + MasteryBonus) × (1 + DamageUpgrade) × FireRate × (1 + SpeedUpgrade)`

Example: Base Tower with Mastery 3 (+30%) and Tier 1 Damage (+25%):
- `50 × 1.30 × 1.25 × 1.0 = 81.25 DPS`

#### Nice to Have (Polish)
Elements that improve game feel and clarity but aren't strictly blocking:
- **Critical Hit Styling**: Critical hits display larger, more prominent numbers with distinct color *(Implemented: see Section 5.5 for details)*
- **Screen Shake**: Subtle shake for impactful events (boss spawn, wall damage, large explosions)
- **Particle Systems**: Thematic particles for tower placement, enemy deaths, and impacts
- **Card Threshold Progress**: Visual progress bar showing progress toward next card selection
- **Enemy Attack Animation**: Per-enemy-type animation when attacking the wall (distinct visual per enemy type)
- **Damage Indicators**: Screen flash or feedback when enemies attack the wall

#### Post-Launch (Enhancement)
Advanced feedback features for future updates:
- **Status Effect Visuals**: Distinct indicators for slow, DOT, stun, etc. *(Implemented: slow=❄ ice-blue, burn=🔥 orange, breach=⚡ green)*
- **Rare Card Distinction**: Special visual effects for Wall Repair cards
- **Low Health Warning**: Full-screen visual warning when Wall HP is critical *(Implemented: red vignette pulse at ≤25% health)*
- **Sound-Visual Sync**: Tighter synchronization of effects with audio

### 3.4 Battle Profiles

Battle Profiles are user-defined loadouts that determine the starting configuration for each run. Players create and customize profiles between runs, then select one before starting a stage.

**Profile Contents:**

| Component | Description |
|-----------|-------------|
| **Basic Tower** | The primary tower placed in the middle slot at run start |
| **Gear Selection** | Equipment that provides passive bonuses or special effects (see Section 9: Gear System) |
| **Other Settings** | Additional battle-affecting configurations (TBD) |

**Profile Rules:**
- Players can create multiple profiles for different strategies - 6 in total
- Only one profile is active per run
- Profile contents are locked once a run begins
- New profiles can be created at any time between runs

### 3.5 Run Flow

Each run follows this structure:

1. Player selects a **Battle Profile** (or uses default)
2. Run starts with the profile's Basic Tower placed in the **middle slot (slot 3)**
3. Fight waves of enemies continuously
4. Earn score by killing enemies
5. Card selection unlocks when score thresholds are reached (place Advanced/Special Towers, upgrade existing towers)
6. Run ends when all 20 waves complete or Wall HP reaches zero

During a run, there is no resource management. Strategic depth comes from profile configuration, tower placement timing, and upgrade choices.

### 3.6 Score System

Score is the primary progression metric during a run:

- Each enemy killed adds to the player's score
- Score thresholds unlock card selection opportunities
- Score persists across waves within a single run

**Enemy Score Values:**

| Enemy Type | Base Score |
|------------|------------|
| Basic Enemy | 10 |
| Fast Enemy | 15 |
| Boss Enemy | 100 |

Score values are not modified by difficulty—Hard mode rewards come from the end-of-run currency multiplier, not in-run score.

### 3.7 Run Controls

Players have access to the following controls during a run:

| Control | Action |
|---------|--------|
| **Pause** | Pauses the game, freezing all enemy movement and tower actions. UI overlay appears with control options. |
| **Resume** | Resumes gameplay from the paused state. Only available when game is paused. |
| **Restart** | Immediately ends the current run and starts a new run of the same stage with the same difficulty selection. Confirmation dialog required to prevent accidental restarts. |
| **Quit** | Exits the current run and returns to the main menu/stage selection. Progress is lost. Confirmation dialog required. |
| **Wave Transition** | Brief pause between waves (1-2s) for strategic assessment. No player action required—next wave begins automatically. |

**Control Availability:**
- Pause/Resume: Available at any time during active gameplay
- Restart: Available during gameplay or while paused
- Quit: Available during gameplay or while paused
- Controls are disabled during card selection (card selection itself acts as a pause state)

### 3.8 Run Failure & Rewards

When Wall HP reaches zero, the run ends immediately. Players receive partial rewards based on progress.

#### Failure Rewards

| Component | Awarded? | Calculation |
|-----------|----------|-------------|
| Wave completion bonus | Yes | 5 × waves completed |
| Enemy kill rewards | Yes | Full value for all kills |
| Boss kill rewards | Yes | Full value for all boss kills |
| Stage clear bonus | **No** | Only on victory |
| Perfect clear bonus | **No** | Only on victory |
| Difficulty multiplier | Yes | Applied to all earned rewards |

#### Failure Penalty

- **No stage unlock progress** - Must complete the stage to unlock the next
- **No Hard mode unlock** - Must complete on Normal to unlock Hard for that stage
- **Lost clear bonus** - 50 shards (75 on Hard) not awarded

#### Retry Flow

After a failed run, players see:

```
╔═══════════════════════════════════════╗
║          FIREWALL BREACHED            ║
╠═══════════════════════════════════════╣
║  Wave Reached:     15 / 20            ║
║  Enemies Defeated: 187                ║
║  ─────────────────────────────────    ║
║  Shards Earned:    262                ║
╠═══════════════════════════════════════╣
║  [RETRY]  [CHANGE STAGE]  [MENU]      ║
╚═══════════════════════════════════════╝
```

**Design Notes:**
- Partial rewards respect player time investment
- ~75% reward potential on failure (missing clear bonus) feels fair
- Clear bonus creates meaningful victory incentive
- Immediate retry option reduces friction
- No "lives" system - each run is independent
- Failed runs still contribute to mastery investment, encouraging experimentation

#### Edge Cases

| Situation | Behavior |
|-----------|----------|
| Fail on wave 1 | Minimum 5 shards (1 wave × 5) + kills |
| Fail during boss wave | Boss kill counted if boss died before wall |
| Alt+F4 / disconnect | Run abandoned, no rewards (prevents save-scumming) |
| Pause then quit | Treated as failure at current wave |

---

## 4. Enemies (3 Types)

Enemies travel along paths toward the wall. Each type has unique behaviors that require different strategic responses.

| Enemy Type | Behavior Template |
|------------|-------------------|
| **Basic Enemy** | Standard enemies with steady speed. The foundation of most waves. |
| **Fast Enemy** | Quick-moving enemies with low health. Reaches the wall faster, requiring rapid response. |
| **Boss Enemy** | Powerful enemy with high health. Appears on waves marked as boss in stage files. |

### 4.1 Wall Attack Behavior

When enemies reach the wall, they **stop moving and begin attacking** the wall repeatedly until killed. This creates sustained pressure that compounds when multiple enemies reach the wall.

**Attack Timing:**
- Enemies do **not** deal damage immediately upon reaching the wall
- The first attack occurs after the initial cooldown period
- Subsequent attacks follow the same cooldown rhythm

**Enemy Wall Attack Stats:**

| Enemy Type | Wall Damage | Attack Cooldown | Effective DPS | Time to Destroy Wall (Solo) |
|------------|-------------|-----------------|---------------|----------------------------|
| **Basic Enemy** | 15 | 1.0s | 15 DPS | ~133 seconds |
| **Fast Enemy** | 10 | 0.8s | 12.5 DPS | ~160 seconds |
| **Boss Enemy** | 100 | 2.0s | 50 DPS | ~40 seconds |

**Compound Pressure Example:**
- 5 Basic Enemies at wall = 75 DPS → Wall destroyed in ~27 seconds
- 1 Boss + 3 Basic at wall = 95 DPS → Wall destroyed in ~21 seconds
- 4 Fast + 2 Basic at wall = 80 DPS → Wall destroyed in ~25 seconds

### 4.2 Enemy Design Guidelines

- Basic Enemies should be visually simple and immediately recognizable
- Fast Enemies should appear sleek and streamlined, conveying speed (e.g., motion trails)
- Boss Enemies should be visually imposing and larger than standard enemies
- Each enemy type should have a distinct **wall attack animation**
- Additional enemy types can be added to increase variety (see Appendix D for examples)

---

## 5. Towers

Towers are divided into **three distinct groups**: Basic Towers, Advanced Towers, and Special Towers. Each group serves a different strategic role and has unique placement rules.

**Tower Slot Layout:**
- 5 fixed tower slots at the bottom of the screen, positioned behind the wall
- **Middle slot (slot 3)**: Always occupied by the selected Basic Tower
- **Outer slots (slots 1, 2, 4, 5)**: Available for Advanced and Special Towers via the card system

### 5.1 Basic Towers

Basic Towers are the main damage-dealing towers that form the foundation of every run. Players select **one Basic Tower before starting a run**, and it is automatically placed in the **middle slot (slot 3)**.

| Rule | Description |
|------|-------------|
| Selection | Chosen before run begins (pre-run selection screen) |
| Placement | Always in the middle slot (slot 3) |
| Limit | Only one Basic Tower per run |
| Unlock | Must be unlocked with currency between runs (except starter) |

**MVP Basic Towers:**

| Tower | Attack Style | Special Mechanic | Archetype |
|-------|--------------|-----------------|-----------|
| **Base Tower** | Single-target projectiles | Balanced starter tower, fast and reliable | Balanced |
| **AOE Tower** | Lobbed explosives | Splash damage, hits clustered enemies | AOE/DOT |
| **Burst Tower** | Charged sniper shot | Slow fire rate, 3x single-target damage | Burst |
| **Piercing Tower** | Piercing rail shot | Projectile passes through all enemies in a line | Multi-Target |

The Base Tower is always unlocked. Other Basic Towers must be purchased with currency before they become selectable.

*For detailed stats (damage, fire rate, range, DPS), see Appendix B: Balance Guidelines.*

### 5.2 Advanced Towers

Advanced Towers are unlockable damage dealers that can be placed during a run via the card system. They provide specialized damage capabilities to complement the Basic Tower.

| Rule | Description |
|------|-------------|
| Selection | Placed via Place Tower cards during the run |
| Placement | Any available outer slot (1, 2, 4, or 5) |
| Limit | Only previously unlocked towers can appear in card selection |
| Unlock | Must be unlocked with currency between runs |

**MVP Advanced Towers:**

| Tower | Attack Style | Special Mechanic | Archetype |
|-------|--------------|-----------------|-----------|
| **Brute Force Node** | 3-shot burst | Consecutive hits on same target deal bonus damage | Sustained single-target |

*For detailed stats, see Appendix B: Balance Guidelines.*

#### Brute Force Node - Credential Stuffing

Fires 3 projectiles in rapid succession, then pauses to reload. Rewards landing consecutive hits on the same target.

**Special Mechanic:**

Consecutive hits on the **same target** gain stacking damage:

| Hit | Damage | Cumulative |
|-----|--------|------------|
| 1st | 18 (100%) | 18 |
| 2nd | 21 (+20%) | 39 |
| 3rd | 25 (+40%) | 64 |

Total if all 3 hit same target: 64 damage (vs 54 if spread).

Additional Advanced Towers may be added in future updates (see Appendix D).

### 5.3 Special Towers (Post-MVP)

Special Towers will focus on **crowd control and utility effects** rather than dealing damage. They will provide strategic options for controlling enemy movement and creating defensive setups.

| Rule | Description |
|------|-------------|
| Selection | Will be placed via Place Tower cards during the run |
| Placement | Any available outer slot (1, 2, 4, or 5) |
| Limit | Only previously unlocked towers will appear in card selection |
| Unlock | Will require currency to unlock between runs |

**Planned Special Tower Archetypes:**

| Archetype | Effect | Example Mechanics |
|-----------|--------|-------------------|
| **Barrier** | Blocks or redirects enemy movement | Force fields, walls, deflectors |
| **Trap** | Triggers effects when enemies enter area | Mines, snares, damage zones |
| **Teleport** | Moves enemies to different locations | Send enemies back, relocate to other lanes |
| **Debuff** | Applies negative effects without damage | Weaken, expose vulnerabilities, disable abilities |

**Planned Special Towers:**

| Tower | Effect Type | Special Mechanic |
|-------|-------------|------------------|
| **Barrier Tower** | Barrier | Creates temporary force field blocking enemy path |
| **Trap Tower** | Trap | Places mines that detonate when enemies pass |
| **Warp Tower** | Teleport | Teleports enemies back toward spawn point |
| **Disruptor Tower** | Debuff | Marks enemies to take increased damage from all sources |

Special Towers will be introduced in future content updates.

### 5.4 Tower Targeting Priority

All towers use the following targeting priority system to select enemies:

1. **Attacking Wall** (highest priority) - Enemies currently at the wall dealing damage are targeted first
2. **Closest to Wall** - Among non-attacking enemies, those closest to the wall are targeted
3. **Highest Health** - Among enemies at the same distance, prioritize those with the most remaining health
4. **First Spawned** - If multiple enemies share the same distance and health, target the one that spawned first

**Rationale:**
- Prioritizing enemies attacking the wall stops active damage immediately
- Targeting enemies closest to the wall prevents new attackers from joining
- Targeting high-health enemies ensures they're eliminated before reaching the wall
- This creates strategic depth: players must position towers to cover different paths and upgrade towers to handle high-health threats

**Special Cases:**
- **Boss Priority:** Within the same priority tier, Boss enemies are always targeted before Basic enemies. This ensures towers focus high-value targets when multiple enemies are equidistant.
- Towers with multi-shot or piercing abilities still follow this priority for their primary target
- Towers with area-of-effect attacks prioritize the center of enemy clusters using the same rules

**Design Note:** The fixed targeting system is intentionally simple. Per-tower targeting modes were considered but rejected to keep gameplay focused on tower placement and upgrade decisions rather than micro-management.

Additional tower examples and archetypes can be found in Appendix D. The MVP includes 4 unlockable towers representing distinct archetypes (AOE, Utility, Burst, Multi-Target).

### 5.5 Critical Hit System

All towers have a chance to deal critical hits, dealing bonus damage with enhanced visual feedback.

#### Base Critical Hit Values

| Property | Value |
|----------|-------|
| Base Crit Chance | 5% |
| Base Crit Multiplier | 1.5x damage |

These base values apply to all towers that do not have an overriding special ability.

#### Overclocked Special Ability (Base Tower)

The Base Tower at Mastery Level 5 gains the "Overclocked Processor" ability, which overrides the base crit values:

| Property | Value |
|----------|-------|
| Overclocked Crit Chance | 20% |
| Overclocked Crit Multiplier | 2.0x damage |

#### Critical Hit Calculation

1. Tower calculates base damage including all modifiers (upgrades, mastery bonuses)
2. RNG roll determines if critical hit occurs (based on crit chance)
3. If critical: `damage = floor(damage × critMultiplier)`
4. Critical flag is passed to the projectile and collision system

#### Visual Feedback

Critical hits are displayed with distinct styling:

| Property | Normal Hit | Critical Hit |
|----------|------------|--------------|
| Color | Yellow | Red (#ff4444) |
| Font Size | 14px | 22px |
| Font Weight | 700 | 900 |
| Text Suffix | None | "!" |
| Animation | Float up (0.8s) | Pop up with scale (1s) |
| Effects | Basic shadow | Dual-layer glow |

#### Known Issues & Limitations

See **Appendix F.5: Critical Hit System Issues** for documented limitations and gaps in the current implementation.

### 5.6 Status Effects

Towers and abilities can apply status effects to enemies. This section defines duration, stacking, and interaction rules.

#### Burn (Firewall Cascade - AOE Mastery 5)

| Property | Value |
|----------|-------|
| Ground Duration | 2 seconds |
| Tick Rate | Every 0.5 seconds (4 ticks total) |
| Damage per Tick | 25% of AOE Tower base damage |
| Total Burn Damage | 100% of tower damage over 2s |
| Stacking | None—overlapping zones refresh duration only |

Burning ground zones do not stack damage. An enemy standing in multiple burn zones takes damage as if in one zone, but the duration refreshes. This prevents AOE Tower from becoming dominant through zone overlap.

#### Breach (Network Breach - Piercing Mastery 5)

| Property | Value |
|----------|-------|
| Damage Amplification | +15% per stack |
| Max Stacks | 2 (+30% max) |
| Duration | 5 seconds (shared timer) |
| Reapplication | Adds stack (if below cap) and refreshes all stacks to 5s |

Breach marks stack up to 2 times, rewarding focus-fire strategies. Any hit from a Piercing Tower with Network Breach refreshes the duration of all existing stacks. Multiple Piercing Towers can quickly build and maintain max stacks on priority targets.

#### Status Effect Visuals

| Effect | Indicator | Color |
|--------|-----------|-------|
| Burn | 🔥 (flame) | Orange |
| Breach | ⚡ (lightning) | Green |

---

## 6. Card System

When score thresholds are reached, players are presented with cards to choose from. Cards allow players to place new towers or upgrade existing ones.

### 6.1 When Cards Appear

Card selection is triggered by reaching score thresholds during a run. The game pauses and waits until the player makes their selection before combat continues.

**Card Selection Thresholds:**

| Card # | Score Threshold | Typical Wave |
|--------|-----------------|--------------|
| 1 | 50 | ~2 |
| 2 | 120 | ~4 |
| 3 | 220 | ~6 |
| 4 | 350 | ~8 |
| 5 | 520 | ~11 |
| 6 | 730 | ~14 |
| 7 | 1000 | ~17 |
| 8 | 1350 | ~20 |

**Design Notes:**
- Front-loaded curve allows players to establish their build in early waves
- First 4 cards arrive by ~wave 8, enough to fill all tower slots
- Later cards reward efficient play and serve as upgrade opportunities
- Boss kills (100 points) create satisfying score spikes
- Game remains paused until player selects a card

### 6.2 Card Types

| Card Type | Effect |
|-----------|--------|
| Place Tower | Place an unlocked tower into an empty slot |
| Tower Upgrade | Buffs a specific tower already on the field |
| Wall Repair | Restores a portion of Wall HP (rare) |

**Wall Repair card rules:**

- Restores **30% of max Wall HP**
- Cannot exceed **max Wall HP**
- Does not grant any other buffs (pure survivability)
- Has a low drop chance

### 6.3 Tower Upgrade Options

Each tower can receive the following upgrades with multiple tiers. Upgrades are applied as tiers and replace previous tiers (they do not stack):

- **Tier 1** upgrades can be applied first
- **Tier 2** upgrades require Tier 1 to be applied first, and replace Tier 1 when applied
- When a higher tier upgrade is applied, it replaces the lower tier (e.g., applying Tier 2 Damage+ replaces Tier 1 Damage+, resulting in +50% damage, not +75%)
- The same upgrade type can be applied multiple times through card selection, progressing through tiers
- Each upgrade type maintains only one active tier at a time

| Upgrade | Tier 1 | Tier 2 |
|---------|--------|--------|
| Damage + | +25% damage | +50% damage |
| Fire Rate + | +25% attack speed | +50% attack speed |

### 6.4 Card Selection Rules

Players are presented with **3 cards** to choose from.

#### Card Generation

All 3 cards are randomly selected from the available pool:

- **Place Tower:** Available if empty slots exist AND unlocked towers haven't been placed
- **Tower Upgrade:** Available for placed towers with upgrades not at max tier
- **Wall Repair (15% chance):** If wall has taken damage, 15% chance to replace one card with Wall Repair. Restores 30% of max Wall HP

No duplicate cards (same tower + same upgrade type/tier) can appear in the same selection.

#### Card Pool Exhaustion

When the available card pool is limited:

| Situation | Behavior |
|-----------|----------|
| Fewer than 3 valid cards | Show only available cards (1 or 2) |
| No valid cards exist | Skip card selection, continue gameplay |

**No valid cards occurs when ALL conditions are true:**
- All 5 tower slots are filled
- All placed towers have max tier upgrades (Tier 2 Damage + Tier 2 Fire Rate)
- Wall is at full HP (Wall Repair ineligible)

This is an edge case that occurs only in near-perfect late-game runs.

### 6.5 Strategic Considerations

- **Battle Profile:** Configure a profile with the Basic Tower and gear that best suits the stage
- **Early run:** Balance between placing Advanced/Special Towers vs. upgrading the Basic Tower
- **Score optimization:** Efficient enemy kills unlock cards faster
- **Tower timing:** Decide whether to save card selections or use them immediately based on current threats
- **Late run:** Stack upgrades on key towers for maximum effectiveness against boss waves
- **Slot management:** Only 4 outer slots available for Advanced/Special Towers - choose wisely

---

## 7. Meta-Progression

Between runs, players spend currency to unlock new towers and permanently upgrade them. This provides long-term goals and allows players to develop personalized playstyles.

### 7.1 Progression Currency

**Data Shards** (Cybersecurity theme)

A single currency used for unlocks and permanent upgrades. Earned from run performance.

#### Currency Sources

| Source | Base Amount | Notes |
|--------|-------------|-------|
| Wave completed | 5 | Awarded per wave survived |
| Basic enemy killed | 1 | Bulk of income from kills |
| Fast enemy killed | 2 | Harder to hit, slightly more rewarding |
| Boss enemy killed | 15 | High-value targets |
| Stage clear bonus | 50 | Awarded only on victory |
| Perfect clear bonus | +25 | No wall damage taken (full run) |

#### Difficulty Multipliers

| Difficulty | Multiplier | Example (Full Clear, 200 kills) |
|------------|------------|--------------------------------|
| Normal | 1.0x | ~350 shards |
| Hard | 1.5x | ~525 shards |

#### Earning Examples

| Scenario | Calculation | Total |
|----------|-------------|-------|
| Stage 1-1 Normal, full clear, 180 kills | (20×5) + (180×1) + (4×15) + 50 | 390 |
| Stage 1-3 Hard, died wave 15, 250 kills | ((15×5) + (250×1) + (3×15)) × 1.5 | 487 |
| Stage 1-5 Normal, perfect clear, 300 kills | (20×5) + (300×1) + (5×15) + 50 + 25 | 550 |

**Design Notes:**
- First mastery level (75 shards) achievable in ~2 runs
- Full tower mastery (2325-4650 per tower) requires dedicated investment
- Hard mode rewards skill without being mandatory for progression
- Perfect clear bonus encourages defensive play without punishing aggression

#### Daily & Retention Bonuses

Optional bonuses that reward consistent play without punishing extended sessions.

| Bonus | Trigger | Reward |
|-------|---------|--------|
| **Daily Login** | First launch each day | +25 shards |
| **First Win of Day** | First stage clear per day | +50% shard bonus on that run |
| **Daily Challenge** | Complete a specific stage on Hard | +100 shards (rotates daily) |

**Streak System:**

| Consecutive Days | Streak Bonus |
|------------------|--------------|
| 2 days | +10% shards (all runs) |
| 5 days | +20% shards (all runs) |
| 7 days | +30% shards (all runs) |
| 14+ days | +50% shards (all runs) |

**Streak Rules:**
- Missing a day resets streak to 0
- Streak bonus applies to all shard earnings (kills, waves, bonuses)
- Streak counter displayed on main menu
- No punishment beyond losing the multiplier—progress is never lost

**Design Notes:**
- Rewards return without limiting play
- First-win bonus encourages completing at least one run per session
- Daily challenge adds variety and directs players to different stages
- Streak system creates long-term engagement without feeling mandatory

### 7.2 Tower Unlocks

Towers must be unlocked with currency before they become available. Each tower group has different unlock benefits:

| Tower Group | Unlock Benefit |
|-------------|----------------|
| **Basic Towers** | Unlocked towers become selectable in pre-run tower selection |
| **Advanced Towers** | Unlocked towers can appear in Place Tower cards during runs |
| **Special Towers** | Unlocked towers can appear in Place Tower cards during runs |

The Base Tower is always unlocked. All other towers must be purchased with currency.

**MVP Scope:**
- **Basic Towers (4):** Base Tower (always unlocked), AOE Tower, Burst Tower, Piercing Tower
- **Advanced Towers (1):** Brute Force Node
- **Special Towers:** None in MVP (post-launch content)

Additional towers may be added in future content updates (see Appendix D).

### 7.3 Tower Mastery

Each tower has its own mastery track. Spend currency to boost a specific tower's damage permanently, with a special ability unlocked at Level 5:

| Tower | Damage Bonus (Lv 1-5) | Costs (Lv 1-5) | Level 5 Special Ability |
|-------|----------------------|----------------|-------------------------|
| Base Tower | +10/20/30/40/50% | 75, 150, 300, 600, 1200 | Overclocked Processor: 20% chance for 2x damage |
| AOE Tower | +10/20/30/40/50% | 100, 200, 400, 800, 1600 | Firewall Cascade: Explosions leave burning ground (2s, 25% damage/tick) |
| Burst Tower | +10/20/30/40/50% | 125, 250, 500, 1000, 2000 | Precision Strike: +50% damage to enemies above 50% HP |
| Piercing Tower | +10/20/30/40/50% | 150, 300, 600, 1200, 2400 | Network Breach: Hits mark enemies for 5s, +15% damage taken |
| Brute Force Node | +10/20/30/40/50% | 100, 200, 400, 800, 1600 | Dictionary Attack: 4th shot added, +25% per consecutive hit |

### 7.4 Progression Notes

- Permanent upgrades apply to all future runs
- In-run card upgrades stack on top of permanent upgrades
- Number of unlockable towers and upgrade levels can be tuned for desired progression length
- Players naturally specialize in favorite towers over time

---

## 8. Stage & Wave Design

The game uses a chapter-based stage system with fixed, deterministic enemy spawns. This allows players to learn patterns and optimize their strategies. All stage data is defined in external stage files, making it easy to add new content.

**MVP Scope:** The game launches with **1 chapter** containing **5 stages** (1-1 through 1-5), for a total of **100 waves** (20 waves per stage). Additional chapters may be added in future content updates (see Section 8.1 for full chapter architecture).

### 8.1 Stage Architecture

**MVP Launch:** Chapter 1 only (5 stages, 100 waves total)
- Chapter 1: 1-1, 1-2, 1-3, 1-4, 1-5

**Full Design (Future Content):** Stages are organized into chapters, each containing 5 sub-stages with 20 waves each:

- Chapter 1: 1-1, 1-2, 1-3, 1-4, 1-5
- Chapter 2: 2-1, 2-2, 2-3, 2-4, 2-5
- Chapter 3: 3-1, 3-2, 3-3, 3-4, 3-5
- Additional chapters can be added as needed

### 8.2 Stage File Contents

Each sub-stage is defined in its own file containing all necessary data:

| Field | Description |
|-------|-------------|
| stageId | Unique identifier (e.g., "1-3") |
| stageName | Display name (e.g., "Payload Delivery") |
| waves | Array of 20 wave definitions |
| rewards | Currency awarded per difficulty (Normal/Hard) |

### 8.3 Wave Definition

Each wave within a stage file contains:

| Field | Description |
|-------|-------------|
| waveNumber | Wave number (1–20) |
| enemies | Array of enemy spawn definitions |
| isBoss | Boolean flag indicating if this is a boss wave |

### 8.4 Enemy Spawn Definition

Each enemy spawn within a wave is defined by:

| Field | Description |
|-------|-------------|
| enemyType | Type of enemy (Basic, Boss, etc.) |
| spawnX | X coordinate where enemy spawns (0.0–1.0 normalized) |
| spawnTime | Seconds after wave start when enemy appears |
| baseHP | Base hit points (modified by difficulty selection) |

### 8.5 Difficulty Selection

Before each run, players select a difficulty level that modifies enemy stats and rewards:

| Difficulty | HP Multiplier | Speed Multiplier | Reward Multiplier |
|------------|---------------|------------------|-------------------|
| Normal | 1.0x | 1.0x | 1.0x |
| Hard | 1.5x | 1.2x | 1.5x |

**Design Notes:**
- HP increases DPS requirements—enemies take longer to kill
- Speed reduces reaction time—enemies reach the wall faster
- Wave timing and enemy count remain identical across difficulties
- Combined difficulty is ~1.8x for 1.5x reward

### 8.6 Enemy Movement

Enemies spawn at the top of the screen at their designated X coordinate and travel straight down toward the wall. Each enemy follows its own vertical path based on where it spawned.

### 8.7 Example Stage File

Stage files use JSON format for easy editing and extensibility:

```json
{
  "stageId": "1-3",
  "stageName": "Payload Delivery",
  "rewards": { "normal": 50, "hard": 100 },
  "waves": [
    {
      "waveNumber": 1,
      "isBoss": false,
      "enemies": [
        { "enemyType": "Basic", "spawnX": 0.5, "spawnTime": 0, "baseHP": 100 },
        { "enemyType": "Basic", "spawnX": 0.3, "spawnTime": 1.5, "baseHP": 100 },
        { "enemyType": "Basic", "spawnX": 0.7, "spawnTime": 1.5, "baseHP": 100 }
      ]
    },
    {
      "waveNumber": 8,
      "isBoss": false,
      "enemies": [
        { "enemyType": "Fast", "spawnX": 0.2, "spawnTime": 0, "baseHP": 60 },
        { "enemyType": "Fast", "spawnX": 0.8, "spawnTime": 0, "baseHP": 60 },
        { "enemyType": "Basic", "spawnX": 0.5, "spawnTime": 1.0, "baseHP": 100 },
        { "enemyType": "Fast", "spawnX": 0.5, "spawnTime": 2.0, "baseHP": 60 }
      ]
    },
    {
      "waveNumber": 20,
      "isBoss": true,
      "enemies": [
        { "enemyType": "Boss", "spawnX": 0.5, "spawnTime": 0, "baseHP": 500 }
      ]
    }
  ]
}
```

### 8.8 Future: Random Mode

A future game mode will feature randomly generated enemy spawns for increased replayability. The fixed stage mode will remain available for players who prefer learning and mastering specific patterns.

### 8.9 Stage Unlock System

Stages unlock progressively within each chapter. Players must complete stages in order on any difficulty.

#### Unlock Requirements

| Stage | Requirement |
|-------|-------------|
| 1-1 | Always unlocked |
| 1-2 | Complete 1-1 (any difficulty) |
| 1-3 | Complete 1-2 (any difficulty) |
| 1-4 | Complete 1-3 (any difficulty) |
| 1-5 | Complete 1-4 (any difficulty) |

#### Difficulty Unlock

| Difficulty | Requirement |
|------------|-------------|
| Normal | Always available |
| Hard | Unlocked per-stage after Normal clear |

**Example:** Completing 1-3 on Normal unlocks both 1-4 (Normal) and 1-3 (Hard).

#### Chapter Progression (Future Content)

| Chapter | Requirement |
|---------|-------------|
| Chapter 1 | Always unlocked |
| Chapter 2 | Complete 1-5 (any difficulty) |
| Chapter 3 | Complete 2-5 (any difficulty) |

**Design Notes:**
- Linear unlock prevents overwhelming new players
- Hard mode as optional challenge, not progression gate
- Per-stage Hard unlock lets players challenge favorite stages
- No star ratings in MVP (simplifies UI, can add post-launch)

---

## 9. Gear System

Gear provides passive bonuses and triggered effects that modify gameplay. Players equip gear to Battle Profiles before starting a run. Each gear piece can be enhanced with Chips—small stat modules that slot into gear sockets.

### 9.1 Gear Slots

Each Battle Profile has **5 gear slots**:

| Slot | Name | Effect Category | Thematic Role |
|------|------|-----------------|---------------|
| 1 | **Firmware** | Wall HP, damage reduction, survival | Core system protection |
| 2 | **Protocol** | Tower damage, fire rate, crits | Attack routines |
| 3 | **Targeting** | Priority, range, multi-target | Threat detection |
| 4 | **Network** | Enemy debuffs, slows, marks | Traffic interception |
| 5 | **Utility** | Shards, cards, score, QoL | System optimization |

Players can leave slots empty. Empty slots provide no penalty.

### 9.2 Socket System

Gear pieces have **sockets** based on rarity. Sockets hold **Chips**—small modular bonuses.

| Rarity | Sockets | Base Power | Visual |
|--------|---------|------------|--------|
| Common | 0 | Low | White border |
| Uncommon | 1 | Medium | Green border |
| Rare | 2 | Medium-High | Blue border |
| Epic | 3 | High | Purple border, glow |
| Legendary | 4 | Very High + Drawback | Orange border, particle effect |

**Maximum potential: 5 slots × 4 sockets = 20 chips + 5 gear pieces = 25 total components**

### 9.3 Chip Types

Chips are universal—any chip fits any socket. This allows deep customization.

#### Offensive Chips

| Chip | Effect | Stack Cap | Icon |
|------|--------|-----------|------|
| **DMG Chip** | +4% tower damage | +20% | ⚔ |
| **SPD Chip** | +4% fire rate | +20% | ⚡ |
| **CRIT Chip** | +3% crit chance | +15% | ✦ |
| **CRIT-X Chip** | +0.15x crit multiplier | +0.6x | ✦✦ |
| **BURST Chip** | +8% damage to enemies above 50% HP | +40% | ▲ |
| **EXECUTE Chip** | +8% damage to enemies below 25% HP | +40% | ▼ |

#### Defensive Chips

| Chip | Effect | Stack Cap | Icon |
|------|--------|-----------|------|
| **HP Chip** | +5% wall HP | +30% | ♥ |
| **ARMOR Chip** | -3% wall damage taken | -15% | ◆ |
| **REGEN Chip** | +10 wall HP per wave | +50/wave | ♥+ |
| **EMERGENCY Chip** | +2% threshold for low HP warning (earlier warning) | +10% | ⚠ |

#### Economy Chips

| Chip | Effect | Stack Cap | Icon |
|------|--------|-----------|------|
| **SHARD Chip** | +5% shard gain | +30% | ◈ |
| **SCORE Chip** | +5% score gain | +30% | ★ |
| **THRESH Chip** | -3% card threshold requirement | -15% | ▽ |
| **BONUS Chip** | +10% stage clear bonus | +50% | ✚ |

#### Utility Chips

| Chip | Effect | Stack Cap | Icon |
|------|--------|-----------|------|
| **RANGE Chip** | +3% tower range | +15% | ◎ |
| **PROJECTILE Chip** | +5% projectile speed | +25% | → |
| **SPLASH Chip** | +5% AOE radius | +25% | ○ |
| **SLOW Chip** | +0.5s slow duration | +2.5s | ❄ |

### 9.4 Chip Acquisition

Chips are earned and purchased separately from gear:

| Source | Chip Reward |
|--------|-------------|
| Wave 10 reached | 1 random chip |
| Wave 20 cleared | 2 random chips |
| Boss killed | 15% chance for chip drop |
| Perfect clear | 1 guaranteed chip + 1 random |
| Hard mode clear | 2 guaranteed chips |
| Shop purchase | 50 shards per chip (random) |
| Shop purchase | 150 shards per chip (choose type) |

**Chip Inventory:** Players maintain a shared chip inventory. Chips equipped to gear are reserved but can be unslotted freely between runs.

### 9.5 Gear List

#### Firmware Slot (Defense)

| Gear | Rarity | Sockets | Effect |
|------|--------|---------|--------|
| **backup_protocol.sys** | Common | 0 | +100 Wall HP |
| **redundant_array.sys** | Common | 0 | +8% Wall HP |
| **error_correction.sys** | Uncommon | 1 | +12% Wall HP |
| **raid_cluster.sys** | Uncommon | 1 | +150 Wall HP, +5% damage reduction |
| **failsafe_kernel.sys** | Rare | 2 | +15% Wall HP, auto-heal 100 HP when first dropping below 30% |
| **hardened_runtime.sys** | Rare | 2 | +10% Wall HP, -10% damage from Boss enemies |
| **quantum_backup.sys** | Epic | 3 | +20% Wall HP, regenerate 25 HP per wave |
| **immortal_process.sys** | Epic | 3 | +25% Wall HP, survive one killing blow with 1 HP (once per run) |
| **root_persistence.sys** | Legendary | 4 | +35% Wall HP, -20% tower damage |

#### Protocol Slot (Offense)

| Gear | Rarity | Sockets | Effect |
|------|--------|---------|--------|
| **basic_attack.dll** | Common | 0 | +5% tower damage |
| **overclock.dll** | Common | 0 | +8% fire rate |
| **optimized_loop.dll** | Uncommon | 1 | +10% damage, +5% fire rate |
| **exploit_kit.dll** | Uncommon | 1 | +15% damage to Fast enemies |
| **zero_day.dll** | Rare | 2 | First hit on each enemy deals +30% damage |
| **privilege_escalation.dll** | Rare | 2 | +12% damage, kills grant +2% damage for 5s (stacks to +20%) |
| **apt_payload.dll** | Epic | 3 | +20% damage, enemies below 20% HP take 2x damage |
| **worm_propagation.dll** | Epic | 3 | Kills have 20% chance to deal 50% damage to nearest enemy |
| **chaos_monkey.dll** | Legendary | 4 | +40% damage, -15% fire rate |

#### Targeting Slot (Tower Behavior)

| Gear | Rarity | Sockets | Effect |
|------|--------|---------|--------|
| **basic_scan.exe** | Common | 0 | +5% tower range |
| **threat_detect.exe** | Common | 0 | Towers prioritize lowest HP (instead of highest) |
| **wide_scan.exe** | Uncommon | 1 | +10% tower range |
| **predictive_aim.exe** | Uncommon | 1 | +15% projectile speed, projectiles lead targets |
| **multi_thread.exe** | Rare | 2 | Towers can track 2 targets (splits damage 60/40) |
| **deep_scan.exe** | Rare | 2 | +15% range, +10% damage to enemies in outer 50% of range |
| **omniscient.exe** | Epic | 3 | +20% range, towers reveal enemy HP bars at spawn |
| **skynet.exe** | Epic | 3 | Towers prioritize enemies that would reach wall soonest |
| **tunnel_vision.exe** | Legendary | 4 | +50% damage to primary target, cannot switch targets until it dies or leaves range |

#### Network Slot (Enemy Debuffs)

| Gear | Rarity | Sockets | Effect |
|------|--------|---------|--------|
| **throttle.cfg** | Common | 0 | Enemies move 5% slower |
| **packet_loss.cfg** | Common | 0 | Enemies attack wall 10% slower |
| **rate_limit.cfg** | Uncommon | 1 | Enemies move 8% slower |
| **syn_flood.cfg** | Uncommon | 1 | Enemies pause 0.5s when first hit |
| **honeypot.cfg** | Rare | 2 | 20% chance enemies pause 1.5s on spawn |
| **mitm_attack.cfg** | Rare | 2 | Enemies take +10% damage while slowed |
| **blackhole_route.cfg** | Epic | 3 | Enemies move 12% slower, slowed enemies take +15% damage |
| **dns_poison.cfg** | Epic | 3 | First enemy each wave walks backward for 2s |
| **total_compromise.cfg** | Legendary | 4 | Enemies move 20% slower, -25% shard gain |

#### Utility Slot (Economy & QoL)

| Gear | Rarity | Sockets | Effect |
|------|--------|---------|--------|
| **data_miner.bat** | Common | 0 | +10% shards from kills |
| **score_logger.bat** | Common | 0 | +10% score gain |
| **crypto_miner.bat** | Uncommon | 1 | +15% shards from kills |
| **packet_sniffer.bat** | Uncommon | 1 | Card thresholds reduced by 10% |
| **efficiency_daemon.bat** | Rare | 2 | +20% shards, +10% score |
| **neural_cache.bat** | Rare | 2 | Start with 1 random card selection |
| **quantum_compute.bat** | Epic | 3 | +25% shards, card thresholds reduced by 15% |
| **prescience_engine.bat** | Epic | 3 | See next 3 cards in selection pool, pick order |
| **greed_protocol.bat** | Legendary | 4 | +50% shards, Wall HP capped at 75% |

### 9.6 Legendary Gear

Legendary gear is powerful but comes with significant drawbacks:

| Gear | Benefit | Drawback |
|------|---------|----------|
| **root_persistence.sys** | +35% Wall HP, 4 sockets | -20% tower damage |
| **chaos_monkey.dll** | +40% damage, 4 sockets | -15% fire rate |
| **tunnel_vision.exe** | +50% single-target damage, 4 sockets | Cannot switch targets |
| **total_compromise.cfg** | 20% slow + damage amp, 4 sockets | -25% shard gain |
| **greed_protocol.bat** | +50% shards, 4 sockets | Wall HP capped at 75% |

**Legendary Acquisition:**
- Complete all Chapter 1 stages on Hard: 1 Legendary unlock
- Perfect clear Stage 1-5 on Hard: 1 Legendary unlock
- Achievement milestones: Specific Legendary unlocks
- Cannot be purchased with shards

### 9.7 Gear Unlock Costs

| Rarity | Shard Cost | Primary Source |
|--------|------------|----------------|
| Common | Free | Unlocked by default |
| Uncommon | 150 | Stage clears |
| Rare | 400 | Hard mode clears |
| Epic | 800 | Achievements, late-game |
| Legendary | N/A | Achievements only |

**Starting Unlocks:** Players begin with all Common gear and 10 random chips.

### 9.8 Build Examples

#### "Glass Cannon" - Maximum Damage

| Slot | Gear | Chips |
|------|------|-------|
| Firmware | backup_protocol.sys | — |
| Protocol | apt_payload.dll | DMG, DMG, CRIT |
| Targeting | deep_scan.exe | DMG, BURST |
| Network | mitm_attack.cfg | DMG, EXECUTE |
| Utility | neural_cache.bat | DMG, SPD |

**Profile:** High risk, high reward. Minimal defense, maximum kill speed.

#### "Immortal Fortress" - Survival Focus

| Slot | Gear | Chips |
|------|------|-------|
| Firmware | immortal_process.sys | HP, HP, REGEN |
| Protocol | optimized_loop.dll | ARMOR |
| Targeting | skynet.exe | HP, ARMOR, REGEN |
| Network | blackhole_route.cfg | ARMOR, HP, SLOW |
| Utility | efficiency_daemon.bat | HP, REGEN |

**Profile:** Survive anything. Kill speed is secondary to wall preservation.

#### "Shard Farmer" - Economy Run

| Slot | Gear | Chips |
|------|------|-------|
| Firmware | error_correction.sys | SHARD |
| Protocol | overclock.dll | — |
| Targeting | wide_scan.exe | SHARD |
| Network | rate_limit.cfg | BONUS |
| Utility | quantum_compute.bat | SHARD, SHARD, BONUS |

**Profile:** Maximize currency gain on easier stages for faster progression.

#### "Crit Machine" - Burst Build

| Slot | Gear | Chips |
|------|------|-------|
| Firmware | hardened_runtime.sys | CRIT, CRIT |
| Protocol | zero_day.dll | CRIT, CRIT-X, CRIT-X |
| Targeting | omniscient.exe | CRIT, CRIT, CRIT-X |
| Network | syn_flood.cfg | CRIT |
| Utility | neural_cache.bat | CRIT, CRIT |

**Profile:** Stack crit to cap (+15%), boost multiplier. Pairs well with Base Tower Mastery 5 (Overclocked Processor).

**Final Crit Stats:** 5% base + 15% chips + 20% mastery = 40% crit chance, 2.0x + 0.45x = 2.45x multiplier

#### "Speedrun" - Fast Clear

| Slot | Gear | Chips |
|------|------|-------|
| Firmware | failsafe_kernel.sys | SPD, SPD |
| Protocol | worm_propagation.dll | SPD, SPD, EXECUTE |
| Targeting | skynet.exe | SPD, PROJECTILE, PROJECTILE |
| Network | dns_poison.cfg | SPD, SPD, SPD |
| Utility | prescience_engine.bat | SPD, THRESH, THRESH |

**Profile:** Maximize fire rate, fast card acquisition, chain kills.

### 9.9 UI Integration

#### Profile Editor

```
╔══════════════════════════════════════════════════════════════╗
║  BATTLE PROFILE: Crit Machine                    [SAVE] [X]  ║
╠══════════════════════════════════════════════════════════════╣
║  Basic Tower: [Antivirus Turret]                             ║
╠══════════════════════════════════════════════════════════════╣
║                                                              ║
║  FIRMWARE ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░  ║
║  ┌─────────────────────────────────────┐                     ║
║  │ ◆◆ hardened_runtime.sys             │  [✦][✦][ ][ ]      ║
║  │ +10% Wall HP, -10% Boss damage      │   CRIT CRIT         ║
║  └─────────────────────────────────────┘                     ║
║                                                              ║
║  PROTOCOL ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░  ║
║  ┌─────────────────────────────────────┐                     ║
║  │ ◆◆ zero_day.dll                     │  [✦][✦✦][✦✦][ ]    ║
║  │ +30% first hit damage               │   CRIT CRITX CRITX  ║
║  └─────────────────────────────────────┘                     ║
║                                                              ║
║  TARGETING ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░  ║
║  ┌─────────────────────────────────────┐                     ║
║  │ ◆◆◆ omniscient.exe                  │  [✦][✦][✦✦]        ║
║  │ +20% range, reveal HP at spawn      │   CRIT CRIT CRITX   ║
║  └─────────────────────────────────────┘                     ║
║                                                              ║
║  NETWORK ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░  ║
║  ┌─────────────────────────────────────┐                     ║
║  │ ◆ syn_flood.cfg                     │  [✦]               ║
║  │ Enemies pause 0.5s on first hit     │   CRIT              ║
║  └─────────────────────────────────────┘                     ║
║                                                              ║
║  UTILITY ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░  ║
║  ┌─────────────────────────────────────┐                     ║
║  │ ◆◆ neural_cache.bat                 │  [✦][✦]            ║
║  │ Start with 1 card selection         │   CRIT CRIT         ║
║  └─────────────────────────────────────┘                     ║
║                                                              ║
╠══════════════════════════════════════════════════════════════╣
║  SUMMARY                                                     ║
║  ────────────────────────────────────                        ║
║  Crit Chance: +15%  │  Crit Multi: +0.45x  │  Wall HP: +10%  ║
║  Damage: +0%        │  Fire Rate: +0%      │  Range: +20%    ║
╚══════════════════════════════════════════════════════════════╝
```

#### Chip Inventory

```
╔═══════════════════════════════════════════════════╗
║  CHIP INVENTORY                        [SORT ▼]   ║
╠═══════════════════════════════════════════════════╣
║                                                   ║
║  OFFENSIVE                                        ║
║  [⚔ DMG ×8] [⚡ SPD ×6] [✦ CRIT ×12] [✦✦ CRITX ×4]║
║  [▲ BURST ×3] [▼ EXECUTE ×2]                      ║
║                                                   ║
║  DEFENSIVE                                        ║
║  [♥ HP ×7] [◆ ARMOR ×4] [♥+ REGEN ×3] [⚠ EMERG ×1]║
║                                                   ║
║  ECONOMY                                          ║
║  [◈ SHARD ×5] [★ SCORE ×3] [▽ THRESH ×2] [✚ BONUS×2]║
║                                                   ║
║  UTILITY                                          ║
║  [◎ RANGE ×4] [→ PROJ ×3] [○ SPLASH ×2] [❄ SLOW ×2]║
║                                                   ║
║  ────────────────────────────────────────         ║
║  Equipped: 14/47  │  Free: 33                     ║
╚═══════════════════════════════════════════════════╝
```

#### In-Run HUD

Active gear displayed as icons in corner:

```
┌──────────────────┐
│ ⚙ ⚙ ⚙ ⚙ ⚙      │  ← 5 gear icons
│ [✦×10][⚔×3]     │  ← chip summary (hover to expand)
└──────────────────┘
```

Triggered effects show notifications:

```
╔════════════════════════════╗
║ ⚡ FAILSAFE ACTIVATED      ║
║ +100 HP restored           ║
╚════════════════════════════╝
```

### 9.10 Socket Management

| Action | Behavior |
|--------|----------|
| Slot chip | Drag chip to empty socket, chip moves from inventory to gear |
| Unslot chip | Click socketed chip to return to inventory |
| Swap chip | Drag new chip onto occupied socket, old chip returns to inventory |
| Quick-fill | Right-click gear to auto-fill sockets with best available chips of matching category |
| Clear all | Button to unslot all chips from a gear piece |

**Restrictions:**
- Chips can only be managed between runs
- No chip management during run or card selection
- Chips are not consumed—unlimited reuse

### 9.11 Balance Considerations

#### Stack Caps Prevent Abuse

All chip bonuses have hard caps to prevent degenerate builds:

| Stat | Cap | Max Chips Needed |
|------|-----|------------------|
| Damage | +20% | 5 DMG chips |
| Fire Rate | +20% | 5 SPD chips |
| Crit Chance | +15% | 5 CRIT chips |
| Crit Multiplier | +0.6x | 4 CRIT-X chips |
| Wall HP | +30% | 6 HP chips |
| Damage Reduction | -15% | 5 ARMOR chips |
| Shard Gain | +30% | 6 SHARD chips |

#### Power Budget by Rarity

| Rarity | Gear Power | Socket Value | Total Budget |
|--------|------------|--------------|--------------|
| Common | ~5-8% | 0 | ~5-8% |
| Uncommon | ~10-15% | +1 chip (~4%) | ~14-19% |
| Rare | ~15-20% + trigger | +2 chips (~8%) | ~23-28% |
| Epic | ~20-25% + trigger | +3 chips (~12%) | ~32-37% |
| Legendary | ~35-50% + drawback | +4 chips (~16%) | ~35-50% net |

#### Chip Value Calibration

Each chip is balanced to ~4% of a "power point":
- 5 chips of one type ≈ 20% bonus ≈ equivalent to Tier 2 card upgrade
- Full socket loadout (20 chips) ≈ 80% total bonus spread across stats
- No single stat can be pushed to game-breaking levels

### 9.12 Progression Integration

#### Recommended Unlock Order (New Players)

| Priority | Unlock | Reason |
|----------|--------|--------|
| 1 | error_correction.sys | Uncommon, survivability |
| 2 | optimized_loop.dll | Uncommon, damage + speed |
| 3 | 10-15 mixed chips | Build flexibility |
| 4 | First Rare gear | Significant power jump |
| 5 | Specialize in build path | Commit to playstyle |

#### Chip Farming Efficiency

| Stage | Shards/Run | Chips/Run | Best For |
|-------|------------|-----------|----------|
| 1-1 Normal | ~200 | 1-2 | Quick chip drops |
| 1-3 Hard | ~400 | 3-4 | Balanced farming |
| 1-5 Hard Perfect | ~600 | 5-6 | Maximum efficiency |

### 9.13 Future Expansion Hooks

| Feature | Description |
|---------|-------------|
| **Chip Crafting** | Combine 3 chips of same type → 1 chip of next tier |
| **Chip Tiers** | Bronze/Silver/Gold chips with 4%/6%/8% values |
| **Set Chips** | Matching chip sets grant bonus (3× DMG = +5% bonus damage) |
| **Gear Upgrades** | Spend shards to add sockets to lower-rarity gear |
| **Cursed Chips** | Powerful chips with drawbacks (-5% HP, +8% damage) |
| **Chip Fusion** | Combine two different chips into hybrid (DMG+CRIT → 2% each) |

---

## Appendix A: Cybersecurity Theme Reference

Complete mapping of game elements to cybersecurity-themed names.

### Entity Names

| Game Element | Themed Name |
|--------------|-------------|
| Wall | Firewall |
| Progression Currency | Data Shards |
| Basic Enemy | Virus |
| Fast Enemy | Worm |
| Boss Enemy | Ransomware |

### Tower Names

| Tower ID | Display Name |
|----------|--------------|
| Base Tower | Antivirus Turret |
| AOE Tower | Logic Bomb |
| Burst Tower | Zero-Day Striker |
| Piercing Tower | Traceroute Cannon |
| Brute Force Node | Brute Force Node |

### Stage/Chapter Names

| Stage | Name |
|-------|------|
| Chapter 1 | Network Perimeter |
| 1-1 | Entry Point |
| 1-2 | Packet Storm |
| 1-3 | Payload Delivery |
| 1-4 | Privilege Escalation |
| 1-5 | Root Access |

---

## Appendix B: Balance Guidelines

### Tower Stats Reference

This is the authoritative source for all tower statistics. For tower mechanics and special abilities, see Section 5.

Towers have distinct stat profiles while maintaining similar overall DPS. Basic towers have full range (0.9), Advanced towers have moderate range.

#### Basic Towers

| Tower | Damage | Fire Rate | Range | DPS | Unlock Cost |
|-------|--------|-----------|-------|-----|-------------|
| Base Tower | 50 | 1.0/s | 0.9 | 50 | Always unlocked |
| AOE Tower | 40 | 1.2/s | 0.9 | 48 | 200 shards |
| Burst Tower | 150 | 0.33/s | 0.9 | 50 | 250 shards |
| Piercing Tower | 50 | 1.0/s | 0.9 | 50 | 300 shards |

#### Advanced Towers

| Tower | Damage | Fire Rate | Range | DPS | Unlock Cost |
|-------|--------|-----------|-------|-----|-------------|
| Brute Force Node | 18×3 | 0.83 bursts/s | 0.85 | 45 | 400 shards |

**Brute Force Node Details:**

| Property | Value |
|----------|-------|
| Burst Count | 3 shots |
| Burst Interval | 0.1s (fires in ~0.3s) |
| Reload Time | 1.2s between bursts |
| Projectile Speed | 2.0 units/s |
| Credential Stuffing | +20%/+40% damage on consecutive hits (64 total vs 54 base) |

### Tower DPS with Upgrades

| Upgrade State | DPS Multiplier | Example (Base Tower) |
|---------------|----------------|----------------------|
| No upgrades | 1.0x | 50 DPS |
| Tier 1 Damage OR Fire Rate | 1.25x | 62.5 DPS |
| Tier 2 Damage OR Fire Rate | 1.5x | 75 DPS |
| Tier 2 Damage AND Fire Rate | 2.25x | 112.5 DPS |

### Enemy HP Scaling

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

### DPS vs HP Balance Check

Time to kill with 5 fully upgraded towers (~112.5 DPS each = 562.5 DPS total):
- Wave 20 Basic (Hard): 435 HP / 562.5 DPS = **0.77 seconds**
- Wave 20 Fast (Hard): 261 HP / 562.5 DPS = **0.46 seconds**
- Wave 20 Boss (Hard): 2175 HP / 562.5 DPS = **3.9 seconds**

### Wave Difficulty Curve

| Wave Range | Enemy HP | Enemy Count | Composition |
|------------|----------|-------------|-------------|
| 1-5 | Low | Few | Basic only |
| 6-10 | Medium | Moderate | Basic + Fast mixed |
| 11-15 | High | Many | Basic + Fast mixed |
| 16-19 | Very High | Many | Basic + Fast mixed |
| 20 | Boss | 1 + support | Boss + Basic + Fast |

---

## Appendix C: Technical Specifications

### Screen Layout

```
+----------------------------------+
| SCORE: [value]   NEXT CARD: [value] |
+----------------------------------+
|                                  |
|          ENEMY SPAWN AREA        |
|              (top)               |
|                                  |
|                                  |
|          PLAY FIELD              |
|        (enemies travel           |
|         downward here)           |
|                                  |
|                                  |
+----------------------------------+
|              WALL                |
+----------------------------------+
| [T1] [T2] [T3] [T4] [T5]        |
|       TOWER SLOTS                |
+----------------------------------+
```

### Data Structures

**Tower Definition:**
```json
{
  "towerId": "base_tower",
  "displayName": "Antivirus Turret",
  "attackStyle": "single_target",
  "baseDamage": 50,
  "baseFireRate": 1.0,
  "baseRange": 0.9,
  "specialMechanic": null,
  "unlockCost": 0
}
```

*Note: Values match Appendix B balance tables. Range uses normalized coordinates (0-1).*

**Enemy Definitions:**
```json
{
  "enemyId": "basic",
  "displayName": "Virus",
  "baseHP": 100,
  "speed": 0.1,
  "firewallDamage": 15,
  "firewallAttackCooldown": 1.0,
  "behavior": "direct",
  "specialAbility": null
}
```

```json
{
  "enemyId": "fast",
  "displayName": "Worm",
  "baseHP": 60,
  "speed": 0.16,
  "firewallDamage": 10,
  "firewallAttackCooldown": 0.8,
  "behavior": "direct",
  "specialAbility": null
}
```

*Note: Speed uses normalized units per second. See Enemy Speed table below.*

### Enemy Speed

Speed determines how quickly enemies travel from spawn to wall. Values use normalized coordinates where the play area spans 0.0 (top) to ~0.8 (wall position).

| Enemy Type | Speed (units/s) | Time to Wall | Hard Mode (1.2x) |
|------------|-----------------|--------------|------------------|
| Basic | 0.1 | ~8 seconds | ~6.7 seconds |
| Fast | 0.16 | ~5 seconds | ~4.2 seconds |
| Boss | 0.06 | ~13 seconds | ~11 seconds |

**Design Notes:**
- Basic enemies give towers ~8 seconds of firing time at Normal difficulty
- Fast enemies give only ~5 seconds, requiring quick targeting or they slip through
- Bosses are slower, giving more time to deal with their high HP
- Hard mode's 1.2x speed multiplier reduces reaction time by ~17%

### Projectile Mechanics

Towers fire different projectile types with varying travel behavior.

| Projectile Type | Tower | Speed | Behavior |
|-----------------|-------|-------|----------|
| Basic | Base Tower | 1.5 units/s | Single target, disappears on hit |
| AOE | AOE Tower | 1.0 units/s | Explodes on impact, deals splash |
| Burst | Burst Tower | 2.0 units/s | Single target, high damage |
| Rail | Piercing Tower | Instant | Hitscan, pierces all enemies in line |
| Rapid | Brute Force Node | 2.0 units/s | 3-shot burst, consecutive hit bonus |

**Hitscan vs Travel:**
- Instant (hitscan) projectiles cannot miss—damage applies immediately on fire
- Traveling projectiles can miss if the enemy dies or moves before impact

### Splash Damage (AOE Tower)

| Property | Value |
|----------|-------|
| Splash Radius | 0.12 normalized units |
| Center Damage | 100% tower damage |
| Edge Damage | 50% tower damage |
| Falloff | Linear from center to edge |
| Max Targets | Unlimited (all enemies in radius) |

**Design Notes:**
- Splash radius is roughly 15% of screen width
- Enemies at the exact impact point take full damage
- Damage decreases linearly—enemies at the edge take half damage

---

## Appendix D: Future Content Examples

This appendix contains ideas for future content updates. For current MVP towers, see Section 5 (mechanics) and Appendix B (stats).

### Future Tower Ideas

The following towers are reserved for future content updates and expansions:

**Future Basic Towers:**

| Tower ID | Attack Style | Special Mechanic | Archetype |
|----------|--------------|------------------|-----------|
| Flame Tower | Flame/burst, short-medium range | Burns enemies over time (DOT) | DOT/Area |
| Shotgun Tower | Shotgun spread | High burst damage at close range | Burst |
| Sniper Tower | Instant hitscan beam | Very long range, low damage, fast targeting | Range |
| Critical Tower | Critical hit focus | Normal damage, but 20% chance for 5x crit | Burst |
| Nova Tower | Charged AOE blast | Slow charge-up, then massive damage in wide radius | AOE/Burst |

**Future Advanced Towers:**

| Tower ID | Attack Style | Special Mechanic | Archetype |
|----------|--------------|------------------|-----------|
| Throttle Node | Slowing beam attack | Deals damage AND slows enemies by 50% | Utility/Damage |
| Scanner Tower | Continuous beam | Reveals hidden enemies, steady damage | Utility/Range |
| Spray Tower | Rapid-fire weak projectiles | Sprays the whole path, good for groups | Multi-Target |
| Drone Tower | Deployable units | Units seek targets, return to reload | Special |

**Future Special Towers:**

| Tower ID | Effect Type | Special Mechanic |
|----------|-------------|------------------|
| Stasis Tower | Trap | Traps enemy in place for 2 seconds |
| Mind Tower | Debuff | Chance to turn enemies against others briefly |
| Slow Field Tower | Debuff | Creates area that slows all enemies passing through |

### Future: Damage Type System

The MVP uses untyped damage for simplicity. Post-launch updates will introduce a damage type system to increase strategic depth when more enemy types are added.

#### Planned Damage Types

| Type | Theme | Visual Color | Status Synergy |
|------|-------|--------------|----------------|
| **Signature** | Pattern matching, known threat detection | Green | Breach (flag for deletion) |
| **Heuristic** | Behavioral analysis, anomaly detection | Orange | Burn (corruption spread) |
| **Encryption** | Cryptographic attacks, key breaking | Blue/cyan | Slow (decryption overhead) |
| **Protocol** | Network-layer attacks, packet injection | Purple | Stun (connection interrupt) |

#### Tower Type Assignments

| Tower | Damage Type | Rationale |
|-------|-------------|-----------|
| Antivirus Turret (Base) | Signature | Classic signature-based detection |
| Firewall Cannon (AOE) | Heuristic | Broad pattern analysis, catches clusters |
| Intrusion Detector (Burst) | Signature | Deep scan, high-confidence single target |
| Network Railgun (Piercing) | Protocol | Packet-level attack through multiple hosts |
| Malware Scanner (Utility) | Encryption | Decrypt and slow malicious processes |

#### Enemy Resistance Framework

When new enemy types are introduced, they will have type affinities:

| Affinity | Damage Modifier |
|----------|-----------------|
| Immune | 0x (no damage) |
| Resistant | 0.5x |
| Neutral | 1.0x |
| Vulnerable | 1.5x |
| Critical Weakness | 2.0x |

#### Planned Enemy Type Examples

| Enemy | Signature | Heuristic | Encryption | Protocol |
|-------|-----------|-----------|------------|----------|
| Virus (Basic) | Neutral | Neutral | Neutral | Neutral |
| Worm (Fast) | Neutral | Neutral | Neutral | Neutral |
| Ransomware (Boss) | Neutral | Neutral | Neutral | Neutral |
| Polymorphic Virus (future) | Resistant | Vulnerable | Neutral | Neutral |
| Encrypted Payload (future) | Neutral | Resistant | Critical Weakness | Neutral |
| Botnet Node (future) | Neutral | Neutral | Resistant | Critical Weakness |
| Zero-Day Exploit (future) | Immune | Vulnerable | Neutral | Vulnerable |

#### Implementation Notes

- Type system activates when Chapter 2 introduces new enemy types
- MVP enemies (Virus, Worm, Ransomware) remain type-neutral for backwards compatibility
- UI must show enemy resistances on hover/inspection
- Tower selection screen should display damage type iconography
- Mastery abilities retain their effects independent of damage type

#### Design Goals

1. **Rock-paper-scissors depth** without hard counters
2. **No mandatory types**—any tower can contribute, typed towers excel
3. **Stage theming**—certain stages favor certain types (e.g., encryption-heavy datacenter)
4. **Build diversity**—discourage mono-type tower setups via enemy mix

### Tower Group Guidelines

When designing new towers, assign them to groups based on their role:

| Group | Role | Characteristics |
|-------|------|-----------------|
| **Basic** | Primary damage dealer | High DPS, defines run strategy, selected pre-run |
| **Advanced** | Supplementary damage | Complements Basic Tower, moderate DPS with utility |
| **Special** | Crowd control / Utility | No direct damage, focuses on enemy manipulation |

### Tower Archetypes

When designing towers, consider these archetypes to ensure gameplay variety:

| Archetype | Role | Example Mechanics | DPS Range |
|-----------|------|-------------------|-----------|
| Balanced | Reliable all-rounder | Consistent damage, no special effects | 100% baseline |
| DOT/Area | Sustained and splash damage | Burn, poison, explosions | 80-120% over time |
| Utility | Reveal, slow, trap | Crowd control effects | 60-80% + effect |
| Burst | High single-target damage | Sniper shots, critical hits | 150-200% peak |
| Multi-Target | Handle many enemies at once | Piercing, rapid-fire, spread | 50% per hit, high rate |
| Range | Long-distance coverage | Extended range, fast targeting | 70-90% |
| Special | Unique mechanics | Mind control, deployable units | Varies |

### Example Additional Enemies

The following are example enemy types that can be added beyond Basic and Boss:

| Enemy Type | Behavior |
|------------|----------|
| Splitting Enemy | Split into smaller copies when damaged, creating more targets |
| Disguised Enemy | Appear harmless until close to the wall, then reveal true nature |
| Swarm Enemy | Huge waves of weak units designed to overwhelm defenses |
| Invisible Enemy | Cannot be targeted until revealed by a detection tower |
| Disabling Enemy | Attempts to disable towers, temporarily shutting them down |

---

## Appendix E: Original Tower Upgrade Options (Archived)

The following upgrade options were part of the original design but have been simplified for the core implementation. These may be reintroduced in future updates or expansions.

### Original Upgrade Tier System

Each tower could receive the following upgrades with multiple tiers. Upgrades were applied as tiers and replaced previous tiers (they did not stack):

- **Tier 1** upgrades could be applied first
- **Tier 2** upgrades required Tier 1 to be applied first, and replaced Tier 1 when applied
- **Tier 3** upgrades (where available) required Tier 2 to be applied first, and replaced Tier 2 when applied
- When a higher tier upgrade was applied, it replaced the lower tier (e.g., applying Tier 2 Damage+ replaced Tier 1 Damage+, resulting in +40% damage, not +60%)
- The same upgrade type could be applied multiple times through card selection, progressing through tiers
- Each upgrade type maintained only one active tier at a time

### Original Upgrade Table

| Upgrade | Tier 1 | Tier 2 | Tier 3 |
|---------|--------|--------|--------|
| Damage + | +20% damage | +40% damage | +60% damage |
| Fire Rate + | +15% attack speed | +30% attack speed | +50% attack speed |
| Range + | +20% range | +40% range | — |
| Cooldown - | -15% cooldown | -25% cooldown | — |
| Crit Chance | +10% crit chance | +20% crit chance | — | *(Not implemented; see Section 5.5)*
| Multi-shot | +1 extra target | +2 extra targets | — |
| Piercing | Shots pierce 1 enemy | Shots pierce all enemies | — |
| Slow Effect | 10% slow on hit | 20% slow on hit | — |
| Effect Duration + | +25% duration | +50% duration | — |

---

## Appendix F: Known Design Issues & Gaps

This appendix documents known design limitations and gaps that may require future attention. Issues are organized into **Active** (unresolved) and **Resolved** sections.

---

### Active Issues

#### F.5 Critical Hit System Issues

The following issues affect the critical hit system (Section 5.5):

| ID | Issue | Impact | Suggested Fix |
|----|-------|--------|---------------|
| F.5.1 | **Undocumented Base Crit** - All towers have a hidden 5% crit chance not displayed in tooltips or mentioned in-game | Players unaware of mechanic | Add crit stats to tower tooltips |
| F.5.2 | **No Crit Upgrades** - Appendix E lists "Crit Chance" (+10%/+20%) but never implemented in card system | Lost upgrade variety | Implement crit cards or remove from Appendix E |
| F.5.3 | **No Tooltip Display** - Crit chance/multiplier never shown, even for Overclocked ability | Player confusion | Update tooltip format in Section 3.3 |
| F.5.4 | **Only Base Tower Has Crit Mastery** - Other mastery abilities don't affect crits | Asymmetric design | Intentional—document as design choice |
| F.5.5 | **Splash Crit Inheritance** - AOE crits show "!" on all splash hits despite falloff damage | Misleading feedback | Only show "!" on center hit, or show reduced crit numbers |
| F.5.6 | **Two-Tier Crit Gap** - Large jump from 5% to 20% crit with no intermediate values | Abrupt power spike at Mastery 5 | Add intermediate crit bonuses or smooth curve |
| F.5.7 | **Rounding Behavior** - `Math.floor()` slightly reduces crit damage on odd values (49 × 1.5 = 73) | Minor damage loss | Use `Math.round()` or document as intentional |

---

### Resolved Issues

#### ~~F.1 Targeting Priority Creates Strategic Limitations~~ *(Addressed)*

~~The fixed targeting priority system removes tactical decision-making during gameplay.~~

*Resolution: The fixed system is an intentional design choice to keep gameplay focused on tower placement and upgrades rather than micro-management. Boss priority rule added to ensure high-value targets are focused. See Section 5.4.*

**Final Priority Order:**
1. Attacking Wall (highest)
2. Boss enemies (within same distance tier)
3. Closest to Wall
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

*Resolution: Per-wave scaling increased from 8% to 10%. See Appendix B.*

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

---

## Appendix G: Alternative Theme Options

The game's core mechanics are theme-agnostic. Future content updates or reskins could adapt the cybersecurity theme to alternative settings. Below are potential theme mappings for reference.

### Theme Comparison Matrix

| Element | Cybersecurity | Fantasy | Military | Space | Nature |
|---------|---------------|---------|----------|-------|--------|
| **Setting** | Data center | Castle kingdom | Military base | Space station | Forest grove |
| **Wall** | Firewall | Castle Wall | Barricade | Energy Shield | Root Barrier |
| **Currency** | Data Shards | Gold Coins | Supply Points | Stardust | Life Essence |
| **Basic Enemy** | Virus | Goblin | Infantry | Drone | Beetle |
| **Boss Enemy** | Ransomware | Dragon | Tank | Mothership | Apex Predator |

### Tower Name Mappings

| Tower Type | Cybersecurity | Fantasy | Military | Space | Nature |
|------------|---------------|---------|----------|-------|--------|
| Base Tower | Antivirus Turret | Arrow Tower | Machine Gun | Laser Turret | Thorn Launcher |
| AOE Tower | Logic Bomb | Catapult | Mortar | Plasma Cannon | Spore Spreader |
| Burst Tower | Zero-Day Striker | Ballista | Sniper Nest | Rail Gun | Venom Striker |
| Piercing Tower | Traceroute Cannon | Lightning Rod | Gauss Rifle | Ion Beam | Root Spike |
| Brute Force Node | Brute Force Node | Siege Engine | Minigun | Swarm Launcher | Hive Mind |

### Implementation Notes

- Theme changes are cosmetic only—mechanics remain identical
- Each theme requires: sprites, sound effects, UI reskin, background art
- Estimated effort: 2-4 weeks per theme depending on asset complexity
- Consider theme packs as potential DLC or unlockable content

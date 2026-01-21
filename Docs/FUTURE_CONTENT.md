# Future Content Examples

> This content was previously Appendix D of the [Game Design Document](GDD.md).

This document contains ideas for future content updates. For current MVP towers, see Section 5 (mechanics) and Appendix B (stats) in the GDD.

## Future Tower Ideas

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

**Future Deployables:**

| Deployable | Effect Type | Special Mechanic |
|------------|-------------|------------------|
| Stasis Trap | Trap | Traps enemy in place for 2 seconds |
| Mind Disruptor | Debuff | Chance to turn enemies against others briefly |
| Slow Field | Debuff | Creates area that slows all enemies passing through |

## Future: Damage Type System

The MVP uses untyped damage for simplicity. Post-launch updates will introduce a damage type system to increase strategic depth when more enemy types are added.

### Planned Damage Types

| Type | Theme | Visual Color | Status Synergy |
|------|-------|--------------|----------------|
| **Signature** | Pattern matching, known threat detection | Green | Breach (flag for deletion) |
| **Heuristic** | Behavioral analysis, anomaly detection | Orange | Burn (corruption spread) |
| **Encryption** | Cryptographic attacks, key breaking | Blue/cyan | Slow (decryption overhead) |
| **Protocol** | Network-layer attacks, packet injection | Purple | Stun (connection interrupt) |

### Tower Type Assignments

| Tower | Damage Type | Rationale |
|-------|-------------|-----------|
| Antivirus Turret (Base) | Signature | Classic signature-based detection |
| Firewall Cannon (AOE) | Heuristic | Broad pattern analysis, catches clusters |
| Intrusion Detector (Burst) | Signature | Deep scan, high-confidence single target |
| Network Railgun (Piercing) | Protocol | Packet-level attack through multiple hosts |
| Malware Scanner (Utility) | Encryption | Decrypt and slow malicious processes |

### Enemy Resistance Framework

When new enemy types are introduced, they will have type affinities:

| Affinity | Damage Modifier |
|----------|-----------------|
| Immune | 0x (no damage) |
| Resistant | 0.5x |
| Neutral | 1.0x |
| Vulnerable | 1.5x |
| Critical Weakness | 2.0x |

### Planned Enemy Type Examples

| Enemy | Signature | Heuristic | Encryption | Protocol |
|-------|-----------|-----------|------------|----------|
| Virus (Basic) | Neutral | Neutral | Neutral | Neutral |
| Worm (Fast) | Neutral | Neutral | Neutral | Neutral |
| Ransomware (Boss) | Neutral | Neutral | Neutral | Neutral |
| Polymorphic Virus (future) | Resistant | Vulnerable | Neutral | Neutral |
| Encrypted Payload (future) | Neutral | Resistant | Critical Weakness | Neutral |
| Botnet Node (future) | Neutral | Neutral | Resistant | Critical Weakness |
| Zero-Day Exploit (future) | Immune | Vulnerable | Neutral | Vulnerable |

### Implementation Notes

- Type system activates when Chapter 2 introduces new enemy types
- MVP enemies (Virus, Worm, Ransomware) remain type-neutral for backwards compatibility
- UI must show enemy resistances on hover/inspection
- Tower selection screen should display damage type iconography
- Mastery abilities retain their effects independent of damage type

### Design Goals

1. **Rock-paper-scissors depth** without hard counters
2. **No mandatory types**—any tower can contribute, typed towers excel
3. **Stage theming**—certain stages favor certain types (e.g., encryption-heavy datacenter)
4. **Build diversity**—discourage mono-type tower setups via enemy mix

## Tower Group Guidelines

When designing new towers, assign them to groups based on their role:

| Group | Role | Characteristics |
|-------|------|-----------------|
| **Basic** | Primary damage dealer | High DPS, defines run strategy, selected pre-run |
| **Advanced** | Supplementary damage | Complements Basic Tower, moderate DPS with utility |
| **Special** | Crowd control / Utility | No direct damage, focuses on enemy manipulation |

## Tower Archetypes

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

## Example Additional Enemies

The following are example enemy types that can be added beyond Basic and Boss:

| Enemy Type | Behavior |
|------------|----------|
| Splitting Enemy | Split into smaller copies when damaged, creating more targets |
| Disguised Enemy | Appear harmless until close to the wall, then reveal true nature |
| Swarm Enemy | Huge waves of weak units designed to overwhelm defenses |
| Invisible Enemy | Cannot be targeted until revealed by a detection tower |
| Disabling Enemy | Attempts to disable towers, temporarily shutting them down |

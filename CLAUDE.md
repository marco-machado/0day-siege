# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

0DaySiege is a tower defense game where players defend a network's Firewall from waves of malware (viruses, worms, ransomware). Built with Unity 6 (6000.2.7f2) using URP with a 2D renderer.

**Target Platforms:** iOS, Android, macOS, Windows

**Key Documents:**
- [GDD.md](GDD.md) - Complete game design document with mechanics, balance values, and systems
- [PLAN.md](PLAN.md) - Implementation plan organized into 20 epics

## Architecture

### Assembly Structure

All game code lives in the `ZeroDaySiege` assembly (`Assets/Scripts/ZeroDaySiege.asmdef`) with namespace `ZeroDaySiege`. Dependencies: Unity.InputSystem, Unity.TextMeshPro.

### Core Systems (Implemented)

**GameBootstrap** (`Core/GameBootstrap.cs`) - Entry point using `[RuntimeInitializeOnLoadMethod]`. Creates persistent manager objects at startup:
- `[GameManager]` - State machine and run flow
- `[WaveManager]` - Wave state and transitions
- `[GameLayout]` - Screen boundaries and coordinate conversion
- `[ScreenController]` - Resolution and orientation handling
- `[Firewall]` - Firewall entity with HP and visual feedback
- `[EventSystem]` - UI input handling (new Input System)
- `[RunCanvas]` - UI canvas with wave display, health bar, pause overlay, vignette
- `[DebugControls]` - Keyboard shortcuts (Escape for pause, F1-F7 for debug)

**GameManager** (Singleton) - State machine with validated transitions:
```
Menu -> Playing
Playing <-> Paused
Playing <-> CardSelection
Playing -> GameOver
GameOver -> Menu | Playing (restart)
Paused -> Menu
```
- Events: `OnStateChanged(prev, new)`, `OnWaveChanged(wave)`
- Run tracking: `CurrentWave` (1-20), `LastRunOutcome` (None/Victory/Defeat)
- Methods: `StartRun()`, `PauseGame()`, `ResumeGame()`, `RestartRun()`, `EndRun(victory)`, `AdvanceWave()`

**WaveManager** (Singleton) - Wave lifecycle and transitions:
```
Idle -> InProgress -> Transitioning (1s pause) -> InProgress -> ... -> Victory
```
- Events: `OnWaveStateChanged(state)`
- Methods: `CompleteCurrentWave()` - called when wave spawning finishes, triggers transition

**GameLayout** (Singleton) - Defines play area in world units:
- Spawn line: Y=8, Firewall: Y=-4, Tower slots: Y=-6
- Play width: 10 units (-5 to +5)
- Coordinate helpers: `NormalizedToWorldX/Y()`, `GetTowerSlotPosition(0-4)`

**Firewall** (`Firewall/Firewall.cs`) - Singleton, the defensive wall players protect:
- Base HP: 2000, tracks `CurrentHP`, `MaxHP`, `HPPercent`
- Health states: `Healthy` (100-51%), `Damaged` (50-26%), `Critical` (≤25%), `Destroyed` (0%)
- Events: `OnHPChanged(current, max)`, `OnHealthStateChanged(state)`, `OnFirewallDestroyed`
- Methods: `TakeDamage(amount)`, `Heal(amount)`, `HealPercent(percent)`, `ResetHP()`
- Visual: Color changes (cyan → orange → red with flicker), positioned at GameLayout.FirewallY
- Triggers `GameManager.EndRun(false)` when destroyed

### Patterns

- **Singletons** for managers (`Instance` property with `DontDestroyOnLoad`)
- **Events** for state changes (C# events, not UnityEvents)
- **SerializeField** for inspector-exposed private fields
- **Time.timeScale** controls pause (0 for Paused/CardSelection/GameOver)

### UI Systems (Implemented)

**RunUI** (`UI/RunUI.cs`) - Wave counter display, visible during Playing/Paused/CardSelection

**FirewallUI** (`UI/FirewallUI.cs`) - Health bar display (bottom-center), shows current/max HP with color matching health state

**VignetteOverlay** (`UI/VignetteOverlay.cs`) - Red pulsing screen overlay when Firewall HP ≤25%

**PauseUI** (`UI/PauseUI.cs`) - Pause button (top-right during Playing) and pause overlay with Resume/Restart/Quit buttons

**ConfirmationDialog** (`UI/ConfirmationDialog.cs`) - Reusable modal dialog for destructive actions (Restart/Quit)

### Debug Controls

| Key | Action |
|-----|--------|
| Escape | Toggle pause (Playing ↔ Paused) |
| F1 | Start run |
| F2 | Advance wave (instant, no transition) |
| F3 | Trigger defeat |
| F4 | Return to menu |
| F5 | Complete wave (triggers 1s transition) |
| F6 | Damage firewall by 200 HP |
| F7 | Heal firewall by 30% |

*F1-F7 only available in Editor/Development builds*

### Namespace Organization

```
ZeroDaySiege.Core     - Game state, managers, bootstrap
ZeroDaySiege.Firewall - Firewall entity and health states
ZeroDaySiege.UI       - HUD, pause menu, dialogs
```

Future namespaces (per PLAN.md): `Enemies`, `Towers`, `Cards`, `Progression`

## Testing

Unity Test Framework is installed. Tests go in:
- `Assets/Tests/Editor/` - Edit Mode tests
- `Assets/Tests/Runtime/` - Play Mode tests

Run via Unity: Window > General > Test Runner

## Build

Building is done through Unity Editor:
- File > Build Settings to configure platform
- File > Build And Run to build and launch

## Git Rules

**Never auto-commit.** Only commit when explicitly requested by the user.

## GitHub Issues

Development tracked in GitHub Issues. See PLAN.md for epic breakdown.

**Labels:** `epic:1-core-framework` through `epic:20-content`, plus `type:feature|bug|chore`, `priority:high|low`, `status:blocked`

**Issue Format:**
```
Title: [Epic X] Short description

## Description
What needs to be done.

## Acceptance Criteria
- [ ] Criterion 1

## References
- GDD Section X.X
```

**Rules:**
- One issue = one focused task
- Issues link to GDD sections for requirements
- PRs reference issues they close (`Closes #123`)
- Do not create issues unless explicitly asked

## Code Conventions

- Scripts in `Assets/Scripts/` organized by system (Core, UI, Enemies, Towers, etc.)
- Use `[SerializeField]` for inspector fields
- Use Unity's new Input System (not legacy Input class)
- Follow GDD.md for game constants (HP values, damage, speeds, etc.)

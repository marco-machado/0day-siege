# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

0DaySiege is a Unity 6 (version 6000.2.7f2) game project using the Universal Render Pipeline (URP) with a 2D-focused setup. The project is in early development with the framework established but no game code written yet.

## Development Environment

- **Engine**: Unity 6000.2.7f2
- **Render Pipeline**: Universal Render Pipeline (URP) 17.2.0 with 2D renderer
- **IDE Support**: JetBrains Rider and Visual Studio packages installed

## Target Platforms

| Platform | Notes |
|----------|-------|
| iOS | Mobile - touch input |
| Android | Mobile - touch input |
| macOS | Desktop - keyboard/mouse, gamepad |
| Windows | Desktop - keyboard/mouse, gamepad |

## Project Structure

```
Assets/
├── Scenes/          # Game scenes (MainGame.unity is the primary scene)
├── Scripts/         # C# game scripts (place scripts here)
├── Prefabs/         # Reusable game objects
├── Sprites/         # 2D graphics and artwork
├── Audio/           # Sound effects and music
└── Settings/        # URP and render pipeline configuration
```

## Key Packages

| Package | Purpose |
|---------|---------|
| com.unity.inputsystem | New Input System for player controls |
| com.unity.2d.animation | Sprite animation system |
| com.unity.2d.tilemap | Grid-based level design |
| com.unity.2d.aseprite | Direct Aseprite file import |
| com.unity.visualscripting | Visual scripting support |
| com.unity.test-framework | Unit testing |

## Input System

The project uses Unity's new Input System with pre-configured action maps in `Assets/InputSystem_Actions.inputactions`:

**Player Actions:**
- Move (Vector2): WASD/Arrow keys, Gamepad left stick
- Look (Vector2): Mouse delta, Gamepad right stick
- Attack: Left mouse, Gamepad west button, Enter
- Interact: E key (hold), Gamepad north button
- Jump: Space, Gamepad south button
- Sprint: Left Shift, Gamepad left stick press
- Crouch: C key, Gamepad east button
- Previous/Next: 1/2 keys, Gamepad D-pad left/right

**Supported Control Schemes:** Keyboard & Mouse, Gamepad, Touch, Joystick, XR

## Testing

Unity Test Framework is installed. Tests should be placed in:
- `Assets/Tests/Editor/` for Edit Mode tests
- `Assets/Tests/Runtime/` for Play Mode tests

Run tests via Unity Editor: Window > General > Test Runner

## Build Commands

Building is done through Unity Editor:
- File > Build Settings to configure platform
- File > Build And Run to build and launch

## Git Rules

- **Never auto-commit.** Only commit when explicitly requested by the user.

## GitHub Issues Workflow

Development tasks are tracked in GitHub Issues.

### Labels

| Label | Purpose |
|-------|---------|
| `epic:1-core-framework` | Epic 1: Core Game Framework |
| `epic:2-firewall` | Epic 2: Firewall System |
| `epic:3-enemy` | Epic 3: Enemy System |
| `epic:4-tower-core` | Epic 4: Tower System - Core |
| `epic:5-tower-basic` | Epic 5: Tower System - Basic Towers |
| `epic:6-tower-advanced` | Epic 6: Tower System - Advanced Towers |
| `epic:7-score-cards` | Epic 7: Score & Card System |
| `epic:8-stage-wave` | Epic 8: Stage & Wave System |
| `epic:9-currency-unlocks` | Epic 9: Meta-Progression - Currency & Unlocks |
| `epic:10-mastery` | Epic 10: Meta-Progression - Tower Mastery |
| `epic:11-profiles` | Epic 11: Battle Profiles |
| `epic:12-gear-core` | Epic 12: Gear System - Core |
| `epic:13-gear-chips` | Epic 13: Gear System - Chips |
| `epic:14-gear-full` | Epic 14: Gear System - Full Implementation |
| `epic:15-stage-progression` | Epic 15: Stage Progression & Unlocks |
| `epic:16-status-effects` | Epic 16: Status Effects System |
| `epic:17-vfx` | Epic 17: Visual Feedback & Polish |
| `epic:18-ui` | Epic 18: UI/UX Systems |
| `epic:19-audio` | Epic 19: Audio System |
| `epic:20-content` | Epic 20: Balancing & Content |
| `type:feature` | New functionality |
| `type:bug` | Defects |
| `type:chore` | Maintenance, refactoring |
| `priority:high` | Must do soon |
| `priority:low` | Can wait |
| `status:blocked` | Waiting on something |

### Milestones

| Milestone | Epics | Goal |
|-----------|-------|------|
| Phase 1 - Core Loop | 1-4 | Playable prototype |
| Phase 2 - Tower Variety | 5-6 | All tower types |
| Phase 3 - In-Run Progression | 7-8 | Cards, stages |
| Phase 4 - Meta-Progression | 9-11 | Unlocks, mastery, profiles |
| Phase 5 - Gear Depth | 12-14 | Gear and chips |
| Phase 6 - Polish | 15-20 | VFX, UI, audio, content |

### Issue Format

```
Title: [Epic X] Short description

## Description
What needs to be done.

## Acceptance Criteria
- [ ] Criterion 1
- [ ] Criterion 2

## References
- GDD Section X.X
- Related issues: #123
```

### Rules

- One issue = one focused task (few hours to ~1 day of work)
- Issues link back to GDD sections for requirements
- PRs reference issues they close (`Closes #123`)
- Claude does not create issues unless explicitly asked

## Code Conventions

- Scripts go in `Assets/Scripts/` organized by feature/system
- Use `[SerializeField]` for inspector-exposed private fields
- Follow Unity's component-based architecture
- Use the new Input System APIs (not legacy Input class)

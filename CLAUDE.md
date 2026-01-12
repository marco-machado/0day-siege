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

## Code Conventions

- Scripts go in `Assets/Scripts/` organized by feature/system
- Use `[SerializeField]` for inspector-exposed private fields
- Follow Unity's component-based architecture
- Use the new Input System APIs (not legacy Input class)

# 2D Platformer

A 2D platformer built in Unity from an inherited, deliberately incomplete codebase. The project involved reading unfamiliar code, fixing the bugs that made it unplayable, completing the missing gameplay scripts, and adding new features on top.

## How to play

- **Move:** Left / Right arrow keys or A/D keys
- **Jump:** Spacebar (only while grounded)
- **Shoot:** J key (fires a bullet to damage enemies and the boss)
- **Goal:** survive the enemies and the water and defeat the boss to win. Falling in water or taking an enemy hit costs a life and some time; running out of either ends the game.

## Features

**Bug fixes**
- Camera follow now assigns the player as its target at runtime (was throwing a null reference).
- Player movement completed: per-frame grounded and jump checks, correct horizontal input axis, and a spacebar jump that fires once per press while grounded.
- Scene loading fixed and the gameplay scene registered in the build list.
- Fixed a "sticky" collision where the player caught on the seams between ground colliders, using a frictionless physics material.

**Gameplay systems**
- A `GameManager` acts as the single source of truth for lives, the timer, respawning, and the win/lose flow.
- Water death: respawn while lives remain, otherwise show the end scene with Replay and Quit. Respawn happens near the water the player entered, not back at the start.
- Countdown timer displayed in `mm:ss`, with a time penalty applied on each death.
- Boss fight with tunable health and a health bar above the boss; defeating the boss wins the game.
- Main menu with a Play button.

## Project structure

```
Assets/Scripts/
  Camera Scripts/      CameraFollow
  Player Scripts/      PlayerMovement, PlayerDamage, PlayerShoot, FireBullet, ScoreManager, WaterDeath
  Enemy Scripts/       Bird, Frog, Snail, Spider, Egg
  Boss Scripts/        BossScript, BossHealth, StoneScript
  Controller Scripts/  MainMenuController
  Helper Scripts/      MyTags
  GameManager.cs
Assets/Scenes/         MainMenu, Gameplay
```

## Getting started

1. Clone the repository.
2. Open the project in Unity (2D Core template).
3. Open `Assets/Scenes/MainMenu.unity` and press Play, or build via `File > Build Profiles` with `MainMenu` first in the scene list.

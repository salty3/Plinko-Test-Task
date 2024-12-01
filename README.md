# Plinko-Test-Task

**Used Packages:** Zenject, DOTween, UniTask
**Used Patterns:** Entry Point, MVP, DI, Object Pool

## How to Run
1. Open the `Startup` scene.
2. Press play.

## Patterns

### Entry Point
Used for easy control of game loading and initialization. Each scene has its own entry point, which initializes the scene context, necessary services, and loads heavy assets. (See `ISceneEntryPoint` and the `SceneManagement` folder)

**Examples:**
- `Startup.cs`: A lightweight entry point that initializes the project context and loads the initial scene.
- `ApplicationInitializer.cs`: Initializes all global services and can be used for establishing connections with the backend, loading initial data, etc.

### Object Pool
Made the simplest pool implementation for plinko balls.
- `Pool.cs`

### Betting/Balance
Simplest realizations. Ideally all balance and plinko calculations should be done on the server side.
- `ICurrencyService.cs`
- `IBetService.cs`

### ~~State Machine~~
I really don't know where to use it here. I had 2 variants:
1. Use it for pins amount (12/14/16) but is it really necessary?
2. Use it for game states but there is only one state.

## Main Files
- `PlinkoLogic.cs`: Roll calculations.
- `PlinkoCore.cs`: Main game loop logic.
- `PinsRenderer.cs`: Visuals logic. Ideally should be separated into smaller classes :)

## UI
I didn't find any similar assets to demo, so I used Rounded rects from open upm https://openupm.com/packages/com.gilzoide.rounded-corners/
Looks kinda ugly and add to many vertices but that's most similar to the original design.

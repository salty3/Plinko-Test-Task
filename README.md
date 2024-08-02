# Card-Flip-Test-Task

**Used Packages:** Zenject, DOTween, UniTask, SmoothLayout  
**Used Patterns:** Entry Point, State Machine, MVP, DI

## How to Run
1. Open the `Startup` scene.
2. Press play.

## Patterns

### Entry Point
Used for easy control of game loading and initialization. Each scene has its own entry point, which initializes the scene context, necessary services, and loads heavy assets. (See `ISceneEntryPoint` and the `SceneManagement` folder)

**Examples:**
- **Startup.cs**: A lightweight entry point that initializes the project context and loads the initial scene.
- **ApplicationInitializer.cs**: Initializes all global services and can be used for establishing connections with the backend, loading initial data, etc.

### State Machine
Used for easy control of game states. Each state is a separate class, which can be easily extended and modified.

- **GameplayLoopStateManager.cs**: Responsible for game loop states.
- **MenuStateManager.cs**: Responsible for menu states.

### MVP (Model-View-Presenter)
Used for easy separation of concerns and testability. Each view has its own presenter, responsible for view logic and communication with other parts of the system. Ideally, any reactive library should be used.

### Dependency Injection (DI)
Not Singleton :)

## Packages

### DOTween
Chosen for its simplicity over Unity's animation system for this task.

### UniTask
Preferred for its clarity over coroutines.

### [SmoothLayout](https://gist.github.com/codorizzi/79aab1ae7d7940fe3e3603af61cd8617)
Used for cards shuffle animation.

---

The completion of this test assignment took ~24 hours.

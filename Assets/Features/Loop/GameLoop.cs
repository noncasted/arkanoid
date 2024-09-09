using System;
using Cysharp.Threading.Tasks;
using Features.Completion;
using Features.GamePlay;
using Features.GamePlay.Balls.Collection;
using Features.GamePlay.Balls.Entity;
using Features.GamePlay.Balls.Factory;
using Features.GamePlay.Balls.SpeedHandler;
using Features.GamePlay.Balls.Starter;
using Features.GamePlay.Levels.Platforms;
using Features.GamePlay.Powerups.Spawner;
using Features.Services;
using Global.Cameras;
using Global.UI;
using Internal;

namespace Features.Loop
{
    public class GameLoop : IGameLoop
    {
        public GameLoop(
            IUIStateMachine stateMachine,
            IMainOverlay overlay,
            IGameCamera gameCamera,
            ICurrentCameraProvider cameraProvider,
            ILevelsStorage levelsStorage,
            ILevelLoader levelLoader,
            ICompletionUI completionUI,
            IBallStarter ballStarter,
            IPlatform platform,
            IBallFactory ballFactory,
            IObjectFactory<Ball> ballsObjectFactory,
            IBallCollection collection,
            IBallsSpeedHandler ballsSpeedHandler,
            IPowerupsSpawner powerupsSpawner)
        {
            _stateMachine = stateMachine;
            _overlay = overlay;
            _gameCamera = gameCamera;
            _cameraProvider = cameraProvider;
            _levelsStorage = levelsStorage;
            _levelLoader = levelLoader;
            _completionUI = completionUI;
            _ballStarter = ballStarter;
            _platform = platform;
            _ballFactory = ballFactory;
            _ballsObjectFactory = ballsObjectFactory;
            _collection = collection;
            _ballsSpeedHandler = ballsSpeedHandler;
            _powerupsSpawner = powerupsSpawner;
        }

        private readonly IUIStateMachine _stateMachine;
        private readonly IMainOverlay _overlay;
        private readonly IGameCamera _gameCamera;
        private readonly ICurrentCameraProvider _cameraProvider;
        private readonly ILevelsStorage _levelsStorage;
        private readonly ILevelLoader _levelLoader;
        private readonly ICompletionUI _completionUI;
        private readonly IBallStarter _ballStarter;
        private readonly IPlatform _platform;
        private readonly IBallFactory _ballFactory;
        private readonly IObjectFactory<Ball> _ballsObjectFactory;
        private readonly IBallCollection _collection;
        private readonly IBallsSpeedHandler _ballsSpeedHandler;
        private readonly IPowerupsSpawner _powerupsSpawner;

        private ILifetime _currentLifetime;
        private ILevelConfiguration _currentSelection;
        private IReadOnlyLifetime _parentLifetime;

        public UniTask Process(IReadOnlyLifetime lifetime)
        {
            _parentLifetime = lifetime;
            _cameraProvider.SetCamera(_gameCamera.Camera);
            _stateMachine.EnterChild(_stateMachine.Base, _overlay);
            _overlay.LevelSelected.Advise(lifetime, LoadLevel);

            LoadLevel(_levelsStorage.Get(0));

            return UniTask.CompletedTask;
        }

        private void LoadLevel(ILevelConfiguration configuration)
        {
            _currentLifetime?.Terminate();
            _currentLifetime = _parentLifetime.Child();
            _currentSelection = configuration;
            HandleLevel(_currentLifetime, _currentSelection).Forget();
        }

        private async UniTask HandleLevel(IReadOnlyLifetime lifetime, ILevelConfiguration configuration)
        {
            _ballsObjectFactory.DestroyAll();
            var level = _levelLoader.Load(configuration);
            var levelLifetime = lifetime.Child();
            
            _collection.Setup(levelLifetime);
            _platform.ToSpawn();
            _ballsSpeedHandler.Setup(levelLifetime);
            
            var startBall = _ballFactory.Create(_platform.BallSpawnPosition);
            var startDirection = await _ballStarter.ProcessStart(lifetime);
            startBall.Setup(startDirection);

            _platform.Setup(levelLifetime);
            _powerupsSpawner.Setup(levelLifetime, level);

            var completionAwaiter = new GameCompletionAwaiter(lifetime, level.Blocks, _collection);
            var result = await completionAwaiter.Await();

            levelLifetime.Terminate();

            _ballsObjectFactory.DestroyAll();

            await _stateMachine.ProcessChild(_overlay, _completionUI, handle => _completionUI.Process(handle, result));

            switch (result)
            {
                case GameResult.Win:
                    _levelsStorage.OnLevelPassed(configuration);
                    LoadLevel(_levelsStorage.GetNext(configuration));
                    break;
                case GameResult.Lose:
                    LoadLevel(configuration);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
using Features.GamePlay.Balls.Collection;
using Features.GamePlay.Balls.Factory;
using Features.GamePlay.Balls.SpeedHandler;
using Features.GamePlay.Powerups.Spawner;
using Features.Loop;
using Features.Services.Sounds;
using Internal;

namespace Features.Services
{
    public static class GamePlayServicesExtensions
    {
        public static IScopeBuilder AddGamePlayServices(this IScopeBuilder builder)
        {
            builder.Register<LevelsStorage>()
                .WithAsset<LevelsStorageOptions>()
                .As<ILevelsStorage>()
                .As<IScopeSetupAsync>();

            builder.Register<GameInput>()
                .As<IGameInput>()
                .As<IScopeSetup>();

            builder.Register<BallCollection>()
                .As<IBallCollection>();
            
            builder.Register<BallFactory>()
                .As<IBallFactory>();

            builder.RegisterAsset<BallFactoryOptions>();

            builder.Register<GameUpdater>()
                .As<IScopeSetup>()
                .As<IGameUpdater>();

            builder.Register<BallsSpeedHandler>()
                .As<IBallsSpeedHandler>();

            builder.Register<PauseHandler>()
                .As<IPause>();
            
            builder.Register<PowerupsSpawner>()
                .WithAsset<PowerupSpawnerOptions>()
                .As<IPowerupsSpawner>();

            builder.Register<GameSounds>()
                .WithAsset<GameSoundsOptions>()
                .As<IScopeSetup>();

            return builder;
        }
    }
}
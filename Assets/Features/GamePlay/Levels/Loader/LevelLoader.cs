using Features.Services;

namespace Features.GamePlay
{
    public class LevelLoader : ILevelLoader
    {
        public LevelLoader(IGameUpdater updater, IObjectFactory<Level> objectFactory)
        {
            _updater = updater;
            _objectFactory = objectFactory;
        }

        private readonly IGameUpdater _updater;
        private readonly IObjectFactory<Level> _objectFactory;

        public ILevel Load(ILevelConfiguration configuration)
        {
            _objectFactory.DestroyAll();
            var level = _objectFactory.Create(configuration.Prefab);
            level.Setup();

            foreach (var block in level.BlocksInternal)
                block.Construct(_updater);
            
            return level;
        }
    }
}
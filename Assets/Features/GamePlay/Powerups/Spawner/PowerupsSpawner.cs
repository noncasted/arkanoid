using System.Collections.Generic;
using Features.GamePlay.Balls.Collection;
using Features.GamePlay.Balls.Factory;
using Features.GamePlay.Powerups.Base;
using Features.Services;
using Global.Systems;
using Internal;

namespace Features.GamePlay.Powerups.Spawner
{
    public class PowerupsSpawner : IPowerupsSpawner, IUpdatable
    {
        public PowerupsSpawner(
            IObjectFactory<PowerupBase> factory,
            IPowerupSpawnBounds bounds,
            IGameUpdater updater,
            IBallCollection ballCollection,
            IBallFactory ballFactory,
            PowerupSpawnerOptions options)
        {
            _factory = factory;
            _bounds = bounds;
            _updater = updater;
            _ballCollection = ballCollection;
            _ballFactory = ballFactory;
            _options = options;
        }

        private readonly IObjectFactory<PowerupBase> _factory;
        private readonly IPowerupSpawnBounds _bounds;
        private readonly IGameUpdater _updater;
        private readonly IBallCollection _ballCollection;
        private readonly IBallFactory _ballFactory;
        private readonly PowerupSpawnerOptions _options;

        private readonly Dictionary<PowerupDefinition, float> _timers = new();

        public void Setup(IReadOnlyLifetime lifetime, ILevel level)
        {
            _updater.Add(lifetime, this);
            _timers.Clear();

            foreach (var definition in _options.Objects)
                _timers.Add(definition, 0);

            foreach (var block in level.Blocks)
                block.IsDestroyed.AdviseTrue(lifetime, OnBlockDestroyed);
            
            lifetime.Listen(_factory.DestroyAll);
        }
        
        public void OnUpdate(float delta)
        {
            foreach (var definition in _options.Objects)
                _timers[definition] += delta;
        }
        
        private void OnBlockDestroyed()
        {
            foreach (var definition in _options.Objects)
            {
                if (_timers[definition] < definition.SpawnRate)
                    continue;

                var position = _bounds.GetSpawnPosition();
                var powerup = _factory.Create(definition.Prefab, position);
                powerup.Construct(_updater, _ballCollection, _ballFactory, definition);
                
                _timers[definition] = 0;
            }
        }
    }
}
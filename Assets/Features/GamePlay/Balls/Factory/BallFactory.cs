using Features.GamePlay.Balls.Collection;
using Features.GamePlay.Balls.Entity;
using Features.Services;
using Internal;
using UnityEngine;

namespace Features.GamePlay.Balls.Factory
{
    public class BallFactory : IBallFactory
    {
        public BallFactory(
            IObjectFactory<Ball> objectFactory,
            IViewInjector injector,
            IBallCollection collection,
            BallFactoryOptions options)
        {
            _objectFactory = objectFactory;
            _injector = injector;
            _collection = collection;
            _options = options;
        }

        private readonly IObjectFactory<Ball> _objectFactory;
        private readonly IViewInjector _injector;
        private readonly IBallCollection _collection;
        private readonly BallFactoryOptions _options;

        public IBall Create(Vector2 position)
        {
            var ball = _objectFactory.Create(_options.Prefab, position);
            _injector.Inject(ball);
            _collection.Add(ball);
            
            return ball;
        }
    }
}
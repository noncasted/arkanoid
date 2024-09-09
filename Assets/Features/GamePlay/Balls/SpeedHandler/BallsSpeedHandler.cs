using Features.GamePlay.Balls.Collection;
using Features.Services;
using Internal;

namespace Features.GamePlay.Balls.SpeedHandler
{
    public class BallsSpeedHandler : IBallsSpeedHandler
    {
        public BallsSpeedHandler(IBallCollection collection, IGameUpdater updater)
        {
            _updater = updater;
            _collection = collection;
        }
        
        private readonly IBallCollection _collection;
        private readonly IGameUpdater _updater;
        
        public void Setup(IReadOnlyLifetime lifetime)
        {
            _updater.IsPaused.View(lifetime, OnPaused);
        }

        private void OnPaused(bool isPaused)
        {
            if (isPaused == true)
            {
                foreach (var ball in _collection.Entries)
                    ball.Stop();
            }
            else
            {
                foreach (var ball in _collection.Entries)
                    ball.Continue();
            }
            
        }
    }
}
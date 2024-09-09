using System.Collections.Generic;
using Features.GamePlay.Balls.Entity;
using Internal;

namespace Features.GamePlay.Balls.Collection
{
    public class BallCollection : IBallCollection
    {
        private readonly List<IBall> _balls = new();
        private readonly ViewableProperty<bool> _anyAlive = new(false);
        private readonly ViewableDelegate _ballDied = new();
        private IReadOnlyLifetime _lifetime;

        public IViewableProperty<bool> AnyAlive => _anyAlive;
        public IReadOnlyList<IBall> Entries => _balls;

        public void Setup(IReadOnlyLifetime lifetime)
        {
            _lifetime = lifetime;
            _balls.Clear();
        }

        public void Add(IBall ball)
        {
            _balls.Add(ball);
            _anyAlive.Set(true);

            ball.Died.Advise(_lifetime, () =>
            {
                _balls.Remove(ball);

                if (_balls.Count == 0)
                    _anyAlive.Set(false);
                else 
                    _ballDied.Invoke();
            });
        }
    }
}
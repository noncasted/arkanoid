using System.Collections.Generic;
using Features.GamePlay.Balls.Entity;
using Internal;

namespace Features.GamePlay.Balls.Collection
{
    public interface IBallCollection
    {
        IViewableProperty<bool> AnyAlive { get; }
        IReadOnlyList<IBall> Entries { get; }

        void Setup(IReadOnlyLifetime lifetime);
        void Add(IBall ball);
    }
}
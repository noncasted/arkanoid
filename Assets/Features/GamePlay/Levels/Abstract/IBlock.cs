using Features.GamePlay.Common;
using Internal;

namespace Features.GamePlay
{
    public interface IBlock : ICollisionTarget
    {
        IViewableProperty<bool> IsDestroyed { get; }

        void OnDamage();
    }
}
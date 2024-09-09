using Features.GamePlay.Balls.Collection;
using Features.GamePlay.Balls.Factory;
using Global.Systems;
using UnityEngine;

namespace Features.GamePlay.Powerups.Base
{
    public interface IPowerup
    {
        void Construct(
            IUpdater updater,
            IBallCollection collection,
            IBallFactory factory,
            PowerupDefinition definition);

        void Collect();
        void ForceDestroy();
    }
}
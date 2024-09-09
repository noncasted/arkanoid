using UnityEngine;

namespace Features.GamePlay.Powerups.Spawner
{
    public interface IPowerupSpawnBounds
    {
        Vector2 GetSpawnPosition();
    }
}
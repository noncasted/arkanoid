using Features.GamePlay.Common;
using Internal;
using UnityEngine;

namespace Features.GamePlay.Levels.Platforms
{
    public interface IPlatform : ICollisionTarget
    {
        Vector2 BallSpawnPosition { get; }

        void ToSpawn();
        void Setup(IReadOnlyLifetime lifetime);
        void OnBounce();
    }
}
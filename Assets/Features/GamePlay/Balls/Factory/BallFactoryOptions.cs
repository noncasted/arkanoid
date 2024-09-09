using Features.GamePlay.Balls.Entity;
using Internal;
using UnityEngine;

namespace Features.GamePlay.Balls.Factory
{
    public class BallFactoryOptions : EnvAsset
    {
        [SerializeField] private Ball _prefab;

        public Ball Prefab => _prefab;
    }
}
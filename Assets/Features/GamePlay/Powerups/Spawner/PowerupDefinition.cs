using Internal;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.GamePlay.Powerups.Base
{
    [InlineEditor]
    public class PowerupDefinition : EnvAsset
    {
        [SerializeField] private float _spawnRate;
        [SerializeField] private float _speed;
        [SerializeField] private PowerupBase _prefab;

        public float SpawnRate => _spawnRate;
        public float Speed => _speed;
        public PowerupBase Prefab => _prefab;
    }
}
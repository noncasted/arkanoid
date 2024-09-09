using Features.GamePlay.Levels.Platforms;
using Internal;
using UnityEngine;

namespace Features.GamePlay
{
    public class LevelConstructorOptions : EnvAsset
    {
        [SerializeField] private Block _blockPrefab;
        [SerializeField] [Min(0f)] private float _overlapDistance = 0.2f;
        
        public Block BlockPrefab => _blockPrefab;
        public float OverlapDistance => _overlapDistance;
    }
}
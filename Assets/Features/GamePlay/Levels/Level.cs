using System.Collections.Generic;
using Features.GamePlay.Levels.Platforms;
using UnityEngine;

namespace Features.GamePlay
{
    [DisallowMultipleComponent]
    public class Level : MonoBehaviour, ILevel
    {
        private Block[] _blocks;

        public IReadOnlyList<IBlock> Blocks => _blocks;
        public IReadOnlyList<Block> BlocksInternal => _blocks;

        public void Setup()
        {
            _blocks = GetComponentsInChildren<Block>();
        }
    }
}
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.GamePlay.Levels.Platforms
{
    [InlineEditor]
    public class PlatformOptions : ScriptableObject
    {
        [SerializeField] [Min(0f)] private float _speed;

        public float Speed => _speed;
    }
}
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.GamePlay.Balls.Entity
{
    [InlineEditor]
    public class BallOptions : ScriptableObject
    {
        [SerializeField] private float _speed;

        public float Speed => _speed;
    }
}
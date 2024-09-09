using Internal;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.GamePlay
{
    [InlineEditor]
    public class LevelBlockOptions : EnvAsset
    {
        [SerializeField] private Gradient _gradient;
        [SerializeField] private int _maxHealth;
        [SerializeField] private Curve _scaleCurve;
        [SerializeField] private float _targetScale;

        public Gradient Gradient => _gradient;
        public int MaxHealth => _maxHealth;
        public Curve ScaleCurve => _scaleCurve;
        public float TargetScale => _targetScale;
    }
}
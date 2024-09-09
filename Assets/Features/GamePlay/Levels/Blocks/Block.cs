using Features.Services;
using Global.Systems;
using Internal;
using Shapes;
using TMPro;
using UnityEngine;

namespace Features.GamePlay.Levels.Platforms
{
    [DisallowMultipleComponent]
    public class Block : MonoBehaviour, IBlock
    {
        [SerializeField] private int _health;
        [SerializeField] private LevelBlockOptions _options;

        [SerializeField] private Rectangle _rectangle;
        [SerializeField] private TMP_Text _text;

        private readonly ViewableProperty<bool> _isDestroyed = new();
        private IUpdater _updater;
        private IReadOnlyLifetime _lifetime;
        private float _startScale;

        public IViewableProperty<bool> IsDestroyed => _isDestroyed;

        private void Awake()
        {
            _rectangle.SortingOrder = -(int)(transform.position.y * 1000);
            _startScale = _rectangle.transform.localScale.x;

            UpdateData();
        }

        public void Construct(IGameUpdater updater)
        {
            _updater = updater;
            _lifetime = this.GetObjectLifetime();
        }

        public void OnDamage()
        {
            _health--;

            if (_health < 0)
            {
                _isDestroyed.Set(true);
                gameObject.SetActive(false);
                return;
            }

            UpdateData();

            var startHealth = _health;

            _updater.CurveProgression(_lifetime, _options.ScaleCurve, progress =>
            {
                if (_health != startHealth)
                    return;

                var scale = Mathf.Lerp(_startScale, _options.TargetScale, progress);
                _rectangle.transform.localScale = new Vector3(scale, scale, 1);
            });
        }

        private void UpdateData()
        {
            var colorProgress = _health / (float)_options.MaxHealth;
            _rectangle.Color = _options.Gradient.Evaluate(colorProgress);
            _text.text = _health.ToString();
        }
    }
}
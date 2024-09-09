using Features.GamePlay.Balls.Collection;
using Features.GamePlay.Balls.Factory;
using Features.GamePlay.Common;
using Features.GamePlay.Levels.Platforms;
using Features.Services.Sounds;
using Global.Systems;
using Internal;
using Shapes;
using TMPro;
using UnityEngine;

namespace Features.GamePlay.Powerups.Base
{
    [DisallowMultipleComponent]
    public abstract class PowerupBase : MonoBehaviour, IPowerup
    {
        [SerializeField] private Curve _fadeCurve;
        [SerializeField] private Rectangle _rectangle;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Rigidbody2D _rb;

        private bool _isCollected;
        
        private IUpdater _updater;
        private IBallCollection _collection;
        private IBallFactory _factory;

        protected TMP_Text Text => _text;
        
        public void Construct(
            IUpdater updater,
            IBallCollection collection,
            IBallFactory factory,
            PowerupDefinition definition)
        {
            _factory = factory;
            _collection = collection;
            _updater = updater;
            
            var lifetime = this.GetObjectLifetime();
            
            _updater.RunFixedAction(lifetime, () => _isCollected == false, delta =>
            {
                var position = _rb.position;
                position.y -= definition.Speed * delta;
                _rb.position = position;
            });
        }

        public void Collect()
        {
            Msg.Publish(GameSoundType.Pickup);
            
            Create(_collection, _factory);

            var lifetime = this.GetObjectLifetime();

            _updater.CurveProgression(lifetime, _fadeCurve, progress =>
            {
                var rectColor = _rectangle.Color;
                rectColor.a = progress;
                _rectangle.Color = rectColor;

                var textColor = _text.color;
                textColor.a = progress;
                _text.color = textColor;
                
                if (progress >= 1)
                    ForceDestroy();
            });
        }

        public void ForceDestroy()
        {
            Destroy(gameObject);
        }

        protected abstract void Create(IBallCollection collection, IBallFactory factory);

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_isCollected == true)
                return;
            
            if (other.gameObject.TryGetComponent(out ICollisionTarget target) == false)
                return;

            _isCollected = true;
            
            switch (target)
            {
                case IPlatform:
                    Collect();
                    break;
                case IDeadZone:
                    ForceDestroy();
                    break;
            }
        }
    }
}
using System;
using Features.GamePlay.Common;
using Features.GamePlay.Levels.Platforms;
using Features.Services.Sounds;
using Global.Systems;
using Internal;
using UnityEngine;

namespace Features.GamePlay.Balls.Entity
{
    public class Ball : MonoBehaviour, IBall
    {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private BallOptions _options;

        private readonly ViewableDelegate _died = new();

        private Vector2 _velocity;

        public IViewableDelegate Died => _died;
        public Vector2 Direction => _rb.linearVelocity.normalized;
        public Vector2 Position => _rb.position;

        public void Setup(Vector2 direction)
        {
            _rb.linearVelocity = direction * _options.Speed;
        } 

        public void Stop()
        {
            _velocity = _rb.linearVelocity;
            _rb.linearVelocity = Vector2.zero;
        }

        public void Continue()
        {
            _rb.linearVelocity = _velocity;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out ICollisionTarget collision) == false)
                return;

            Msg.Publish(GameSoundType.Collision);

            switch (collision)
            {
                case IDeadZone:
                    _died.Invoke();
                    Destroy(gameObject);
                    return;
                case IPlatform platform:
                    SetSpeed();
                    platform.OnBounce();
                    return;
                case IBlock block:
                    SetSpeed();
                    block.OnDamage();
                    return;
                case ILevelBorder:
                    SetSpeed();
                    return;

                default:
                    throw new Exception();
            }
        }

        private void SetSpeed()
        {
            _rb.linearVelocity = _rb.linearVelocity.normalized * _options.Speed;
        }
    }
}
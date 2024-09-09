using Features.Services;
using Global.Systems;
using Internal;
using UnityEngine;
using VContainer;

namespace Features.GamePlay.Levels.Platforms
{
    [DisallowMultipleComponent]
    public class Platform : MonoBehaviour, IPlatform, IFixedUpdatable, ISceneService
    {
        [SerializeField] private PlatformOptions _speed;

        [SerializeField] private Transform _leftBorder;
        [SerializeField] private Transform _rightBorder;
        [SerializeField] private Transform _ballSpawnPoint;

        private IGameInput _input;
        private IUpdater _updater;
        private Vector2 _previousCursorPosition;
        private Vector2 _startPosition;

        public Vector2 BallSpawnPosition => _ballSpawnPoint.position;

        [Inject]
        private void Construct(IGameUpdater updater, IGameInput input)
        {
            _updater = updater;
            _input = input;
        }

        public void Create(IScopeBuilder builder)
        {
            _startPosition = transform.position;

            builder.RegisterComponent(this)
                .As<IPlatform>();
        }

        public void ToSpawn()
        {
            transform.position = _startPosition;
        }

        public void Setup(IReadOnlyLifetime lifetime)
        {
            _previousCursorPosition = _input.WorldCursorPosition;
            _updater.Add(lifetime, this);

            var position = transform.position;
            position.x = _input.WorldCursorPosition.x;
            _previousCursorPosition = _input.WorldCursorPosition;

            if (position.x < _leftBorder.position.x)
                position.x = _leftBorder.position.x;
            else if (position.x > _rightBorder.position.x)
                position.x = _rightBorder.position.x;

            transform.position = position;
        }

        public void OnBounce()
        {
        }

        public void OnFixedUpdate(float delta)
        {
            if (_updater.IsPaused.Value == true)
                return;

            var position = transform.position;

            var deltaPosition = _input.WorldCursorPosition - _previousCursorPosition;
            position.x += deltaPosition.x;
            _previousCursorPosition = _input.WorldCursorPosition;

            var moveDirection = _input.MoveDirection;
            position.x += moveDirection.x * _speed.Speed * delta;

            if (position.x < _leftBorder.position.x)
                position.x = _leftBorder.position.x;
            else if (position.x > _rightBorder.position.x)
                position.x = _rightBorder.position.x;

            transform.position = position;
        }
    }
}
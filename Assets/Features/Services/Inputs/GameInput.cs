using Global.Inputs;
using Global.Systems;
using Internal;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Features.Services
{
    public class GameInput : IGameInput, IScopeSetup, IUpdatable
    {
        public GameInput(
            IUpdater updater,
            IGamePlayInputPositionConverter positionConverter,
            Controls.GamePlayActions actions)
        {
            _updater = updater;
            _positionConverter = positionConverter;
            _actions = actions;
        }

        private readonly ViewableProperty<bool> _action = new(false);
        private readonly ViewableProperty<Vector2> _moveDirection = new();

        private readonly IUpdater _updater;
        private readonly IGamePlayInputPositionConverter _positionConverter;
        private readonly Controls.GamePlayActions _actions;

        private Vector2 _worldPosition;

        public IViewableProperty<bool> Action => _action;
        public Vector2 MoveDirection => _moveDirection.Value;
        public Vector2 WorldCursorPosition => _worldPosition;

        public void OnSetup(IReadOnlyLifetime lifetime)
        {
            _updater.Add(lifetime, this);
            _actions.Action.AttachFlag(lifetime, _action);
            _actions.Move.AttachProperty(lifetime, _moveDirection);
        }

        public void OnUpdate(float delta)
        {
            var screenPosition = Mouse.current.position.ReadValue();
            _worldPosition = _positionConverter.ScreenToWorld(screenPosition);
        }
    }
}
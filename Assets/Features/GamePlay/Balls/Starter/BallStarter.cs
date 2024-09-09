using Cysharp.Threading.Tasks;
using Features.Services;
using Global.Systems;
using Internal;
using UnityEngine;
using VContainer;

namespace Features.GamePlay.Balls.Starter
{
    [DisallowMultipleComponent]
    public class BallStarter : MonoBehaviour, IBallStarter, ISceneService
    {
        [SerializeField] private Transform[] _points;

        private IUpdater _updater;
        private IGameInput _input;

        [Inject]
        private void Construct(IGameUpdater updater, IGameInput input)
        {
            _input = input;
            _updater = updater;
        }

        public void Create(IScopeBuilder builder)
        {
            builder.RegisterComponent(this)
                .As<IBallStarter>();
        }

        public async UniTask<Vector2> ProcessStart(IReadOnlyLifetime lifetime)
        {
            gameObject.SetActive(true);

            var direction = Vector2.zero;

            await _updater.RunUpdateAction(lifetime, () => _input.Action.Value == false, _ =>
            {
                var end = _input.WorldCursorPosition;
                var start = transform.position;

                direction = end - (Vector2)transform.position;

                for (var i = 0; i < _points.Length; i++)
                {
                    var pointPosition = Vector3.Lerp(start, end, i / (float)_points.Length);
                    _points[i].position = pointPosition;
                }
            });

            gameObject.SetActive(false);

            return direction.normalized;
        }
    }
}
using Internal;
using UnityEngine;

namespace Features.Services
{
    [DisallowMultipleComponent]
    public class GamePlayInputPositionConverter : MonoBehaviour, ISceneService, IGamePlayInputPositionConverter
    {
        [SerializeField] private Camera _camera;

        public void Create(IScopeBuilder builder)
        {
            builder.RegisterComponent(this)
                .As<IGamePlayInputPositionConverter>();
        }

        public Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            return _camera.ScreenToWorldPoint(screenPosition);
        }
    }
}
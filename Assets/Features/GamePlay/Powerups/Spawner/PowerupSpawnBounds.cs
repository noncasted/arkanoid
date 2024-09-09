using Internal;
using UnityEngine;

namespace Features.GamePlay.Powerups.Spawner
{
    [DisallowMultipleComponent]
    public class PowerupSpawnBounds : MonoBehaviour, ISceneService, IPowerupSpawnBounds
    {
        [SerializeField] private Transform _from;
        [SerializeField] private Transform _to;

        public void Create(IScopeBuilder builder)
        {
            builder.RegisterComponent(this)
                .As<IPowerupSpawnBounds>();
        }

        public Vector2 GetSpawnPosition()
        {
            var y = _from.position.y;
            var x = Random.Range(_from.position.x, _to.position.x);

            return new Vector2(x, y);
        }
    }
}
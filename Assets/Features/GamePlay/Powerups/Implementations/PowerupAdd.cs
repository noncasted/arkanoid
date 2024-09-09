using System.Linq;
using Features.GamePlay.Balls.Collection;
using Features.GamePlay.Balls.Factory;
using Features.GamePlay.Powerups.Base;
using Internal;
using UnityEngine;

namespace Features.GamePlay.Powerups.Implementations
{
    [DisallowMultipleComponent]
    public class PowerupAdd : PowerupBase
    {
        [SerializeField] private int _add = 1;

        private void Awake()
        {
            Text.text = $"+{_add}";
        }

        protected override void Create(IBallCollection collection, IBallFactory factory)
        {
            for (var i = 0; i < _add; i++)
            {
                var spawned = factory.Create(collection.Entries.First().Position);
                spawned.Setup(RandomExtensions.RandomDirection());
            }
        }
    }
}
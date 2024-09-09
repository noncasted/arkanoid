using System.Collections.Generic;
using Features.GamePlay.Balls.Collection;
using Features.GamePlay.Balls.Entity;
using Features.GamePlay.Balls.Factory;
using Features.GamePlay.Powerups.Base;
using Internal;
using UnityEngine;

namespace Features.GamePlay.Powerups.Implementations
{
    [DisallowMultipleComponent]
    public class PowerupMultiply : PowerupBase
    {
        [SerializeField] private float _multiplier = 2f;

        private void Awake()
        {
            Text.text = $"x{_multiplier}";
        }

        protected override void Create(IBallCollection collection, IBallFactory factory)
        {
            var entries = new List<IBall>(collection.Entries);

            foreach (var ball in entries)
            {
                for (var i = 0; i < _multiplier - 1; i++)
                {
                    var spawned = factory.Create(ball.Position);
                    spawned.Setup(RandomExtensions.RandomDirection());
                }
            }
        }
    }
}
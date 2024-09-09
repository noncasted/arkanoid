using Features.GamePlay.Balls.Entity;
using UnityEngine;

namespace Features.GamePlay.Balls.Factory
{
    public interface IBallFactory
    {
        IBall Create(Vector2 position);
    }
}
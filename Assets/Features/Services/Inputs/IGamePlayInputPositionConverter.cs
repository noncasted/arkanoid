using UnityEngine;

namespace Features.Services
{
    public interface IGamePlayInputPositionConverter
    {
        Vector2 ScreenToWorld(Vector2 screenPosition);
    }
}
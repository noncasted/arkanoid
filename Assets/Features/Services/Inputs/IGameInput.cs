using Internal;
using UnityEngine;

namespace Features.Services
{
    public interface IGameInput
    {
        IViewableProperty<bool> Action { get;  }
        Vector2 MoveDirection { get; }
        Vector2 WorldCursorPosition { get; }
    }
}
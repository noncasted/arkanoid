using Internal;
using UnityEngine;

namespace Features.GamePlay.Balls.Entity
{
    public interface IBall
    {
        IViewableDelegate Died { get; }
        Vector2 Direction { get; }        
        Vector2 Position { get; }        
        
        void Setup(Vector2 direction);
        void Stop();
        void Continue();
    }
}
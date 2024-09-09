using Cysharp.Threading.Tasks;
using Internal;
using UnityEngine;

namespace Features.GamePlay.Balls.Starter
{
    public interface IBallStarter
    {
        UniTask<Vector2> ProcessStart(IReadOnlyLifetime lifetime);
    }
}
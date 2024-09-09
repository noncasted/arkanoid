using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Features.GamePlay;
using Features.GamePlay.Balls.Collection;
using Internal;

namespace Features.Loop
{
    public class GameCompletionAwaiter
    {
        public GameCompletionAwaiter(
            IReadOnlyLifetime lifetime,
            IReadOnlyList<IBlock> blocks,
            IBallCollection ballCollection)
        {
            _lifetime = lifetime;
            _blocks = blocks;
            _ballCollection = ballCollection;
        }

        private readonly IReadOnlyLifetime _lifetime;
        private readonly IReadOnlyList<IBlock> _blocks;
        private readonly IBallCollection _ballCollection;

        public async UniTask<GameResult> Await()
        {
            var completion = new UniTaskCompletionSource();
            _lifetime.Listen(() => completion.TrySetCanceled());

            _ballCollection.AnyAlive.Advise(_lifetime, IsCompleted);

            foreach (var block in _blocks)
                block.IsDestroyed.AdviseTrue(_lifetime, IsCompleted);

            await completion.Task;

            return _ballCollection.AnyAlive.Value == true ? GameResult.Win : GameResult.Lose;

            void IsCompleted()
            {
                if (_ballCollection.AnyAlive.Value == false)
                {
                    completion.TrySetResult();
                    return;
                }

                foreach (var block in _blocks)
                {
                    if (block.IsDestroyed.Value == false)
                        return;
                }

                completion.TrySetResult();
            }
        }
    }
}
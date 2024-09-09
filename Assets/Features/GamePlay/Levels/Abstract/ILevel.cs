using System.Collections.Generic;

namespace Features.GamePlay
{
    public interface ILevel
    {
        IReadOnlyList<IBlock> Blocks { get; }
    }
}
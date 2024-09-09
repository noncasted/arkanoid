using Cysharp.Threading.Tasks;
using Features.Loop;
using Global.UI;

namespace Features.Completion
{
    public interface ICompletionUI : IUIState
    {
        UniTask Process(IUIStateHandle handle, GameResult result);
    }
}
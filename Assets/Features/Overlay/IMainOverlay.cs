using Features.Services;
using Global.UI;
using Internal;

namespace Features
{
    public interface IMainOverlay : IUIState 
    {
        IViewableDelegate<ILevelConfiguration> LevelSelected { get; }
    }
}
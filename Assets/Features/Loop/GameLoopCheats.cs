using Internal;
using Sirenix.OdinInspector;

namespace Features.Loop
{
    public class GameLoopCheats : EnvAsset
    {
        private readonly ViewableDelegate _complete = new();
        
        public IViewableDelegate Complete => _complete;

        [Button("Complete game")]
        private void CompleteGame()
        {
            _complete.Invoke();
        }
    }
}
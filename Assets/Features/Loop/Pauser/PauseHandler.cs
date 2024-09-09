using Features.Services;

namespace Features.Loop
{
    public class PauseHandler : IPause
    {
        public PauseHandler(IGameUpdater updater)
        {
            _updater = updater;
        }

        private readonly IGameUpdater _updater;
        
        public void Pause()
        {
            _updater.Pause();    
        }

        public void Continue()
        {
            _updater.Continue();    
        }
    }
}
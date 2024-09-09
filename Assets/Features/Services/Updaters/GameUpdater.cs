using Global.Systems;

namespace Features.Services
{
    public class GameUpdater : UpdaterProxy, IGameUpdater
    {
        public GameUpdater(IUpdater updater) : base(updater)
        {
        }
    }
}
using Internal;

namespace Features.GamePlay.Powerups.Spawner
{
    public interface IPowerupsSpawner
    {
        void Setup(IReadOnlyLifetime lifetime, ILevel level);
    }
}
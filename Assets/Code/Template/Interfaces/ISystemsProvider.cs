using Leopotam.Ecs;

namespace alicewithalex
{
    public interface ISystemsProvider
    {
        EcsSystems GetSystems(EcsSystems current, EcsSystems endFrame,
            IEcsWorldHandler ecsWorldHandler);
    }
}
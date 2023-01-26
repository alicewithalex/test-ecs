using Leopotam.Ecs;

namespace alicewithalex
{
    public interface IEcsWorldHandler
    {
        EcsEntity CreateEntity();

        EcsSystems CreateSystems(string name);
    }
}
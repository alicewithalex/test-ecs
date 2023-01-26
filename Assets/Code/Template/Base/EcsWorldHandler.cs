using Leopotam.Ecs;

namespace alicewithalex
{
    public class EcsWorldHandler : IEcsWorldHandler
    {
        private readonly EcsWorld _ecsWorld;

        public EcsWorldHandler(EcsWorld ecsWorld = null)
        {
            if (ecsWorld != null && ecsWorld.IsAlive())
            {
                _ecsWorld = ecsWorld;
                return;
            }

            _ecsWorld = new EcsWorld();
        }

        public EcsEntity CreateEntity()
        {
            return _ecsWorld.NewEntity();
        }

        public EcsSystems CreateSystems(string name)
        {
            return new EcsSystems(_ecsWorld, name);
        }

        public void Destroy()
        {
            _ecsWorld.Destroy();
        }
    }
}
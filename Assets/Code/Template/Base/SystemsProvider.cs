using Leopotam.Ecs;
using UnityEngine;

namespace alicewithalex
{
    public abstract class SystemsProvider : MonoBehaviour, ISystemsProvider
    {
        public abstract EcsSystems GetSystems(EcsSystems current, EcsSystems endFrame,
            IEcsWorldHandler ecsWorldHandler);
    }
}
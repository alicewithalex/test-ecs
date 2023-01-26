using alicewithalex;
using Leopotam.Ecs;
using UnityEngine;

public abstract class ObjectSystemsProvider : ScriptableObject, ISystemsProvider
{
    public abstract EcsSystems GetSystems(EcsSystems current, EcsSystems endFrame,
        IEcsWorldHandler ecsWorldHandler);
}

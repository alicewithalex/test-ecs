using Leopotam.Ecs;
using UnityEngine;

namespace alicewithalex
{
    public abstract class ViewComponent : MonoBehaviour
    {
        public abstract void Init(IEcsWorldHandler ecsWorldHandler, EcsEntity entity);
    }
}
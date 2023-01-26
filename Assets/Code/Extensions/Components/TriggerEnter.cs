using Leopotam.Ecs;
using UnityEngine;

namespace alicewithalex.Extensions.Components
{
    public struct TriggerEnter
    {
        public EcsEntity OtherEntity;
        public Collider OtherCollider;
    }
}
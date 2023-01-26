using Leopotam.Ecs;
using UnityEngine;

namespace alicewithalex.Game.Components
{
    public struct ActiveTarget
    {
        public EcsEntity Entity;
        public Transform Transform;

        public bool Valid => Entity.IsAlive();
    }
}
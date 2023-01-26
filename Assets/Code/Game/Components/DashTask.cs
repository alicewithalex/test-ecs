using Leopotam.Ecs;
using UnityEngine;

namespace alicewithalex.Game.Components
{
    public struct DashTask
    {
        public readonly Vector3 Velocity;
        public readonly float Duration;
        public float Current { get; private set; }

        public DashTask(Vector3 velocity, float duration)
        {
            Velocity = velocity;
            Current = Duration = duration;
        }

        public bool Tick(float dt)
        {
            return (Current -= dt) <= 0;
        }
    }
}
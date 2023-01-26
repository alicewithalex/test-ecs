using Leopotam.Ecs;
using UnityEngine;

namespace alicewithalex.Game.Components
{
    public struct KnockbackTask
    {
        public readonly Vector3 Knockback;
        public readonly float Duration;
        public float Current { get; private set; }

        public KnockbackTask(Vector3 knockback, float duration)
        {
            Knockback = knockback;
            Current = Duration = duration;
        }

        public bool Tick(float dt)
        {
            return (Current -= dt) <= 0;
        }

        public float Percentage => Current / Duration;
    }
}
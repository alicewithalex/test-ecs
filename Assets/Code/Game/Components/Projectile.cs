using Leopotam.Ecs;
using UnityEngine;

namespace alicewithalex.Game.Components
{
    public struct Projectile
    {
        public Rigidbody Rigidbody;
        public Vector3 PreviousPosition;
        public float Damage;

        public void Toggle(bool state)
        {
            Rigidbody.isKinematic = !state;
            if (state) return;

            Rigidbody.velocity = Rigidbody.angularVelocity = Vector3.zero;
        }
    }
}
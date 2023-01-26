using Leopotam.Ecs;
using UnityEngine;

namespace alicewithalex.Game.Components
{
    public struct Player
    {
        public CharacterController CharacterController;
        public Transform GroundRayOrigin;

        public Vector3 Velocity;
        public Vector3 KnockbackVelocity;
        public Vector3 DashVelocity;
        public float VelocityY;

        public Ray Ray => new Ray(GroundRayOrigin.position,
           Vector3.down);
    }
}
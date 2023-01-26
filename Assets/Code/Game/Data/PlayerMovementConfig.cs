using Leopotam.Ecs;
using UnityEngine;

namespace alicewithalex.Game.Data
{
    [CreateAssetMenu(fileName = "PlayerMovementConfig", menuName = "alicewithalex/Game/Data/GameConfigs/PlayerMovementConfig")]
    public class PlayerMovementConfig : GameConfig
    {
        [Min(0)] public float MovementSpeed = 5.5f;
        [Min(0)] public float RotationSpeed = 10f;
        [Min(0)] public float WeaponRotationSpeed = 10f;
        [Min(0)] public float Gravity = 9.81f;
        [Min(0)] public float GroundDistance = 0.25f;
        [Min(0)] public float JumpForce = 2.5f;
        [Min(0)] public float DashForce = 10f;
        [Min(0)] public float DashDuration = 1f;

        [Space(5)]
        public KeyCode JumpKey = KeyCode.Space;
        public KeyCode DashKey = KeyCode.LeftShift;
    }
}
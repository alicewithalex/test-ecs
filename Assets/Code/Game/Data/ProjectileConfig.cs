using Leopotam.Ecs;
using UnityEngine;

namespace alicewithalex.Game.Data
{
    [CreateAssetMenu(fileName = "ProjectileConfig", menuName = "alicewithalex/Game/Data/GameConfigs/ProjectileConfig")]
    public class ProjectileConfig : GameConfig
    {
        public ViewElement Projectile;
        public KeyCode ShootKey = KeyCode.Mouse0;

        [Space(5)]
        public bool Automatic;
        [Min(1E-2F)] public float ShootDelay = 0.15f;
        [Min(1E-2F)] public float ShootForce = 100f;
        [Min(1E-2F)] public float Lifetime = 10f;
    }
}
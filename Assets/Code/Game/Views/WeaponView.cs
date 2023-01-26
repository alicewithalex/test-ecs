using Leopotam.Ecs;
using UnityEngine;

namespace alicewithalex.Game.Views
{
    public class WeaponView : ViewComponent
    {
        [SerializeField] private Transform _weapon;
        [SerializeField] private Transform _shootOrigin;

        public override void Init(IEcsWorldHandler ecsWorldHandler, EcsEntity entity)
        {
            ref var weapon = ref entity.Get<Components.Weapon>();

            weapon.Self = _weapon;
            weapon.ShootOrigin = _shootOrigin;
        }

    }
}
using Leopotam.Ecs;
using UnityEngine;
using alicewithalex;

namespace alicewithalex.Game.Views
{
    public class ProjectileView : ViewComponent
    {
        [SerializeField] private Rigidbody _rigidbody;

        public override void Init(IEcsWorldHandler ecsWorldHandler, EcsEntity entity)
        {
            entity.Get<Components.Projectile>().Rigidbody = _rigidbody;
        }

    }
}
using Leopotam.Ecs;
using UnityEngine;
using alicewithalex.Game.Components;
using alicewithalex.Extensions.Data;

namespace alicewithalex.Game.Systems
{
    public class ProjectileCollisionSystem : StateSystem<GameplayState>
    {
        private readonly EcsFilter<Projectile>.Exclude<PoolTag> _projectiles;

        private RaycastHit _hit;

        protected override void OnStateUpdate()
        {
            base.OnStateUpdate();

            foreach (var i in _projectiles)
            {
                ref var projectile = ref _projectiles.Get1(i);

                if ((projectile.Rigidbody.position -
                    projectile.PreviousPosition).sqrMagnitude < Mathf.Epsilon)
                {
                    continue;
                }

                if (Physics.Linecast(projectile.Rigidbody.position,
                    projectile.PreviousPosition, out _hit))
                {
                    _projectiles.GetEntity(i).Get<PoolSignal>();

                    if (_hit.collider.gameObject.TryGetEntity(out var entity))
                    {
                        entity.Get<DamageSignal>().Damage = projectile.Damage;
                    }
                }
                else
                {
                    projectile.PreviousPosition = projectile.Rigidbody.position;
                }
            }
        }

    }
}
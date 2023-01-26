using Leopotam.Ecs;
using UnityEngine;
using alicewithalex;
using alicewithalex.Game.Components;

namespace alicewithalex.Game.Systems
{
    public class ProjectileRestartPoolSystem : StateSystem<LoseState>
    {
        private readonly EcsFilter<UnityView, Projectile>.Exclude<PoolTag>
            _outpool;

        protected override void OnStateExit()
        {
            base.OnStateExit();

            foreach (var i in _outpool)
            {
                _outpool.Get1(i).GameObject.SetActive(false);
                _outpool.Get2(i).Toggle(false);
                _outpool.GetEntity(i).Del<ReturnToPoolTask>();
                _outpool.GetEntity(i).Get<PoolTag>();
            }
        }

    }
}
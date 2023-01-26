using Leopotam.Ecs;
using UnityEngine;
using alicewithalex;
using UnityEngine.AI;

namespace alicewithalex.Game.Views
{
    public class EnemyView : ViewComponent
    {
        [SerializeField] private Data.EnemyType _enemyType;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Collider _collider;

        public Data.EnemyType EnemyType => _enemyType;

        public override void Init(IEcsWorldHandler ecsWorldHandler, EcsEntity entity)
        {
            ref var enemy = ref entity.Get<Components.Enemy>();

            enemy.EnemyType = _enemyType;
            enemy.Agent = _agent;
            enemy.Collider = _collider;

            enemy.Collider.enabled = false;
        }

    }
}
using Leopotam.Ecs;
using alicewithalex.Game.Components;

namespace alicewithalex.Game.Systems
{
    public class EnemyNavigationSystem : StateSystem<GameplayState>
    {
        private readonly EcsFilter<UnityView, Player> _player;

        private readonly EcsFilter<Enemy>
            .Exclude<PoolTag, ActiveTarget> _enemies;

        private readonly EcsFilter<Enemy, ActiveTarget>
            .Exclude<PoolTag> _activeEnemies;


        protected override void OnStateUpdate()
        {
            base.OnStateUpdate();

            AssignTarget();
            UpdateTarget();
        }

        protected override void OnStateExit()
        {
            base.OnStateExit();

            foreach (var i in _enemies)
            {
                _enemies.Get1(i).Agent.isStopped = true;
            }
        }

        private void AssignTarget()
        {
            foreach (var i in _player)
            {
                var transform = _player.Get1(i).Transform;
                var entity = _player.GetEntity(i);

                foreach (var j in _enemies)
                {
                    _enemies.GetEntity(j).Get<ActiveTarget>()
                        = new ActiveTarget()
                        {
                            Entity = entity,
                            Transform = transform
                        };
                }
            }
        }

        private void UpdateTarget()
        {
            foreach (var i in _activeEnemies)
            {
                if (!_activeEnemies.Get2(i).Valid)
                {
                    _activeEnemies.GetEntity(i).Del<ActiveTarget>();
                    continue;
                }

                _activeEnemies.Get1(i).Agent.SetDestination(
                    _activeEnemies.Get2(i).Transform.position);
            }
        }
    }
}
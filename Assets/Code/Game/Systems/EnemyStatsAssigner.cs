using Leopotam.Ecs;
using alicewithalex.Game.Components;
using alicewithalex.Game.Data;

namespace alicewithalex.Game.Systems
{
    public class EnemyStatsAssigner : StateSystem<GameplayState>
    {
        private readonly EcsFilter<Enemy, Stats,
            AssignStatsSignal> _enemies;

        private readonly EnemiesFactory _enemiesFactory;

        public EnemyStatsAssigner()
        {
            _enemiesFactory = SceneContainer.Instance.Container
                .Get<EnemiesFactory>();
        }

        protected override void OnStateUpdate()
        {
            base.OnStateUpdate();

            foreach (var i in _enemies)
            {
                var data = _enemiesFactory.GetData(
                    _enemies.Get1(i).EnemyType);

                _enemies.GetEntity(i).Get<Health>() = new Health(
                    data.Health,new EnemyDestroyable());
                _enemies.Get1(i).Agent.speed = data.Speed;

                ref var stats = ref _enemies.Get2(i);
                stats.Speed = data.Speed;
                stats.Attack = data.Attack;
                stats.Defense = data.Defense;
            }
        }

    }
}
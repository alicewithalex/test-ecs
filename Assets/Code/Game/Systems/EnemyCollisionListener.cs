using Leopotam.Ecs;
using alicewithalex.Game.Components;
using alicewithalex.Extensions.Components;

namespace alicewithalex.Game.Systems
{
    public class EnemyCollisionListener : StateSystem<GameplayState>
    {
        private readonly EcsFilter<Enemy, Stats, TriggerEnter> _enemies;

        protected override void OnStateUpdate()
        {
            base.OnStateUpdate();

            foreach (var i in _enemies)
            {
                if (!_enemies.Get3(i).OtherEntity.IsAlive()) continue;

                _enemies.Get3(i).OtherEntity.Get<DamageSignal>()
                    .Damage += _enemies.Get2(i).Attack;

                _enemies.Get3(i).OtherEntity.Get<KnockbackSignal>()
                    .Source = _enemies.Get1(i).Agent.transform.position;
            }
        }

    }
}
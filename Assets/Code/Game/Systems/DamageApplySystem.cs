using Leopotam.Ecs;
using alicewithalex.Game.Components;

namespace alicewithalex.Game.Systems
{
    public class DamageApplySystem : StateSystem<GameplayState>
    {
        private readonly EcsFilter<Health, Stats, DamageSignal> _health;

        protected override void OnStateUpdate()
        {
            base.OnStateUpdate();

            foreach (var i in _health)
            {
                if (_health.Get1(i).Take(CalculateDamage(
                    _health.Get3(i).Damage,
                    _health.Get2(i).Defense)))
                    continue;

                _health.GetEntity(i).Get<DestroySignal>()
                    .Destroyable = _health.Get1(i).Destroyable;
            }
        }


        private float CalculateDamage(float attack, float defense)
        {
            return attack * (100f / (100f + defense));
        }
    }
}
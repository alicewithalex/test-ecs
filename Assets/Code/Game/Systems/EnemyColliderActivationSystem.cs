using Leopotam.Ecs;
using alicewithalex.Game.Components;

namespace alicewithalex.Game.Systems
{
    public class EnemyColliderActivationSystem : StateSystem<GameplayState>
    {

        private readonly EcsFilter<Enemy, ActivateColliderSignal> _enemies;


        protected override void OnStateUpdate()
        {
            base.OnStateUpdate();

            foreach (var i in _enemies)
            {
                _enemies.Get1(i).Collider.enabled = true;
            }
        }

    }
}
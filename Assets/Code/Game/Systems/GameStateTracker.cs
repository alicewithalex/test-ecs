using Leopotam.Ecs;
using alicewithalex.Game.Components;

namespace alicewithalex.Game.Systems
{
    public class GameStateTracker : StateSystem<GameplayState>
    {
        private readonly EcsFilter<PlayerDestroyedSignal> _gameOver;

        private readonly StateMachine _stateMachine;

        protected override void OnStateUpdate()
        {
            base.OnStateUpdate();

            if (_gameOver.IsEmpty()) return;

            _stateMachine.SetState<LoseState>();
        }

    }
}
using Leopotam.Ecs;
using alicewithalex.Game.Components;

namespace alicewithalex.Game.Systems
{
    public class RestartSystem : StateSystem<LoadState>
    {
        private readonly EcsFilter<CleanupTag> _cleanup;
        private readonly StateMachine _stateMachine;

        protected override void OnStateEnter()
        {
            base.OnStateEnter();

            foreach (var i in _cleanup)
                _cleanup.GetEntity(i).Destroy();

            _stateMachine.SetState<MenuState>();
        }
    }
}
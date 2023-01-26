using Leopotam.Ecs;

namespace alicewithalex
{
    public class StateSwitchSystem : IEcsRunSystem
    {
        private readonly StateMachine _stateMachine;

        public StateSwitchSystem(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Run() => _stateMachine.Apply();
    }
}
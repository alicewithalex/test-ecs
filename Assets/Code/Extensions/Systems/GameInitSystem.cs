using Leopotam.Ecs;

namespace alicewithalex.Extensions.Systems
{
    public class GameInitSystem<T> : IEcsInitSystem where T : struct
    {
        private readonly StateMachine _stateMachine;

        public void Init()
        {
            _stateMachine.SetState<T>();
        }
    }
}
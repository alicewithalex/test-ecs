using alicewithalex.FancyDebug;
using Leopotam.Ecs;

namespace alicewithalex
{
    public class StateSystem<T> : IEcsRunSystem where T : struct
    {
        protected readonly EcsFilter<T, StateEnter> _stateEnter;
        protected readonly EcsFilter<T> _state;
        protected readonly EcsFilter<T, StateExit> _stateExit;

        protected readonly IEcsWorldHandler _worldHandler;

        public void Run()
        {
            if (_state.IsEmpty()) return;

            if (!_stateEnter.IsEmpty())
                OnStateEnter();

            OnStateUpdate();

            if (!_stateExit.IsEmpty())
                OnStateExit();
        }

        protected virtual void OnStateEnter()
        {
            FDebug.Log($"Enter @{typeof(T).Name}@ state.", '@',
                FColor.Mint);
        }

        protected virtual void OnStateUpdate()
        {
        }

        protected virtual void OnStateExit()
        {
            FDebug.Log($"Exit @{typeof(T).Name}@ state.", '@',
                FColor.Orange);
        }

        protected EcsEntity CreateEntity() => _worldHandler.CreateEntity();
    }

    public struct DefaultState : IEcsIgnoreInFilter { }
}
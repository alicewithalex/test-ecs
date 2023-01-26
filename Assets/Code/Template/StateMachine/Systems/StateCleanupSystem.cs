using Leopotam.Ecs;

namespace alicewithalex
{
    public class StateCleanupSystem : IEcsRunSystem
    {
        private readonly EcsFilter<State,StateExit> _stateExit;

        public void Run()
        {
            if (_stateExit.IsEmpty()) return;

            foreach (var i in _stateExit)
                _stateExit.GetEntity(i).Destroy();
        }
    }
}
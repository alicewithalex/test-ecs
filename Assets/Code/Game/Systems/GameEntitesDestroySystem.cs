using Leopotam.Ecs;
using alicewithalex.Game.Components;

namespace alicewithalex.Game.Systems
{
    public class GameEntitesDestroySystem : StateSystem<GameplayState>
    {
        private readonly EcsFilter<DestroySignal> _destroy;

        protected override void OnStateUpdate()
        {
            base.OnStateUpdate();

            foreach (var i in _destroy)
            {
                if (_destroy.Get1(i).Destroyable != null)
                {
                    _destroy.Get1(i).Destroyable.OnDestroy(_worldHandler);
                }

                _destroy.GetEntity(i).Destroy();
            }
        }

    }
}
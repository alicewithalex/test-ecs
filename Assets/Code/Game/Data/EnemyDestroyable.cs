using alicewithalex.Game.Components;
using Leopotam.Ecs;

namespace alicewithalex.Game.Data
{
    public class EnemyDestroyable : IDestroyable
    {
        public void OnDestroy(IEcsWorldHandler ecsWorldHandler)
        {
            ecsWorldHandler.CreateEntity().Get<EnemyDestroyedSignal>();
        }
    }
}
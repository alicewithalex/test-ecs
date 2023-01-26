using alicewithalex.Game.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace alicewithalex.Game.Data
{
    public class PlayerDestroyable : IDestroyable
    {
        public void OnDestroy(IEcsWorldHandler ecsWorldHandler)
        {
            ecsWorldHandler.CreateEntity().Get<PlayerDestroyedSignal>();
        }
    }
}
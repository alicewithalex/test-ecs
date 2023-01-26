using Leopotam.Ecs;
using UnityEngine;
using alicewithalex;
using alicewithalex.Game.Components;
using alicewithalex.Extensions.Components;
using alicewithalex.Game.Data;

namespace alicewithalex.Game.Systems
{
    public class RespawnSystem : StateSystem<GameplayState>
    {
        private readonly EcsFilter<Respawn, TriggerEnter> _respawn;


        protected override void OnStateUpdate()
        {
            base.OnStateUpdate();

            foreach (var i in _respawn)
            {
                _respawn.Get2(i).OtherEntity.Get<DestroySignal>()
                    .Destroyable = new PlayerDestroyable();
            }
        }

    }
}
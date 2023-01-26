using Leopotam.Ecs;
using UnityEngine;
using alicewithalex;

namespace alicewithalex.Game.Views
{
    public class RespawnView : ViewComponent
    {

         public override void Init(IEcsWorldHandler ecsWorldHandler, EcsEntity entity)
         {
            entity.Get<Components.Respawn>();
         }

    }
}
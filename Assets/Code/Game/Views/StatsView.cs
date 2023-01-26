using Leopotam.Ecs;
using UnityEngine;

namespace alicewithalex.Game.Views
{
    public class StatsView : ViewComponent
    {
        public override void Init(IEcsWorldHandler ecsWorldHandler, EcsEntity entity)
        {
            entity.Get<Components.Stats>();
        }
    }
}
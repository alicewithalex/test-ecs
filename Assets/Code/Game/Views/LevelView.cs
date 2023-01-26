using Leopotam.Ecs;
using UnityEngine;
using alicewithalex;
using alicewithalex.Game.Components;

namespace alicewithalex.Game.Views
{
    public class LevelView : ViewComponent
    {
        [SerializeField] private BoxCollider _levelBounds;

        public override void Init(IEcsWorldHandler ecsWorldHandler, EcsEntity entity)
        {
            entity.Get<Level>().Bounds = _levelBounds;
        }

    }
}
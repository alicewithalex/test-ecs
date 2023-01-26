using Leopotam.Ecs;
using UnityEngine;
using alicewithalex;

namespace alicewithalex.Game.Views
{
    public class CrosshairView : ViewComponent
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public override void Init(IEcsWorldHandler ecsWorldHandler, EcsEntity entity)
        {
            entity.Get<Components.Crosshair>()
                .SpriteRenderer = _spriteRenderer;
        }

    }
}
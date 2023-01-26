using Leopotam.Ecs;
using UnityEngine;
using alicewithalex;

namespace alicewithalex.Game.Views
{
    public class PlayerView : ViewComponent
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _grounRayOrigin;

        public override void Init(IEcsWorldHandler ecsWorldHandler,
            EcsEntity entity)
        {
            ref var player = ref entity.Get<Components.Player>();

            player.CharacterController = _characterController;
            player.GroundRayOrigin = _grounRayOrigin;
        }

    }
}
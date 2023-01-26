using Leopotam.Ecs;
using UnityEngine;
using Cinemachine;

namespace alicewithalex.Game.Views
{
    public class VirtualCameraView : ViewComponent
    {
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;

        public override void Init(IEcsWorldHandler ecsWorldHandler, EcsEntity entity)
         {
            entity.Get<Components.VirtualCamera>().Camera = _virtualCamera;
         }

    }
}
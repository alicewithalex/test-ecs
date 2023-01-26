using Leopotam.Ecs;
using alicewithalex.Game.Components;

namespace alicewithalex.Game.Systems
{
    public class CameraAssignSystem : StateSystem<MenuState>
    {
        private readonly EcsFilter<VirtualCamera> _camera;
        private readonly EcsFilter<UnityView, Player> _player;

        protected override void OnStateEnter()
        {
            base.OnStateEnter();

            foreach (var i in _camera)
            {
                foreach (var j in _player)
                {
                    _camera.Get1(i).Camera.Follow = _player.Get1(j).Transform;
                    _camera.Get1(i).Camera.LookAt = _player.Get1(j).Transform;
                }
            }
        }



    }
}
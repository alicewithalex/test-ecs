using Leopotam.Ecs;
using UnityEngine;
using alicewithalex.Game.Components;
using alicewithalex.Game.Data;

namespace alicewithalex.Game.Systems
{
    public class PlayerLookAroundSystem : StateSystem<GameplayState>
    {
        private readonly EcsFilter<UnityView, Player> _player;
        private readonly EcsFilter<UnityView, Level> _level;

        private readonly TimeService _timeService;
        private readonly PlayerMovementConfig _playerMovementConfig;

        private Camera _camera;
        private Plane _plane;
        private Ray _ray;

        private float _distance;
        private Vector3 _hitPoint;

        protected override void OnStateEnter()
        {
            base.OnStateEnter();

            _camera = SceneContainer.Instance.Container.Get<Camera>();

            foreach (var i in _level)
            {
                _plane = new Plane(Vector3.up, _level.Get1(i)
                    .Transform.position);
            }
        }

        protected override void OnStateUpdate()
        {
            base.OnStateUpdate();

            _ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (!_plane.Raycast(_ray, out _distance)) return;


            _hitPoint = _ray.GetPoint(_distance);


            foreach (var i in _player)
            {
                var transform = _player.Get1(i).Transform;

                var direction = (_hitPoint - transform.position).normalized;
                direction = Vector3.ProjectOnPlane(direction, Vector3.up);

                transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(direction, Vector3.up),
                    _timeService.DeltaTime *
                    _playerMovementConfig.RotationSpeed);
            }
        }

    }
}
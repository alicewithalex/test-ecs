using Leopotam.Ecs;
using UnityEngine;
using alicewithalex.Game.Components;
using alicewithalex.Game.Data;

namespace alicewithalex.Game.Systems
{
    public class PlayerMovementSystem : StateSystem<GameplayState>
    {
        private readonly EcsFilter<Player> _player;

        private readonly TimeService _timeService;
        private readonly PlayerMovementConfig _playerMovementConfig;

        private Camera _camera;
        private Vector2 _input;
        private Ray _ray;

        protected override void OnStateEnter()
        {
            base.OnStateEnter();

            _camera = SceneContainer.Instance.Container.Get<Camera>();
        }

        protected override void OnStateUpdate()
        {
            base.OnStateUpdate();

            _input = GetInput();
            foreach (var i in _player)
            {
                ref var player = ref _player.Get1(i);
                _ray = player.Ray;

                player.Velocity = Vector3.zero;

                if (_input.sqrMagnitude > Mathf.Epsilon)
                {
                    var forwardProjected = Vector3.ProjectOnPlane(
                            _camera.transform.TransformDirection(
                            Vector3.forward), Vector3.up).normalized;

                    player.Velocity = _camera.transform.right * _input.x +
                        forwardProjected * _input.y;
                    player.Velocity *= _playerMovementConfig.MovementSpeed;
                }

                if (Input.GetKeyDown(_playerMovementConfig.JumpKey) && Physics.Raycast(
                        _ray, _playerMovementConfig.GroundDistance))
                {
                    player.VelocityY = Mathf.Sqrt(2 *
                        _playerMovementConfig.Gravity *
                        _playerMovementConfig.JumpForce);
                }

                player.VelocityY -= _timeService.DeltaTime
                  * _playerMovementConfig.Gravity;

                player.CharacterController.Move(
                    (player.Velocity + Vector3.up * player.VelocityY +
                    player.KnockbackVelocity + player.DashVelocity) * _timeService.DeltaTime);

                if (player.CharacterController.isGrounded)
                    player.VelocityY = 0f;
            }
        }


        private Vector2 GetInput()
        {
            return new Vector2(Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical"));
        }
    }
}
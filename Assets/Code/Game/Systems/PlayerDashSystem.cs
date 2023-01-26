using Leopotam.Ecs;
using UnityEngine;
using alicewithalex;
using alicewithalex.Game.Components;
using alicewithalex.Game.Data;

namespace alicewithalex.Game.Systems
{
    public class PlayerDashSystem : StateSystem<GameplayState>
    {
        private readonly EcsFilter<UnityView, Player>.Exclude<DashTask> _player;
        private readonly EcsFilter<Player, DashTask> _dash;
        private readonly EcsFilter<UnityView, Crosshair>.Exclude<DisabledTag> _crosshair;

        private readonly TimeService _timeService;
        private readonly PlayerMovementConfig _playerMovementConfig;

        protected override void OnStateUpdate()
        {
            base.OnStateUpdate();

            if (Input.GetKeyDown(_playerMovementConfig.DashKey))
            {
                foreach (var i in _player)
                {
                    Vector3 direction = _player.Get1(i).Transform.forward;

                    foreach (var j in _crosshair)
                    {
                        direction = Vector3.ProjectOnPlane(
                            _crosshair.Get1(i).Transform.position -
                            _player.Get1(i).Transform.position,
                            Vector3.up).normalized;
                    }

                    _player.GetEntity(i).Get<DashTask>() =
                        new DashTask(direction * _playerMovementConfig.DashForce,
                        _playerMovementConfig.DashDuration);
                }


            }

            foreach (var i in _dash)
            {
                if (!_dash.Get2(i).Tick(_timeService.DeltaTime))
                {
                    _dash.Get1(i).DashVelocity = _dash.Get2(i).Velocity;
                }
                else
                {
                    _dash.Get1(i).DashVelocity = Vector3.zero;
                    _dash.GetEntity(i).Del<DashTask>();
                }
            }
        }

    }
}
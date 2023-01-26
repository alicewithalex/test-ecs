using Leopotam.Ecs;
using UnityEngine;
using alicewithalex.Game.Components;
using alicewithalex.Game.Data;

namespace alicewithalex.Game.Systems
{
    public class KnockbackSystem : StateSystem<GameplayState>
    {

        private readonly EcsFilter<UnityView, Player, KnockbackSignal> _player;

        private readonly EcsFilter<Player, KnockbackTask> _task;

        private readonly KnockbackConfig _knockbackConfig;
        private readonly TimeService _timeService;

        protected override void OnStateUpdate()
        {
            base.OnStateUpdate();

            OnKnockback();
            EvaluateTask();
        }

        private void OnKnockback()
        {
            foreach (var i in _player)
            {
                Vector3 direction = _player.Get1(i).Transform.position -
                    _player.Get3(i).Source;

                direction = Vector3.ProjectOnPlane(direction,
                    Vector3.up).normalized;

                _player.Get2(i).KnockbackVelocity =
                    direction * _knockbackConfig.Power;

                _player.GetEntity(i).Get<KnockbackTask>() =
                    new KnockbackTask(_player.Get2(i).KnockbackVelocity,
                    _knockbackConfig.Duration);
            }
        }

        private void EvaluateTask()
        {
            foreach (var i in _task)
            {
                if (_task.Get2(i).Tick(_timeService.DeltaTime))
                {
                    _task.Get1(i).KnockbackVelocity = Vector3.zero;
                    _task.GetEntity(i).Del<KnockbackTask>();
                }
                else
                {
                    if (_knockbackConfig.UseCurve)
                    {
                        _task.Get1(i).KnockbackVelocity =
                            _knockbackConfig.Curve.Evaluate(
                            _task.Get2(i).Percentage) *
                            _task.Get2(i).Knockback;
                    }
                    else
                    {
                        _task.Get1(i).KnockbackVelocity = _task.Get2(i).Knockback;
                    }
                }
            }
        }


    }
}
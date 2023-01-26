using Leopotam.Ecs;
using UnityEngine;
using alicewithalex;
using alicewithalex.Game.Components;
using alicewithalex.Game.Data;

namespace alicewithalex.Game.Systems
{
    public class PlayerStatsAssigner : StateSystem<GameplayState>
    {
        private readonly EcsFilter<Player, Stats> _stats;

        private readonly PlayerStatsConfig _playerStatsConfig;
        private readonly PlayerMovementConfig _playerMovementConfig;

        protected override void OnStateEnter()
        {
            base.OnStateEnter();

            foreach (var i in _stats)
            {
                ref var stats = ref _stats.Get2(i);

                stats.Speed = _playerMovementConfig.MovementSpeed;
                stats.Attack = _playerStatsConfig.Attack;
                stats.Defense = _playerStatsConfig.Defense;

                _stats.GetEntity(i).Get<Health>() = new Health(
                    _playerStatsConfig.Health,new PlayerDestroyable());
            }
        }
    }
}
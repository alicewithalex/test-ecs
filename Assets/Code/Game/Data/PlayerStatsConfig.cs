using Leopotam.Ecs;
using UnityEngine;

namespace alicewithalex.Game.Data
{
    [CreateAssetMenu(fileName = "PlayerStatsConfig", menuName = "alicewithalex/Game/Data/GameConfigs/PlayerStatsConfig")]
    public class PlayerStatsConfig : GameConfig
    {
        [Min(0)] public float Health =100;
        [Min(0)] public float Attack = 25;
        [Min(0)] public float Defense = 25;
    }
}
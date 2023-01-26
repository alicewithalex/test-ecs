using Leopotam.Ecs;
using UnityEngine;

namespace alicewithalex.Game.Data
{
    [CreateAssetMenu(fileName = "LevelsConfig", menuName = "alicewithalex/Game/Data/GameConfigs/LevelsConfig")]
    public class LevelsConfig : GameConfig
    {
        public ViewElement Level;
    }
}
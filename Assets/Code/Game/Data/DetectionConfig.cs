using Leopotam.Ecs;
using UnityEngine;

namespace alicewithalex.Game.Data
{
    [CreateAssetMenu(fileName = "DetectionConfig", menuName = "alicewithalex/Game/Data/GameConfigs/DetectionConfig")]
    public class DetectionConfig : GameConfig
    {
        public ViewElement Crosshair;

        public LayerMask Mask;
        public Color NormalColor;
        public Color DetectedColor;
    }
}
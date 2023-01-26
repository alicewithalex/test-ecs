using UnityEngine;

namespace alicewithalex.Game.Data
{
    [CreateAssetMenu(fileName = "KnockbackConfig", menuName = "alicewithalex/Game/Data/GameConfigs/KnockbackConfig")]
    public class KnockbackConfig : GameConfig
    {
        [Min(0)] public float Power;
        [Min(0)] public float Duration;

        public bool UseCurve;
        public AnimationCurve Curve;
    }
}
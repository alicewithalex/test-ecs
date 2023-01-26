using Leopotam.Ecs;
using UnityEngine;

namespace alicewithalex.Game.Data
{
    [System.Serializable]
    public class EnemySpawnData
    {
        public EnemyType EnemyType;

        [Space(4)]
        public ViewElement View;

        [Space(4)]
        [Min(0)] public float Health = 100;
        [Min(0)] public float Speed = 1;
        [Min(0)] public float Defense = 10;
        [Min(0)] public float Attack = 10;
    }
}
using UnityEngine;

namespace alicewithalex.Game.Data
{
   

    [CreateAssetMenu(fileName = "EnemySpawnerConfig", menuName = "alicewithalex/Game/Data/GameConfigs/EnemySpawnerConfig")]
    public class EnemySpawnerConfig : GameConfig
    {
        [Range(0, 256)] public int MaxEnemyAmount = 100;
        [Range(0f, 10f)] public float SpawnDelay = 2.5f;
        [Range(0f, 64f)] public float ExtraSpawnRadius = 10f;
    }
}
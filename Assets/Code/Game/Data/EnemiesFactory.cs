using System;
using System.Collections.Generic;
using System.Linq;

namespace alicewithalex.Game.Data
{
    public class EnemiesFactory
    {
        private readonly Dictionary<EnemyType, EnemySpawnData>
            _map;

        private readonly List<EnemyType> _hashedTypes;

        public EnemiesFactory(IList<EnemySpawnData> enemiesSpawnData)
        {
            if (enemiesSpawnData == null || enemiesSpawnData.Count == 0) return;

            _map = enemiesSpawnData.ToDictionary(x => x.EnemyType);
            _hashedTypes = _map.Keys.ToList();
        }

        public ViewElement Create(EnemyType enemyType)
        {
            if (!_map.ContainsKey(enemyType))
                throw new KeyNotFoundException();

            return _map[enemyType].View;
        }

        public ViewElement CreateRandom(out EnemyType type)
        {
            type = default;

            if (_map is null || _map.Count == 0)
                throw new NullReferenceException();

            type = _hashedTypes[UnityEngine.Random.Range(0,
                _hashedTypes.Count)];

            return _map[type].View;
        }

        public EnemySpawnData GetData(EnemyType enemyType)
        {
            if (!_map.ContainsKey(enemyType))
                throw new KeyNotFoundException();

            return _map[enemyType];
        }
    }
}
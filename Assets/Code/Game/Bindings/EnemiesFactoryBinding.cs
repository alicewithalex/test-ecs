using alicewithalex.Game.Data;
using System.Collections.Generic;
using UnityEngine;

namespace alicewithalex.Game.Bindings
{
    [CreateAssetMenu(fileName = "EnemiesFactoryBinding", menuName = "alicewithalex/Game/Bindings/Bindings/EnemiesFactoryBinding")]
    public class EnemiesFactoryBinding : ObjectBinding
    {
        [SerializeField]
        private List<EnemySpawnData> _enemiesSpawnData 
            = new List<EnemySpawnData>();

        public override void Bind(IContainer container)
        {
            container.Bind(new EnemiesFactory(_enemiesSpawnData));
        }
    }
}
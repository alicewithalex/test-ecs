using Leopotam.Ecs;
using UnityEngine;

namespace alicewithalex
{
    public class UnityEntity : MonoBehaviour
    {
        private EcsEntity _entity = EcsEntity.Null;

        public EcsEntity Entity
        {
            get => _entity;
            set
            {
                if (_entity != EcsEntity.Null) return;

                _entity = value;
            }
        }
    }
}
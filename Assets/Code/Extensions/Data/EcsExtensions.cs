using Leopotam.Ecs;
using UnityEngine;

namespace alicewithalex.Extensions.Data
{
    public static class EcsExtensions
    {
        public static bool TryGetEntity(this GameObject gameObject,
            out EcsEntity entity)
        {
            entity = EcsEntity.Null;

            if (!gameObject.TryGetComponent<UnityEntity>(out var component))
            {
                component = gameObject.GetComponentInParent<UnityEntity>();
                if (component == null) return false;

                entity = component.Entity;
            }
            else
            {
                entity = component.Entity;
            }

            return true;
        }
    }
}
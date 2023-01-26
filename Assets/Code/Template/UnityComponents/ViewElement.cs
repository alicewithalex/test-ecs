using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace alicewithalex
{
    public class ViewElement : MonoBehaviour
    {
        [SerializeField] private bool _selfInjection;
        [SerializeField]
        private List<ViewComponent> _viewComponents
            = new List<ViewComponent>();

        public async void Start()
        {
            if (!_selfInjection) return;

            await Until(EcsWorldStartup.IsInitialized);

            Init(EcsWorldStartup.EcsWorldHandler);
        }

        public EcsEntity Init(IEcsWorldHandler ecsWorldHandler)
        {
            EcsEntity entity = ecsWorldHandler.CreateEntity();

            OnInit(ecsWorldHandler, entity);

            foreach (var viewComponent in _viewComponents)
                viewComponent.Init(ecsWorldHandler, entity);

            Destroy(this);

            return entity;
        }

        protected virtual void OnInit(IEcsWorldHandler ecsWorldHandler, EcsEntity entity)
        {
            gameObject.AddComponent<UnityEntity>().Entity = entity;

            entity.Get<UnityView>() = new UnityView()
            {
                GameObject = gameObject,
                Transform = transform
            };
        }

        [Button("Collect Components")]
        private void CollectComponents()
        {
            _viewComponents = GetComponentsInChildren<ViewComponent>().ToList();

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        private async Task Until(Func<bool> func)
        {
            while (!func())
            {
                await Task.Yield();
            }
        }
    }
}
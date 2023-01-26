using System;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace alicewithalex
{
    public sealed class EcsStartup : MonoBehaviour
    {
        [SerializeField]
        private List<Object> _systemsProviders
            = new List<Object>();

        [SerializeField]
        private List<Object> _fixedSystemsProviders
          = new List<Object>();

        private IEcsWorldHandler _ecsWorldHandler;
        private EcsSystems _systems;
        private EcsSystems _fixedSystems;

        private bool _initialized;
        public bool Initialized => _initialized;

        public event Action OnDestroyEvent;

        public void PreInit(IEcsWorldHandler ecsWorldHandler)
        {
            SceneManager.sceneLoaded += InitSceneContainer;

            _ecsWorldHandler = ecsWorldHandler;

            DontDestroyOnLoad(gameObject);
        }


#if UNITY_EDITOR

        private EcsWorld _ecsWorld;

        public void SetDebugWorld(EcsWorld ecsWorld)
        {
            _ecsWorld = ecsWorld;
        }

#endif

        public void Init()
        {
            _systems = _ecsWorldHandler.CreateSystems("EcsSystems");
            _fixedSystems = _ecsWorldHandler.CreateSystems("EcsFixedSystems");

            #region Debuging

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_ecsWorld);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif
            #endregion

            PopulateSystems();
            CreateSystems();

            _initialized = true;
        }

        private void PopulateSystems()
        {
            TimeService timeService = new TimeService();

            _systems.Add(new UnityTimeTracker());

            _systems.Inject(timeService, typeof(TimeService));
            _systems.Inject(_ecsWorldHandler, typeof(IEcsWorldHandler));
            _systems.Inject(SceneContainer.Instance.Container.Get<UIHub>(), typeof(UIHub));

            _fixedSystems.Add(new UnityFixedTimeTracker());

            _fixedSystems.Inject(timeService, typeof(TimeService));
            _fixedSystems.Inject(_ecsWorldHandler, typeof(IEcsWorldHandler));
            _fixedSystems.Inject(SceneContainer.Instance.Container.Get<UIHub>(), typeof(UIHub));
        }

        private void CreateSystems()
        {
            EcsSystems endFrame = _ecsWorldHandler.CreateSystems(_systems.Name + " EndFrame");
            EcsSystems fixedEndFrame = _ecsWorldHandler.CreateSystems(_fixedSystems.Name + " EndFrame");

            foreach (var provider in _systemsProviders)
            {
                if (provider is not ISystemsProvider systemsProvider) continue;

                _systems.Add(systemsProvider.GetSystems(_systems, endFrame, _ecsWorldHandler));
            }

            foreach (var provider in _fixedSystemsProviders)
            {
                if (provider is not ISystemsProvider systemsProvider) continue;

                _fixedSystems.Add(systemsProvider.GetSystems(_fixedSystems, fixedEndFrame, _ecsWorldHandler));
            }

            _systems.Add(endFrame);
            _systems.Init();

            _fixedSystems.Add(fixedEndFrame);
            _fixedSystems.Init();
        }

        private void InitSceneContainer(Scene scene, LoadSceneMode mode)
        {
            SceneContainer sceneContainer = null;
            foreach (var root in scene.GetRootGameObjects())
            {
                if (root.TryGetComponent<SceneContainer>(out sceneContainer))
                    break;
            }

            sceneContainer.Init();
        }

        private void Update()
        {
            if (_initialized is false) return;
            _systems?.Run();
        }

        private void FixedUpdate()
        {
            if (_initialized is false) return;
            _fixedSystems?.Run();
        }

        private void OnDestroy()
        {
            if (_systems == null) return;

            SceneManager.sceneLoaded -= InitSceneContainer;

            _systems.Destroy();
            _systems = null;

            OnDestroyEvent?.Invoke();
        }
    }
}
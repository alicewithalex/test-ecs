using Leopotam.Ecs;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace alicewithalex
{
    public static class EcsWorldStartup
    {
        const string StartupFolderPath = "EcsTemplate";

        public static IEcsWorldHandler EcsWorldHandler { get; private set; }


        private static EcsStartup _ecsStartup;


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static async void Init()
        {
            await HandleEntryPoint();

            _ecsStartup = HandleStartup();

            SceneLoader.LoadScene(1, _ecsStartup.Init);
        }

        public static bool IsInitialized()
        {
            return _ecsStartup.Initialized;
        }

        private static async Task HandleEntryPoint()
        {
            if (SceneManager.GetActiveScene().buildIndex == 0) return;

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(0);
            asyncOperation.allowSceneActivation = false;
            while (true)
            {
                if (asyncOperation.isDone)
                    break;

                await Task.Yield();
            }

            asyncOperation.allowSceneActivation = true;
        }

        private static EcsStartup HandleStartup()
        {
            EcsStartup ecsStartup = Object.Instantiate(LoadEcsStartup(), null);

#if UNITY_EDITOR

            var ecsWorld = new EcsWorld();
            var ecsWorldHandler = new EcsWorldHandler(ecsWorld);

            ecsStartup.SetDebugWorld(ecsWorld);
#else
            var ecsWorldHandler = new EcsWorldHandler();
            _ecsWorldHandler = new EcsWorldHandler();
#endif

            EcsWorldHandler = ecsWorldHandler;
            ecsStartup.OnDestroyEvent += ecsWorldHandler.Destroy;
            ecsStartup.PreInit(EcsWorldHandler);

            return ecsStartup;
        }

        private static EcsStartup LoadEcsStartup()
        {
            try
            {
                return Resources.LoadAll<EcsStartup>(StartupFolderPath)[0];
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace alicewithalex
{
    public class SceneContainer : MonoBehaviour
    {
        [SerializeField] private List<MonoBinding> _monoBindings = new List<MonoBinding>();
        [SerializeField] private List<ObjectBinding> _objectBindings = new List<ObjectBinding>();

        private static SceneContainer _instance;
        public static SceneContainer Instance;

        public IContainer Container { get; private set; }
        private bool _initialized;

        public void Init()
        {
            InitSingleton();

            if (_initialized) return;

            Container = new Container();

            foreach (var i in _monoBindings)
                i.Bind(Container);

            foreach (var i in _objectBindings)
                i.Bind(Container);

            _initialized = true;
        }

        private void InitSingleton()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(Instance.gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public void OnDestroy()
        {
            Instance = null;
        }

        [Button("Collect Bindings")]
        private void CollectBindings()
        {
            _monoBindings = FindObjectsOfType<MonoBinding>().ToList();
            _objectBindings = FindObjectsOfType<ObjectBinding>().ToList();

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }
}
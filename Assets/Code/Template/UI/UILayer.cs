using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace alicewithalex
{
    [System.Serializable]
    public class UILayer : MonoBehaviour, IEnumerable<UIScreen>
    {
        [SerializeField] private LayerType _layerType;

        [Space(8)]
        [SerializeField]
        private List<UIScreen> _screens
            = new List<UIScreen>();

        private Dictionary<Type, UIScreen> _screenMap;

        public LayerType Type => _layerType;

        public bool Active
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public void Initialize()
        {
            if (_screenMap != null) return;

            _screenMap = new Dictionary<Type, UIScreen>();

            Type type;
            foreach (var screen in _screens)
            {
                type = screen.GetType();
                if (!_screenMap.ContainsKey(type))
                {
                    screen.Initialize(this);
                    _screenMap.Add(type, screen);
                }
                else
                {
                    throw new ArgumentException($"{type.Name} " +
                           $"is already in UI Hub!");
                }
            }

            _screens.Clear();

            Active = false;
        }

        public void ShowScreen<T>(float duration = 0f)
            where T : UIScreen
        {
            var type = typeof(T);
            if (!_screenMap.TryGetValue(type, out var screen)) return;

            screen.Show(duration);
        }

        public void HideScreen<T>(float duration = 0f)
            where T : UIScreen
        {
            var type = typeof(T);
            if (!_screenMap.TryGetValue(type, out var screen)) return;

            screen.Hide(duration);
        }

        public void ToggleAllScreens(bool value)
        {
            if (value)
            {
                foreach (var screen in _screenMap)
                    screen.Value.Show(0);
            }
            else
            {
                foreach (var screen in _screenMap)
                    screen.Value.Hide(0);
            }
        }

        public T GetScreen<T>() where T : UIScreen
        {
            var type = typeof(T);
            if (!_screenMap.ContainsKey(type)) throw new KeyNotFoundException(
                 $"There are no UI Screen for {type.Name} screen");

            return _screenMap[type] as T;
        }


        public IEnumerator<UIScreen> GetEnumerator()
        {
            return _screens.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }


        #region Editor

        [Button("Collect")]
        public void CollectEditor()
        {
#if UNITY_EDITOR

            _screens = GetComponentsInChildren<UIScreen>(true).ToList();
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        public void ShowScreenEditor(ScreenType type)
        {
#if UNITY_EDITOR

            foreach (var screen in _screens)
            {
                if (!screen.Type.Equals(type))
                {
                    screen.Hide(0f);
                }
                else
                {
                    screen.Show(0f);
                }
            }

            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        public void HideAllEditor()
        {
#if UNITY_EDITOR

            foreach (var screen in _screens)
            {
                    screen.Hide(0f);
            }

            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        #endregion
    }
}
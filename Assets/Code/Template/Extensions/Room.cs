using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace alicewithalex
{
    public class Room<T> : MonoBehaviour where T : Component
    { 
        [SerializeField] protected Vector3 _bounds = Vector3.one;

        [Space(8)]
        [SerializeField] protected List<T> _items = new List<T>();

        public event Action<T> OnShow;
        public event Action<T> OnHide;

        protected T _current;
        protected int _currentIndex;

        public Vector3 Bounds => _bounds;

        public void Initialize()
        {
            _current = null;
            _currentIndex = -1;

            Toggle(false);
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void Show(int element, bool loopOver = false)
        {
            if (!Contains(element) && !loopOver) return;


            if (_current != null)
            {
                OnHide?.Invoke(_current);
                _current.gameObject.SetActive(false);
            }

            _currentIndex = LoopIndex(element);
            _current = _items[_currentIndex];

            OnShow?.Invoke(_current);

            _current.gameObject.SetActive(true);
        }

        public void Hide(int element, bool loopOver = false)
        {
            if (!Contains(element) && !loopOver) return;

            var item = _items[LoopIndex(element)];

            OnHide?.Invoke(item);

            item.gameObject.SetActive(true);
        }

        public void Hide()
        {
            if (_current == null) return;

            OnHide?.Invoke(_current);
            _current.gameObject.SetActive(false);

            _current = null;
        }

        [Button("Next")]

        public void Next()
        {
            Hide();
            Show(++_currentIndex, true);
        }

        [Button("Previous")]
        public void Previous()
        {
            Hide();
            Show(--_currentIndex, true);
        }

        [Button("Collect")]
        private void Collect()
        {
#if UNITY_EDITOR

            _items = GetComponentsInChildren<T>(true)
                .Where(x => x.gameObject != this.gameObject)
                .ToList();

            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        private void Toggle(bool value)
        {
            foreach (var item in _items)
                item.gameObject.SetActive(value);
        }

        [Button("Hide All")]
        private void HideAll()
        {
            Toggle(false);
        }

        private bool Contains(int i) => i >= 0 && i < _items.Count;

        private int LoopIndex(int i)
        {
            if (i < 0)
            {
                return _items.Count + i;
            }
            else
            {
                return i % _items.Count;
            }
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, _bounds);
        }
    }
}
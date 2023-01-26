using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

namespace alicewithalex
{
    public class UIHub : MonoBehaviour
    {
        [SerializeField] private LayerType _previewLayer;
        [SerializeField] private ScreenType _previewScreen;

        [Space(8)]
        [SerializeField] private List<UILayer> _layers = new List<UILayer>();

        private Dictionary<LayerType, UILayer> _layersMap;

        public void Initialize()
        {
            if (_layersMap != null) return;

            _layersMap = new Dictionary<LayerType, UILayer>();

            Type type;
            foreach (var layer in _layers)
            {
                if (!_layersMap.ContainsKey(layer.Type))
                {
                    _layersMap.Add(layer.Type, layer);
                    layer.Initialize();
                }
                else
                {
                    throw new ArgumentException($"{layer.Type} " +
                        $"is already in UI Hub!");
                }
            }

            _layers.Clear();

            ToggleAll(false);
        }

        public T GetScreen<T>(LayerType layer)
           where T : UIScreen
        {
            if (!_layersMap.ContainsKey(layer)) throw new KeyNotFoundException(
                 $"There are no UI Screen for {layer} layer");

            return _layersMap[layer].GetScreen<T>();
        }

        public UILayer GetLayer(LayerType layer)
        {
            if (!_layersMap.ContainsKey(layer)) throw new KeyNotFoundException(
                 $"There are no UI Screen for {layer} layer");

            return _layersMap[layer];
        }

        private void ToggleAll(bool value)
        {
            foreach (var layer in _layersMap.Values)
            {
                layer.Active = false;
                layer.ToggleAllScreens(value);
            }
        }


        #region Editor

        [Button("Collect")]
        private void Collect()
        {
            _layers = GetComponentsInChildren<UILayer>(true).ToList();

            foreach (var layer in _layers)
            {
                layer.CollectEditor();
            }

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        [Button("Show Preview")]
        private void ShowPreview()
        {
            foreach (var layer in _layers)
            {
                if (!layer.Type.Equals(_previewLayer))
                {
                    layer.Active = false;
                    layer.HideAllEditor();
                }
                else
                {
                    layer.Active = true;
                    layer.ShowScreenEditor(_previewScreen);
                }
            }

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif

        }

        [Button("Hide All")]
        private void HideAll()
        {
            foreach (var layer in _layers)
            {
                layer.Active = false;
                layer.HideAllEditor();
            }

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }


        #endregion
    }

}
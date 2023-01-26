using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace alicewithalex
{
    [UnityEditor.CustomEditor(typeof(Object),true)]
    public class CustomEditor : Editor
    {
        private MethodInfo [] _methods;

        private void OnEnable()
        {
            _methods = ReflectorEditor.Reflect(target.GetType());
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (_methods is null) return;

            foreach (var methodInfo in _methods)
            {
                Button(target, methodInfo);
            }
        }

        private void Button(Object target, MethodInfo info)
        {
            if (target is null || info.GetParameters().Any(x => !x.IsOptional))
            {
                return;
            }

            var methodName = ((ButtonAttribute)info.GetCustomAttribute(typeof(ButtonAttribute))).Label;

            if (GUILayout.Button(methodName))
            {
                object[] defaultParams = info.GetParameters().Select(x => x.DefaultValue).ToArray();
                info.Invoke(target, defaultParams);

                if(Application.isPlaying == false)
                {
                    EditorUtility.SetDirty(target);
                }
            }
        }
    }
}
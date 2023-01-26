using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class ReflectorEditor
{
    private static Type  _buttonType = typeof(alicewithalex.ButtonAttribute);
    private static readonly Dictionary<Type, MethodInfo[]> _cachedMethodsInfo = 
        new Dictionary<Type, MethodInfo[]>();
    private static readonly List<MethodInfo> _reusableList = new List<MethodInfo>(64);

    public static MethodInfo[] Reflect(Type type)
    {
        if (_cachedMethodsInfo.ContainsKey(type)) return _cachedMethodsInfo[type];

        List<Type> types = new List<Type>();

        while (type != null)
        {
            types.Add(type);

            var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Static |
                BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly);

            if (methods.Length == 0)
            {
                type = type.BaseType;
                continue;
            }

            var method = default(MethodInfo);
            for (int methodIndex = 0; methodIndex < methods.Length; methodIndex++)
            {
                method = methods[methodIndex];

                if (method.IsDefined(_buttonType, false))
                {
                    _reusableList.Add(method);
                }
            }

            _cachedMethodsInfo[type] = _reusableList.ToArray();
            _reusableList.Clear();

            type = type.BaseType;
        }

        return types
            .Where(_cachedMethodsInfo.ContainsKey)
            .Select(x => _cachedMethodsInfo[x])
            .ToArray();
    }

    private static T [] ToArray<T>(this IEnumerable<T[]> array)
    {
        List<T> data = new List<T>();

        foreach (var item in array)
        {
            foreach (var i in item)
            {
                data.Add(i);
            }
        }

        return data.ToArray();
    } 
}

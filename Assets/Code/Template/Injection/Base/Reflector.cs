using System;
using System.Collections.Generic;
using System.Reflection;

namespace alicewithalex
{
    public static class Reflector
    {
        private static readonly Type _injectAttributeType = typeof(Inject);
        private static readonly Dictionary<Type, FieldInfo[]> _cachedFieldInfos = 
            new Dictionary<Type, FieldInfo[]>();
        private static readonly List<FieldInfo> _reusableList = new List<FieldInfo>(512);

        public static FieldInfo[] Reflect(Type type)
        {
            if (_cachedFieldInfos.ContainsKey(type)) return _cachedFieldInfos[type];

            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.Instance | BindingFlags.FlattenHierarchy);

            if (fields.Length == 0) return null;

            var field = default(FieldInfo);
            for (int fieldIndex = 0; fieldIndex < fields.Length; fieldIndex++)
            {
                field = fields[fieldIndex];

                if (field.IsDefined(_injectAttributeType, false))
                {
                    _reusableList.Add(field);
                }
            }

            _cachedFieldInfos[type] = _reusableList.ToArray();
            _reusableList.Clear();

            return _cachedFieldInfos[type];
        }
    }
}
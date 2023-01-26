using System;
using System.Collections.Generic;
using System.Reflection;

namespace alicewithalex
{
    public static class Injector
    {
        public const int DEFAULT_RECURSION_LIMIT = 5;

        public static void Inject(this object target, object injectable, int recursion = 1) =>
            InjectInternal(target, target.GetType(), injectable, injectable.GetType(), recursion);

        public static void Inject<T0, T1>(this T0 target, T1 injectable, int recursion = 1) =>
           InjectInternal(target, typeof(T0), injectable, typeof(T1), recursion);

        public static void InjectCollection<T0>(this IList<T0> collection, object injectable, int recursion = 1)
        {
            var type0 = typeof(T0);
            var type1 = injectable.GetType();

            foreach (var item in collection)
                InjectInternal(item, type0, injectable, type1, recursion);
        }

        public static void InjectCollection<T0, T1>(this IList<T0> collection, T1 injectable, int recursion = 1)
        {
            var type0 = typeof(T0);
            var type1 = typeof(T1);

            foreach (var item in collection)
                InjectInternal(item, type0, injectable, type1, recursion);
        }

        public static void ContainerInject(this object target, Type targetType, IContainer container, int recursion = 1)
        {
            if (container is null) return;

            if (recursion < 1) recursion = 1;
            if (recursion > DEFAULT_RECURSION_LIMIT) recursion = DEFAULT_RECURSION_LIMIT;

            FieldInfo[] fieldInfos = null;

            while (recursion > 0 || targetType is null)
            {
                if ((fieldInfos = Reflector.Reflect(targetType)) is null) continue;

                for (int i = 0; i < fieldInfos.Length; i++)
                    if (container.TryGet(fieldInfos[i].FieldType, out var obj))
                        fieldInfos[i].SetValue(target, obj);

                targetType = targetType.BaseType;
                recursion--;
            }
        }

        private static void InjectInternal(object target, Type targetType,
            object injectable, Type injectableType, int recursion = 1)
        {
            if (recursion < 1) recursion = 1;
            if (recursion > DEFAULT_RECURSION_LIMIT) recursion = DEFAULT_RECURSION_LIMIT;

            FieldInfo[] fieldInfos = null;

            while (recursion > 0 || targetType is null)
            {
                if ((fieldInfos = Reflector.Reflect(targetType)) is null) continue;

                for (int i = 0; i < fieldInfos.Length; i++)
                    if (fieldInfos[i].FieldType == injectableType)
                        fieldInfos[i].SetValue(target, injectable);

                targetType = targetType.BaseType;
                recursion--;
            }
        }

    }
}
using System;
using System.Collections.Generic;

namespace alicewithalex
{
    public class Container : IContainer
    {
        private readonly Dictionary<Type, object> _objects;

        public Container()
        {
            _objects = new Dictionary<Type, object>();
        }

        public void Bind(object obj, Type type = null)
        {
            if (_objects.ContainsKey(type)) return;
            _objects.Add(type, obj);
        }

        public void Bind<T>(T obj)
        {
            Bind(obj, typeof(T));
        }

        public void Inject(object toInject, int deepness = 1, Type type = null)
        {
            toInject.ContainerInject(type is null ? toInject.GetType() : type, this, deepness);
        }

        public void Inject<T>(object toInject, int deepness = 1)
        {
            toInject.ContainerInject(typeof(T), this, deepness);
        }

        public T Get<T>()
        {
            TryGet(typeof(T), out var obj);
            return (T)obj;
        }

        public bool TryGet(Type type, out object obj)
        {
            obj = null;
            if (!_objects.ContainsKey(type)) return false;

            obj = _objects[type];
            return true;
        }

        public bool TryGet<T>(out T obj)
        {
            var type = typeof(T);
            obj = default(T);
            if (!_objects.ContainsKey(type)) return false;

            obj = (T)_objects[type];
            return true;
        }
    }
}
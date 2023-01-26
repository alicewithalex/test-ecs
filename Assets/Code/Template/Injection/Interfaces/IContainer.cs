using System;

namespace alicewithalex
{
    public interface IContainer
    {
        void Bind<T>(T obj);

        void Bind(object obj, Type type = null);

        T Get<T>();

        bool TryGet<T>(out T obj);

        bool TryGet(Type type, out object obj);

        void Inject<T>(object toInject, int deepness = 1);

        void Inject(object toInject, int deepness = 1, Type type = null);
    }
}
using UnityEngine;

namespace alicewithalex
{
    public abstract class ObjectBinding : ScriptableObject, IBinding
    {
        public abstract void Bind(IContainer container);
    }
}
using UnityEngine;

namespace alicewithalex
{
    public abstract class MonoBinding : MonoBehaviour, IBinding
    {
        public abstract void Bind(IContainer container);
    }
}
using alicewithalex;
using UnityEngine;

namespace alicewithalex.Extensions.Bindings
{
    [RequireComponent(typeof(UIHub))]
    public class UIHubBinding : MonoBinding
    {

        public override void Bind(IContainer container)
        {
            var hub = GetComponent<UIHub>();
            hub.Initialize();
            container.Bind(hub);
        }
    }
}
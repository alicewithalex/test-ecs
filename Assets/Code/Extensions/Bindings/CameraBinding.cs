using alicewithalex;
using UnityEngine;

namespace alicewithalex.Extensions.Bindings
{
    [RequireComponent(typeof(Camera))]
    public class CameraBinding : MonoBinding
    {

        public override void Bind(IContainer container)
        {
            container.Bind(GetComponent<Camera>());
        }
    }
}
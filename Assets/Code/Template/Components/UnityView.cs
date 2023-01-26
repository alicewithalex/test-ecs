using Leopotam.Ecs;
using UnityEngine;

namespace alicewithalex
{
    public struct UnityView : IEcsAutoReset<UnityView>
    {
        public Transform Transform;
        public GameObject GameObject;

        public void AutoReset(ref UnityView c)
        {
            if (c.GameObject != null)
                Object.Destroy(c.GameObject);

            c.GameObject = null;
            c.Transform = null;
        }
    }
}
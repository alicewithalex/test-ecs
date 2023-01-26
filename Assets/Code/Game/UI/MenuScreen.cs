using UnityEngine;
using UnityEngine.UI;

namespace alicewithalex.Game.UI
{
    public class MenuScreen : UIScreen
    {
        [field: Header("Menu Screen")]
        [field: SerializeField]
        public Button PlayButton { get; private set; }

        [field: SerializeField]
        public Button ExitButton { get; private set; }
    }
}
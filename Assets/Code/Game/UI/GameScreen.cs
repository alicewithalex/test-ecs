using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace alicewithalex.Game.UI
{
    public class GameScreen : UIScreen
    {

        [field: Header("Game Screen")]
        [field: SerializeField]
        public TextMeshProUGUI ScoreText { get; private set; }

        [field: SerializeField]
        public Image Healthbar { get; private set; }
    }
}
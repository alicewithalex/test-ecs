using alicewithalex;
using Leopotam.Ecs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace alicewithalex.Game.UI
{
    public class LoseScreen : UIScreen
    {
        [field: Header("Lose Screen")]
        [field: SerializeField]
        public TextMeshProUGUI ScoreText { get; private set; }

        [field: SerializeField]
        public Button RestartButton { get; private set; }
    }
}
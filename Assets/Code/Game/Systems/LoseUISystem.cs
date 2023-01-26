using alicewithalex;
using alicewithalex.Game.Components;
using alicewithalex.Game.UI;
using Leopotam.Ecs;
using System;
using UnityEngine;

namespace alicewithalex.Game.Systems
{
    public class LoseUISystem : UIStateSystem<LoseState, LoseScreen>
    {
        protected override LayerType LayerType => LayerType.Game;

        private readonly EcsFilter<Score> _score;

        private readonly StateMachine _stateMachine;

        protected override void OnStateEnter()
        {
            base.OnStateEnter();

            foreach (var i in _score)
                Screen.ScoreText.text = $"Your Score:{_score.Get1(i).Value}";

            Screen.RestartButton.onClick.AddListener(OnRestartPressed);

            Screen.Show();

            Cursor.visible = true;
        }


        protected override void OnStateExit()
        {
            base.OnStateExit();

            Screen.RestartButton.onClick.RemoveListener(OnRestartPressed);

            Screen.Hide();
        }


        private void OnRestartPressed()
        {
            _stateMachine.SetState<LoadState>();
        }
    }
}
using alicewithalex;
using alicewithalex.Game.UI;
using System;
using UnityEditor;
using UnityEngine;

namespace alicewithalex.Game.Systems
{
    public class MenuUISystem : UIStateSystem<MenuState, MenuScreen>
    {
        protected override LayerType LayerType => LayerType.Menu;

        private readonly StateMachine _stateMachine;

        protected override void OnStateEnter()
        {
            base.OnStateEnter();

            Screen.PlayButton.onClick.AddListener(OnPlayPressed);
            Screen.ExitButton.onClick.AddListener(OnExitPressed);

            Screen.Show(0.15f, DG.Tweening.Ease.Linear);

            Cursor.visible = true;
        }


        protected override void OnStateExit()
        {
            base.OnStateExit();

            Screen.PlayButton.onClick.RemoveListener(OnPlayPressed);
            Screen.ExitButton.onClick.RemoveListener(OnExitPressed);

            Screen.Hide(0.15f, DG.Tweening.Ease.Linear);

            Cursor.visible = false;
        }


        private void OnPlayPressed()
        {
            _stateMachine.SetState<GameplayState>();
        }

        private void OnExitPressed()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
    }
}
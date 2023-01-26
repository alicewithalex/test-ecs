using alicewithalex.Game.Components;
using alicewithalex.Game.UI;
using Leopotam.Ecs;

namespace alicewithalex.Game.Systems
{
    public class PlayerUISystem : UIStateSystem<GameplayState, GameScreen>
    {
        protected override LayerType LayerType => LayerType.Game;

        private readonly EcsFilter<Player, Health> _health;
        private readonly EcsFilter<Player, Health, DamageSignal> _damage;
        private readonly EcsFilter<EnemyDestroyedSignal> _scoreSignal;

        private readonly EcsFilter<Score> _scoreFilter;

        private readonly EcsWorld _ecsWorld;

        private int _score;

        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                Screen.ScoreText.text = $"Score:{_score}";

                foreach (var i in _scoreFilter)
                    _scoreFilter.Get1(i).Value = _score;
            }
        }


        protected override void OnStateEnter()
        {
            base.OnStateEnter();

            foreach (var i in _health)
                Screen.Healthbar.fillAmount = _health.Get2(i).Percentage;

            if (_scoreFilter.IsEmpty())
                _ecsWorld.NewEntity().Get<Score>();

            Score = 0;
            Screen.Show();
        }

        protected override void OnStateUpdate()
        {
            base.OnStateUpdate();

            foreach (var i in _damage)
                Screen.Healthbar.fillAmount = _damage.Get2(i).Percentage;

            foreach (var i in _scoreSignal)
                Score += 1;
        }

        protected override void OnStateExit()
        {
            base.OnStateExit();

            Screen.Hide();
        }
    }
}
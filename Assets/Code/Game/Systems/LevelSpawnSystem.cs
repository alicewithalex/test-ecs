using Leopotam.Ecs;
using UnityEngine;
using alicewithalex.Game.Data;
using alicewithalex.Game.Components;

namespace alicewithalex.Game.Systems
{
    public class LevelSpawnSystem : StateSystem<MenuState>
    {
        private readonly LevelsConfig _levelsConfig;

        protected override void OnStateEnter()
        {
            base.OnStateEnter();

            var level = Object.Instantiate(_levelsConfig.Level);
            level.Init(_worldHandler).Get<CleanupTag>();

            foreach (var view in level.GetComponentsInChildren<ViewElement>())
            {
                if (view == level) continue;

                view.Init(_worldHandler).Get<CleanupTag>();
            }
        }
    }
}
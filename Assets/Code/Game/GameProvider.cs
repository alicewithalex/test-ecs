using alicewithalex.Game.Data;
using Leopotam.Ecs;
using UnityEngine;

namespace alicewithalex.Game
{
    [CreateAssetMenu(menuName = "alicewithalex/Game/Providers/GameProvider",
         fileName = "GameProvider")]
    public class GameProvider : ObjectSystemsProvider
    {
        [SerializeField] private GameConfig[] _gameConfigs;

        public override EcsSystems GetSystems(EcsSystems current,
            EcsSystems endFrame, IEcsWorldHandler ecsWorldHandler)
        {

            EcsSystems systems = ecsWorldHandler.CreateSystems(
                    $"{GetType().Name} Provider");

            systems

                //Game Init
                .Add(new Extensions.Systems.GameInitSystem<MenuState>())
                .Add(new Systems.RestartSystem())
                .Add(new Systems.LevelSpawnSystem())
                .Add(new Systems.CameraAssignSystem())

                //UI
                .Add(new Systems.MenuUISystem())
                .Add(new Systems.LoseUISystem())

                //Player Related
                .Add(new Systems.PlayerStatsAssigner())
                .Add(new Systems.PlayerDashSystem())
                .Add(new Systems.PlayerMovementSystem())
                .Add(new Systems.PlayerLookAroundSystem())

                //Enemies
                .Add(new Systems.EnemyDetectionSystem())
                .Add(new Systems.EnemySpawnerSystem())
                .Add(new Systems.EnemyColliderActivationSystem())
                .Add(new Systems.EnemyRestartPoolSystem())
                .Add(new Systems.EnemyStatsAssigner())
                .Add(new Systems.EnemyNavigationSystem())

                .Add(new Systems.EnemyCollisionListener())

                .Add(new Systems.PlayerShootingSystem())
                .Add(new Systems.ProjectileCollisionSystem())
                .Add(new Systems.ProjectilePoolSystem())
                .Add(new Systems.ProjectileRestartPoolSystem())

                .Add(new Systems.DamageApplySystem())

                .Add(new Systems.KnockbackSystem())
                .Add(new Systems.RespawnSystem())

                .OneFrame<Components.PlayerDestroyedSignal>()
                .OneFrame<Components.EnemyDestroyedSignal>()
                .Add(new Systems.GameEntitesDestroySystem())

                .Add(new Systems.PlayerUISystem())

                .Add(new Systems.GameStateTracker())

                ;

            endFrame

                //Physics
                .OneFrame<Extensions.Components.TriggerEnter>()

                //Custom
                .OneFrame<Components.ActivateColliderSignal>()
                .OneFrame<Components.PoolSignal>()
                .OneFrame<Components.DamageSignal>()
                .OneFrame<Components.AssignStatsSignal>()
                .OneFrame<Components.KnockbackSignal>()

                ;

            foreach (var config in _gameConfigs)
                systems.Inject(config);

            return systems;
        }
    }
}
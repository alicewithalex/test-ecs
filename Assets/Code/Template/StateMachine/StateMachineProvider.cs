using Leopotam.Ecs;

namespace alicewithalex
{
    public class StateMachineProvider : SystemsProvider
    {
        public override EcsSystems GetSystems(EcsSystems current, EcsSystems endFrame, 
            IEcsWorldHandler ecsWorldHandler)
        {
            var stateMachine = new StateMachine(ecsWorldHandler);
            current.Inject(stateMachine);

            EcsSystems systems = ecsWorldHandler.CreateSystems("StateMachine Systems");

            systems

                .OneFrame<StateEnter>()
                .Add(new StateCleanupSystem())
                .Add(new StateSwitchSystem(stateMachine))

                ;

            return systems;
        }
    }
}
using Leopotam.Ecs;

namespace alicewithalex
{
    public sealed class StateMachine
    {
        private readonly IEcsWorldHandler _ecsWorldHandler;

        private EcsEntity _entity;
        private StateTransition _transition;

        private ISetStateCommand _command;
        private bool _execute;

        public StateMachine(IEcsWorldHandler ecsWorldHandler)
        {
            _ecsWorldHandler = ecsWorldHandler;
            _entity = EcsEntity.Null;
        }

        public void SetState<T>() where T : struct
        {
            _command = new SetStateCommand<T>();
            _execute = true;

            if (_entity != EcsEntity.Null)
            {
                _transition = StateTransition.Exit;
            }
            else
            {
                _transition = StateTransition.Enter;
            }
        }

        public void Apply()
        {
            if (_execute is false) return;

            _command.Execute(this);

            if (_transition.Equals(StateTransition.Enter)) return;

            _execute = false;
            _command = null;
        }

        public void Apply<T>() where T : struct
        {
            switch (_transition)
            {
                case StateTransition.Enter:
                    {
                        _entity = _ecsWorldHandler.CreateEntity();
                        _entity.Get<State>();
                        _entity.Get<T>();
                        _entity.Get<StateEnter>();

                        _transition = StateTransition.None;
                        break;
                    }
                case StateTransition.Exit:
                    {
                        _entity.Get<StateExit>();
                        _transition = StateTransition.Enter;
                        break;
                    }
                default: break;
            }
        }
    }

    #region Internals

    internal class SetStateCommand<T> : ISetStateCommand where T : struct
    {
        public void Execute(StateMachine stateMachine) => stateMachine.Apply<T>();
    }

    internal interface ISetStateCommand
    {
        public void Execute(StateMachine stateMachine);
    }

    internal enum StateTransition
    {
        None,
        Enter,
        Exit
    }

    #endregion

}
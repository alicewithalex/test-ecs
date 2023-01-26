using alicewithalex;
using UnityEngine;

public class LoadSystem : StateSystem<LoadState>
{
    private readonly StateMachine _stateMachine;

    protected override void OnStateUpdate()
    {
        base.OnStateUpdate();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _stateMachine.SetState<GameplayState>();
        }
    }
}

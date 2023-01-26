using Leopotam.Ecs;

namespace alicewithalex.Game.Components
{
    public struct PlayerDestroyedSignal : IEcsIgnoreInFilter { }
    public struct EnemyDestroyedSignal : IEcsIgnoreInFilter { }
    public struct ActivateColliderSignal : IEcsIgnoreInFilter { }
    public struct PoolSignal : IEcsIgnoreInFilter { }
}
using Leopotam.Ecs;
using UnityEngine;

namespace alicewithalex.Game.Components
{
    public struct PoolTag : IEcsIgnoreInFilter { }
    public struct CleanupTag : IEcsIgnoreInFilter { }
    public struct DisabledTag : IEcsIgnoreInFilter { }

}
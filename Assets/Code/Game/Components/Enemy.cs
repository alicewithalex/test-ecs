using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;

namespace alicewithalex.Game.Components
{
    public struct Enemy
    {
        public Data.EnemyType EnemyType;
        public NavMeshAgent Agent;
        public Collider Collider;
    }
}
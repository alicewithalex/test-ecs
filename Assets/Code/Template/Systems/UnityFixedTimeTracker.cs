using Leopotam.Ecs;
using UnityEngine;

namespace alicewithalex
{
    public class UnityFixedTimeTracker : IEcsRunSystem
    {
        private readonly TimeService _timeService;

        public void Run()
        {
            _timeService.FixedDeltaTime = _timeService.Scale * Time.fixedDeltaTime;
        }
    }
}
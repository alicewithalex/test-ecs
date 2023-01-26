using Leopotam.Ecs;
using UnityEngine;

namespace alicewithalex
{
    public class UnityTimeTracker : IEcsInitSystem, IEcsRunSystem
    {
        private readonly TimeService _timeService;

        public void Init()
        {
            _timeService.Time = -Time.deltaTime;
        }

        public void Run()
        {
            _timeService.DeltaTime = _timeService.Scale * Time.deltaTime;
            _timeService.Time += _timeService.DeltaTime;

            //Debug.Log($"Unity:{Time.time}");
            //Debug.Log($"Service:{_timeService.Time}");
        }
    }
}
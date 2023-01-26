
namespace alicewithalex.Game.Components
{
    public struct ShootDelayTask
    {
        public readonly float Duration;
        public float Current { get; private set; }

        public ShootDelayTask(float duration)
        {
            Current = Duration = duration;
        }

        public bool Tick(float dt)
        {
            return (Current -= dt) <= 0;
        }
    }
}
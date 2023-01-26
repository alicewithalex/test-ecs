
namespace alicewithalex.Game.Components
{
    public struct ReturnToPoolTask
    {
        public readonly float Duration;
        public float Current { get; private set; }

        public ReturnToPoolTask(float duration)
        {
            Current = Duration = duration;
        }

        public bool Tick(float dt)
        {
            return (Current -= dt) <= 0;
        }
    }
}
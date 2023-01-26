
namespace alicewithalex
{
    public sealed class TimeService
    {
        public float Time;
        public float DeltaTime;
        public float FixedDeltaTime;
        public float Scale;

        public TimeService()
        {
            Scale = 1f;
        }
    }
}
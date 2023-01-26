
namespace alicewithalex.Game.Components
{
    [System.Serializable]
    public struct Health
    {
        public Health(float max,
            Data.IDestroyable destroyable = null) : this()
        {
            if (max <= 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            Max = Current = max;
            Destroyable = destroyable;
        }

        public float Max { get; private set; }
        public float Current { get; private set; }

        public Data.IDestroyable Destroyable { get; private set; }

        public float Percentage => Current * 1f / Max;

        public bool Take(float damage)
        {
            if (damage <= 0) return true;

            return (Current -= damage) > 0;
        }

        public override string ToString()
        {
            return $"Max - {Max}\n" +
                   $"Current - {Current}";
        }
    }
}
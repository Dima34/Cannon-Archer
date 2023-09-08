namespace Infrastructure.Logic
{
    public static class Helpers
    {
        public static float PercentageToLimitedRange(float percentage, float minValue, float maxValue) =>
            (percentage / 100) * (maxValue - minValue) + minValue;
        
        public static float LinearTransform(float input, float at, float get, float at1, float get1)
        {
            float a = (get1 - get) / (at1 - at);
            float b = get - a * at;
            return a * input + b;
        }
    }
}
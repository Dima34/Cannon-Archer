namespace Infrastructure.Logic
{
    public static class Helpers
    {
        public static float PercentageToLimitedRange(float percentage, float minValue, float maxValue) =>
            (percentage / 100) * (maxValue - minValue) + minValue;
    }
}
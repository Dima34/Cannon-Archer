namespace Infrastructure.Services.Input
{
    public interface IInputService
    {
        float XAxis { get; }
        bool IsFireTap { get; }
        float VerticalPercentage { get; }
        void SetVerticalPercentage(float percentage);
        void SetFireTap(bool state);
    }
}
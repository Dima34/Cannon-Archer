namespace Infrastructure.Services.Input
{
    public interface IInputService
    {
        float XAxis { get; }
        bool OnFireTap { get; }
        float VerticalPercentage { get; }
        void SetVerticalPercentage(float percentage);
    }
}
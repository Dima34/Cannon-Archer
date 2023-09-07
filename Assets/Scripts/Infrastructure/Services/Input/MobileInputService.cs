namespace Infrastructure.Services.Input
{
    public class MobileInputService : InputService
    {
        protected override float GetXAxis() =>
            GetTouchOnScreenPercent();

        protected override bool IsFireTap() =>
            UnityEngine.Input.touchCount > 0;
    }
}
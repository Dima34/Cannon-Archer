using UnityEngine;

namespace Infrastructure.Services.Input
{
    public class MobileInputService : InputService
    {
        protected override float GetXAxis() =>
            GetTouchOnScreenPercent();

        protected override float GetVerticalPercentage() =>
            _verticalPercentage;

        protected override bool GetFireTap() =>
            IsScreenTouched() && TouchEnded();
    }
}
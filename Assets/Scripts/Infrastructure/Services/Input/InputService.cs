using System;
using UnityEngine;

namespace Infrastructure.Services.Input
{
    public abstract class InputService : IInputService
    {
        public float XAxis => GetXAxis();
        public float VerticalPercentage => GetVerticalPercentage();
        public bool IsFireTap => GetFireTap();

        protected float _verticalPercentage;
        protected bool _fireTap;

        protected abstract float GetXAxis();
        protected abstract float GetVerticalPercentage();
        protected abstract bool GetFireTap();

        protected static Vector2 GetTouchDeltaPosition()
        {
            Touch touch = UnityEngine.Input.GetTouch(0);
            Vector2 touchDeltaPosition = touch.deltaPosition;
            return touchDeltaPosition;
        }

        public void SetVerticalPercentage(float percentage) =>
            _verticalPercentage = percentage;

        public void SetFireTap(bool state) =>
            _fireTap = state;

        internal static float GetTouchOnScreenPercent()
        {
            Vector2 touchDeltaPosition = GetTouchDeltaPosition();
            return touchDeltaPosition.x / Screen.width * 100;
        }
        
        protected static bool IsScreenTouched()
        {
            return UnityEngine.Input.touchCount > 0;
        }

        protected static bool TouchEnded() =>
            UnityEngine.Input.touches[0].phase == TouchPhase.Stationary;
    }
}
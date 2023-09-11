using UnityEngine;

namespace Infrastructure.Services.Input
{
    public class StandaloneInputService : InputService
    {
        private const int MAX_VERTICAL_PERCENTAGE = 100;
        private const int MIN_VERTICAL_PERCENTAGE = 0;
        private const int VERTICAL_SPEED = 10;
        private const string HORIZONTAL_AXIS = "Horizontal";
        private const string VERTICAL_AXIS = "Vertical";
        private const KeyCode SPACE_KEYCODE = KeyCode.Space;

        protected override float GetXAxis()
        {
            if (IsScreenTouched())
                return GetTouchOnScreenPercent();

            if (UnityEngine.Input.GetAxis(HORIZONTAL_AXIS) is float horizontalAxis){}
                return horizontalAxis;

            return 0;
        }

        protected override float GetVerticalPercentage()
        {
            if (UnityEngine.Input.GetAxis(VERTICAL_AXIS) is float verticalAxis)
            {
                _verticalPercentage += verticalAxis / VERTICAL_SPEED;
                return Mathf.Clamp(_verticalPercentage, MIN_VERTICAL_PERCENTAGE, MAX_VERTICAL_PERCENTAGE);
            } 
            
            return _verticalPercentage;
        }

        protected override bool GetFireTap()
        {
            if (IsSpacebarUp())
                return true;

            return _fireTap;
        }

        private bool IsSpacebarUp() =>
            UnityEngine.Input.GetKeyUp(SPACE_KEYCODE);

        
    }
}
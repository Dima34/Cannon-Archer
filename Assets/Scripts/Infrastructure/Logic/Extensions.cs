using UnityEngine;

namespace Infrastructure.Logic
{
    public static class Extensions
    {
        public static void SetRotation(this Transform targetTransform, float rotAngle, Vector3 rotAxis) =>
            targetTransform.localRotation = Quaternion.Euler(0, 0, rotAngle);

        public static void AddRotation(this Transform target, float rotAngle, Vector3 rotAxis) =>
            target.localRotation *= Quaternion.AngleAxis(rotAngle, rotAxis);
    }
}
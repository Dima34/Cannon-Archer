using UnityEngine;

namespace Infrastructure.Logic
{
    public static class Extensions
    {
        public static void AddRotation(this Transform target, float rotAngle, Vector3 rotAxis) =>
            target.localRotation *= Quaternion.AngleAxis(rotAngle, rotAxis);
    }
}
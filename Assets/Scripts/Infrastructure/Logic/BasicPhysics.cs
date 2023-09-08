using UnityEngine;

namespace Infrastructure.Logic
{
    public static class BasicPhysics
    {
        private static Vector3 GRAVITY = new Vector3(0, -9.81f, 0);
        
        public static Vector3 BallisticTrajectory(Vector3 startPosition,Vector3 forwardDirection, float time, float speed) =>
            startPosition + (forwardDirection * speed * time + GRAVITY * time * time);
    }
}
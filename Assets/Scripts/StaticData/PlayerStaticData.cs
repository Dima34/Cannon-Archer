using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "StaticData/PlayerData")]
    public class PlayerStaticData : ScriptableObject
    {
        public float RotationSpeed;
        public float MinBarrelAngle;
        public float MaxBarrelAngle;
        public float BulletSpeed;
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/LevelData")]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelName;
        public Vector3 PlayerStartPosition;
        public Vector3 LevelSpawnPosition;
    }
}
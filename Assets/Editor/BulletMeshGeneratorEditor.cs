using UnityEditor;
using UnityEngine;

namespace Infrastructure.Logic.Editor
{
    [CustomEditor(typeof(BulletMeshGenerator))]
    public class BulletMeshGeneratorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            BulletMeshGenerator generator = (BulletMeshGenerator)target;

            if (GUILayout.Button("Generate"))
            {
                generator.GenerateRandomBullet();
                
                EditorUtility.SetDirty(generator);
            }
        }
    }
}
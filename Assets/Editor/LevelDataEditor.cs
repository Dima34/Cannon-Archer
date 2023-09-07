using Infrastructure.Constants;
using StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(LevelStaticData))]
public class LevelStaticDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LevelStaticData levelData = (LevelStaticData)target;

        if (GUILayout.Button("Set Scene Name"))
        {
            AddSceneNameData(levelData);

            EditorUtility.SetDirty(levelData);
        }
    }

    private static void AddSceneNameData(LevelStaticData levelData) =>
        levelData.LevelName = SceneManager.GetActiveScene().name;

}
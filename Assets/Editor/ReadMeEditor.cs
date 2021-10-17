using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Flux
{
    [CustomEditor(typeof(ReadMe))]
    public class ReadMeEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.Space();

            if (GUILayout.Button("Open Read Me"))
                SceneLoader.OpenReadMe(EditorSceneManager.GetActiveScene().name);

            if (GUILayout.Button("Open Start Scene") && EditorSceneManager.GetActiveScene().name != "Start Scene")
                EditorSceneManager.OpenScene("Assets/Scenes/Start Scene.unity");
        }
    }
}

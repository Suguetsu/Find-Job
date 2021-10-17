using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Flux
{
    public class LightingTestWindow : EditorWindow
    {
        private static Texture header;

        public static void ShowWindow()
        {
            LightingTestWindow window = GetWindow<LightingTestWindow>();
            Texture icon = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Editor/Resources/Flux Icon.png");
            header = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Editor/Resources/Lighting Test.png");
            window.minSize = new Vector2(600, 570);


            GUIContent titleContent = new GUIContent("Lighting Test", icon);
            window.titleContent = titleContent;
        }

        private void OnGUI()
        {
            GUILayout.Label(header);
            GUILayout.Label("Lighting Test", CustomEditorStyles.headerStyle());
            EditorGUILayout.Space();
            GUILayout.Label("In this scene you must setup the lighting, shadows and reflections to look the best way possible");
            EditorGUILayout.Space(15);
            GUILayout.Label("You can:", CustomEditorStyles.subTitle());
            GUILayout.Label("- Add Post Processing Effects");
            GUILayout.Label("- Add Reflection Probes");
            GUILayout.Label("- Choose between Baked or Realtime lighting");
            EditorGUILayout.Space(15);
            GUILayout.Label("You can't:", CustomEditorStyles.subTitle());
            GUILayout.Label("- Add more lights to the scene");
            GUILayout.Label("- Rotate the existing Directional Light");
            GUILayout.Label("- Move or rotate the Camera");
            GUILayout.Label("- Add or remove geometry");
            GUILayout.Label("- Change the existing materials or the Skybox");
            EditorGUILayout.Space(15);
            GUILayout.Label("Optional:", CustomEditorStyles.subTitle());
            GUILayout.Label("- Keep the batch count under 100");

            EditorGUILayout.Space();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Back to hub"))
            {
                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                EditorSceneManager.OpenScene("Assets/Scenes/Start Scene.unity");
                Close();
            }

            if (GUILayout.Button("Close"))
                Close();
            GUILayout.EndHorizontal();

        }
    }


}


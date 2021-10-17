using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;

namespace Flux
{
    public class StartWindow : EditorWindow
    {
        private static Texture header;
        private static int hubScreen = 0;

        public static void ShowWindow()
        {
            StartWindow window = GetWindow<StartWindow>();
            Texture icon = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Editor/Resources/Flux Icon.png");
            header = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Editor/Resources/Flux Games.png");
            window.minSize = new Vector2(600, 510);

            GUIContent titleContent = new GUIContent("Start", icon);
            window.titleContent = titleContent;

            hubScreen = 0;
        }

        private void OnGUI()
        {
            if (Application.isPlaying)
            {
                Close();
                return;
            }
            GUILayout.Label(header);
            GUILayout.Label("Developer Test", CustomEditorStyles.headerStyle());
            EditorGUILayout.Space();

            switch (hubScreen)
            {
                case 0:
                    HubScreen();
                    break;
                case 1:
                    FormsScreen();
                    break;
                case 2:
                    Forms2();
                    break;
                case 3:
                    Forms3();
                    break;
                case 4:
                    Forms4();
                    break;
                case 5:
                    Forms5();
                    break;
                case 6:
                    Forms6();
                    break;
                case 7:
                    Forms7();
                    break;
                case 8:
                    Forms8();
                    break;
                case 9:
                    Forms9();
                    break;
            }


            EditorGUILayout.Space(15);

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (hubScreen == 0)
            {
                if (GUILayout.Button("Forms"))
                    hubScreen = 1;
            }

            else
            {
                if (GUILayout.Button("Back to hub"))
                    hubScreen = 0;
            }


            if (GUILayout.Button("Close"))
                Close();
            GUILayout.EndHorizontal();

        }

        private void SceneButton(string buttonName, int id, int buttonWidth = 200)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(EditorGUIUtility.currentViewWidth / 2 - buttonWidth / 2);
            Color oldColor = GUI.backgroundColor;

            if (GetBool(buttonName))
                GUI.backgroundColor = Color.green;

            if (GUILayout.Button(buttonName, GUILayout.Width(buttonWidth)))
            {
                hubScreen = id;
            }

            SetBool(buttonName, EditorGUILayout.Toggle(GetBool(buttonName)));
            GUI.backgroundColor = oldColor;

            GUILayout.EndHorizontal();
        }

        private void HubScreen()
        {
            GUILayout.Label("Click the button for more information about the tasks.\nMark the CheckBox only if you completed the task\nGood luck!", EditorStyles.wordWrappedLabel);
            if (GUILayout.Button("Delivery information"))
            {
                hubScreen = 9;
            }
            EditorGUILayout.Space(20);
            SceneButton("Increase the enemy's firing speed", 2);
            EditorGUILayout.Space();
            SceneButton("Enemy always dodge the shot", 3);
            EditorGUILayout.Space();
            SceneButton("Create Save", 4);
            EditorGUILayout.Space();
            SceneButton("Create ghost P2", 5);
            EditorGUILayout.Space();
            SceneButton("Add Life Shield UI Feedback", 6);
            EditorGUILayout.Space();
            SceneButton("Add Pause", 7);
            EditorGUILayout.Space();
            SceneButton("Build UWP", 8);
        }

        private void FormsScreen()
        {
            GUILayout.Label("Please fill the information above. You can check more than one interest area.", EditorStyles.wordWrappedLabel);
            EditorGUILayout.Space(20);
            GUILayout.Label("Personal info", EditorStyles.boldLabel);
            SetString("Name", EditorGUILayout.TextField("Name", GetString("Name")));
            EditorGUILayout.Space();
            GUILayout.Label("Interest areas", EditorStyles.boldLabel);
            SetBool("GamePlay", EditorGUILayout.Toggle("GamePlay", GetBool("GamePlay")));
            SetBool("UI", EditorGUILayout.Toggle("UI Delevop", GetBool("UI")));
            SetBool("IA", EditorGUILayout.Toggle("IA", GetBool("IA")));
            SetBool("Multiplayer", EditorGUILayout.Toggle("Multiplayer", GetBool("Multiplayer")));
            // SetBool("Cinematics", EditorGUILayout.Toggle("Cinematics", GetBool("Cinematics")));
            SetString("Other", EditorGUILayout.TextField("Other", GetString("Other")));
        }
        private void SetString(string name, string b)
        {
            if (File.Exists("Assets/StreamingAssets/" + name))
            {
                File.Delete("Assets/StreamingAssets/" + name);
            }

            File.WriteAllText("Assets/StreamingAssets/" + name, b);

        }
        private string GetString(string name)
        {
            if (!File.Exists("Assets/StreamingAssets/" + name))
            {
                return "";
            }
            return File.ReadAllText("Assets/StreamingAssets/" + name);
        }
        private void SetBool(string name, bool b)
        {
            if (File.Exists("Assets/StreamingAssets/" + name))
            {
                File.Delete("Assets/StreamingAssets/" + name);
            }
            if (b)
            {
                File.WriteAllText("Assets/StreamingAssets/" + name, "OK");
            }

        }

        private bool GetBool(string name)
        {
            return File.Exists("Assets/StreamingAssets/" + name);
        }

        private void Forms2()
        {
            GUILayout.Label("Increase the enemy's Test", CustomEditorStyles.subTitle());
            EditorGUILayout.Space(10);
            GUILayout.Label("In this Test you must increase the shooting speed of all enemies.");
            GUILayout.Label(AssetDatabase.LoadAssetAtPath<Texture>("Assets/Editor/Resources/Panel2.png"));
            EditorGUILayout.Space();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Open Game Scene"))
            {
                EditorSceneManager.OpenScene("Assets/Scenes/Game.unity");
            }
            GUILayout.EndHorizontal();
        }
        private void Forms3()
        {
            GUILayout.Label("Enemy always dodge the shot Test", CustomEditorStyles.subTitle());
            EditorGUILayout.Space(20);
            GUILayout.Label("Here the following tasks must be done:");
            GUILayout.Label("- Choose only 1 enemy");
            GUILayout.Label("- Create an exposed parameter to mark that the enemy has this ability");
            GUILayout.Label("- Create this enemy's ability to detect the Player's shot and dodge it");
            EditorGUILayout.Space(10);
            GUILayout.Label("It is important that this enemy never takes a shot from the player.");
            GUILayout.Label(AssetDatabase.LoadAssetAtPath<Texture>("Assets/Editor/Resources/Panel3.png"));
            EditorGUILayout.Space();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Open Game Scene"))
            {
                EditorSceneManager.OpenScene("Assets/Scenes/Game.unity");
            }
            GUILayout.EndHorizontal();
        }

        private void Forms4()
        {
            GUILayout.Label("Create Save", CustomEditorStyles.subTitle());
            EditorGUILayout.Space(20);
            GUILayout.Label("Create the resource to save the player's stage, whenever he dies he should return to the stage he was in.");
            GUILayout.Label("You should NOT use PlayerPrefs to do this feature");
            GUILayout.Label("On the Game Over screen, add a button to restart at the stage that died");
            GUILayout.Label("Even when closing the game, the player must be able to return to the stage where he left off, \ncreate a continue button in the Menu");
            GUILayout.Label(AssetDatabase.LoadAssetAtPath<Texture>("Assets/Editor/Resources/Panel4.png"));
            EditorGUILayout.Space();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Open Game Scene"))
            {
                EditorSceneManager.OpenScene("Assets/Scenes/Game.unity");
            }
            GUILayout.EndHorizontal();
        }

        private void Forms5()
        {
            GUILayout.Label("Create ghost P2", CustomEditorStyles.subTitle());
            EditorGUILayout.Space(20);
            GUILayout.Label("In this test you must create a second player who will always\n make the same movements as the player with a delay time (Phantom)");
            GUILayout.Label("Player 2's shot does not have to be a copy of Player 1, the movements must be the same,\n both in position and rotation.");
            GUILayout.Label(AssetDatabase.LoadAssetAtPath<Texture>("Assets/Editor/Resources/Panel5.png"));
            EditorGUILayout.Space();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Open Game Scene"))
            {
                EditorSceneManager.OpenScene("Assets/Scenes/Game.unity");
            }
            GUILayout.EndHorizontal();
        }
        private void Forms6()
        {
            GUILayout.Label("Create ghost P2", CustomEditorStyles.subTitle());
            EditorGUILayout.Space(20);
            GUILayout.Label("Here you must create a UI sistem to show the life of the shield when the player is equipped with it.");
            GUILayout.Label(AssetDatabase.LoadAssetAtPath<Texture>("Assets/Editor/Resources/Panel6.png"));
            EditorGUILayout.Space();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Open Game Scene"))
            {
                EditorSceneManager.OpenScene("Assets/Scenes/Game.unity");
            }
            GUILayout.EndHorizontal();
        }

        private void Forms7()
        {
            GUILayout.Label("Create Pause", CustomEditorStyles.subTitle());
            EditorGUILayout.Space(20);
            GUILayout.Label("Develop a pause system for the game.\nRemember the audios and the UI.");
            GUILayout.Label(AssetDatabase.LoadAssetAtPath<Texture>("Assets/Editor/Resources/Panel7.png"));
            EditorGUILayout.Space();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Open Game Scene"))
            {
                EditorSceneManager.OpenScene("Assets/Scenes/Game.unity");
            }
            GUILayout.EndHorizontal();
        }
        private void Forms8()
        {
            GUILayout.Label("Build UWP", CustomEditorStyles.subTitle());
            EditorGUILayout.Space(20);
            GUILayout.Label("Build for Universal Windows Platform\nConvert the game to make it work for UWP");
            GUILayout.Label(AssetDatabase.LoadAssetAtPath<Texture>("Assets/Editor/Resources/Panel8.png"));
            EditorGUILayout.Space();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Open Game Scene"))
            {
                EditorSceneManager.OpenScene("Assets/Scenes/Game.unity");
            }
            GUILayout.EndHorizontal();
        }
        private void Forms9()
        {
            GUILayout.Label("Delivery information", CustomEditorStyles.subTitle());
            EditorGUILayout.Space(20);
            GUILayout.Label("1) You should only check the items that you have successfully completed.");
            GUILayout.Label("2) Don't forget to fill in your name on the form");
            GUILayout.Label("3) The sources must be on delivery");
            GUILayout.Label("4) Delivery must be made with Builds for Windows");
            GUILayout.Label("5) If you perform the UWP build task you must deliver the 2 builds, Windows and UWP");
            GUILayout.Label("6) The files must be delivered by GitLab (https://gitlab.com), Do not forget to leave the project as public.");
            GUILayout.Label("7) Create a document with all the details you think is necessary and forward it together with the GitLab \nlink to the email: job@flux.games");
            EditorGUILayout.Space();

        }

    }
}

public static class CustomEditorStyles
{
    public static GUIStyle headerStyle()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 24;
        style.fontStyle = FontStyle.Bold;
        style.margin.left = 5;
        style.margin.top = 5;
        return style;
    }

    public static GUIStyle subTitle()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 14;
        style.fontStyle = FontStyle.Bold;
        style.margin.left = 5;
        return style;
    }
}

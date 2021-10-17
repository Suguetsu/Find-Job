using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Flux
{

    [InitializeOnLoad]
    public static class SceneLoader
    {
        static SceneLoader()
        {
            EditorApplication.update += Startup;
            EditorSceneManager.sceneOpened += SceneOpenedCallback;
        }

        static void SceneOpenedCallback(Scene _scene, OpenSceneMode _mode)
        {
            OpenReadMe(_scene.name);
        }

        public static void OpenReadMe(string sceneName)
        {
            switch (sceneName)
            {

                case "Start Scene":
                    StartWindow.ShowWindow();
                    break;


            }
        }

        static void Startup()
        {
            EditorApplication.update -= Startup;
            OpenReadMe("Start Scene");
        }
    }
}

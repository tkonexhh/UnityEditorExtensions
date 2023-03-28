using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace UnityToolbarExtender
{
    static class ToolbarStyles
    {
        public static readonly GUIStyle commandButtonStyle = new GUIStyle(EditorStyles.toolbarButton);

        // static ToolbarStyles()
        // {
        //     commandButtonStyle = new GUIStyle("Command")
        //     {
        //         fontSize = 10,
        //         fixedWidth = 100,
        //         alignment = TextAnchor.MiddleCenter,
        //         imagePosition = ImagePosition.ImageAbove,
        //         fontStyle = FontStyle.Bold,

        //     };
        // }
    }

    [InitializeOnLoad]
    public class Toolbar_SceneSwitch
    {
        static Toolbar_SceneSwitch()
        {
            ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
        }

        static void OnToolbarGUI()
        {
            if (!ToolbarExtenderSettings.GetConfig().SceneSwitcherConfig.enable)
                return;
            GUILayout.FlexibleSpace();

            if (GUILayout.Button(new GUIContent("场景跳转"), ToolbarStyles.commandButtonStyle))
            {
                ShowMenu();
            }
        }

        static void ShowMenu()
        {
            GenericMenu menu = new GenericMenu();
            var config = ToolbarExtenderSettings.GetConfig().SceneSwitcherConfig;
            for (int i = 0; i < config.scenes.Count; i++)
            {
                var scene = config.scenes[i];
                if (scene == null)
                    continue;
                menu.AddItem(new GUIContent(scene.name), false, ChangeScene, scene.name);
            }
            menu.ShowAsContext();
        }

        static void ChangeScene(object name)
        {
            SceneHelper.StartScene(name as string);
        }

    }

    static class SceneHelper
    {
        static string sceneToOpen;

        public static void StartScene(string sceneName)
        {
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
            }

            sceneToOpen = sceneName;
            EditorApplication.update += OnUpdate;
        }

        static void OnUpdate()
        {
            if (sceneToOpen == null ||
                EditorApplication.isPlaying || EditorApplication.isPaused ||
                EditorApplication.isCompiling || EditorApplication.isPlayingOrWillChangePlaymode)
            {
                return;
            }

            EditorApplication.update -= OnUpdate;

            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                // need to get scene via search because the path to the scene
                // file contains the package version so it'll change over time
                string[] guids = AssetDatabase.FindAssets("t:scene " + sceneToOpen, null);
                if (guids.Length == 0)
                {
                    Debug.LogWarning("Couldn't find scene file");
                }
                else
                {
                    //会找到相似的名字上面去 需要进一步处理
                    for (int i = 0; i < guids.Length; i++)
                    {
                        string scenePath = AssetDatabase.GUIDToAssetPath(guids[i]);
                        if (scenePath.EndsWith($"{sceneToOpen}.unity"))
                        {
                            EditorSceneManager.OpenScene(scenePath);
                            EditorGUIUtility.PingObject(AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath));
                            break;
                        }

                    }
                    // EditorApplication.isPlaying = true;
                }
            }
            sceneToOpen = null;
        }
    }

}

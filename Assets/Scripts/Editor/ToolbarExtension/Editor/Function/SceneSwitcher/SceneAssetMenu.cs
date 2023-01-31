using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UnityToolbarExtender
{
    [InitializeOnLoad]
    public class SceneAssetMenu
    {
        static SceneAssetMenu()
        {
            Selection.selectionChanged += OnSelectionChanged;
        }

        static void OnSelectionChanged()
        {
            var target = Selection.activeObject;
            if (target is SceneAsset)
            {
                bool contains = ToolbarExtenderSettings.GetConfig().SceneSwitcherConfig.scenes.Contains(target as SceneAsset);
                Menu.SetChecked("CONTEXT/SceneAsset/添加到场景跳转", contains);
            }
        }

        [MenuItem("CONTEXT/SceneAsset/添加到场景跳转")]
        static void AddToList()
        {
            var target = Selection.activeObject;
            var scene = target as SceneAsset;
            bool contains = ToolbarExtenderSettings.GetConfig().SceneSwitcherConfig.scenes.Contains(scene);
            if (contains)
            {
                ToolbarExtenderSettings.GetConfig().SceneSwitcherConfig.scenes.Remove(scene);
                Debug.LogError("从场景跳转中移除:" + scene.name);
            }
            else
            {
                ToolbarExtenderSettings.GetConfig().SceneSwitcherConfig.scenes.Add(scene);
                Debug.LogError("从场景跳转中添加:" + scene.name);
            }
            ToolbarExtenderSettings.Save();
            EditorUtility.SetDirty(ToolbarExtenderSettings.GetConfig());
        }
    }
}

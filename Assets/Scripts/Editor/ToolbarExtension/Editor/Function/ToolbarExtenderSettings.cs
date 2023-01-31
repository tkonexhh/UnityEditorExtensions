using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace UnityToolbarExtender
{
    public class ToolbarExtenderSettings : ScriptableObject
    {
        #region  static
        static string filePath => Application.dataPath + "/../Library/ToolbarExtenderSettings.asset";


        static ToolbarExtenderSettings _Setting;
        public static ToolbarExtenderSettings GetConfig()
        {
            if (_Setting == null)
            {
                _Setting = ScriptableObject.CreateInstance<ToolbarExtenderSettings>();
                Load();
            }


            //清除无效scene 
            for (int i = _Setting.SceneSwitcherConfig.scenes.Count - 1; i >= 0; i--)
            {
                if (_Setting.SceneSwitcherConfig.scenes[i] == null)
                    _Setting.SceneSwitcherConfig.scenes.RemoveAt(i);
            }

            return _Setting;
        }

        internal static SerializedObject GetSerializedSettings()
        {
            return new SerializedObject(GetConfig());
        }


        public static void Save()
        {
            var s = EditorJsonUtility.ToJson(_Setting);
            if (!string.IsNullOrEmpty(s))
            {
                var fs = File.CreateText(filePath);
                fs.Write(s);
                fs.Flush();
                fs.Close();
                fs.Dispose();
                // Debug.LogError("Save ToolbarExtenderSettings:" + s);
            }
        }

        public static void Load()
        {
            if (File.Exists(filePath))
            {
                var s = File.ReadAllText(filePath);
                Debug.LogWarning("Load ToolbarExtenderSettings:" + s);
                EditorJsonUtility.FromJsonOverwrite(s, _Setting);
            }
        }

        #endregion

        public bool enable = true;
        public SceneSwitcherConfig SceneSwitcherConfig = new SceneSwitcherConfig();
    }

    static class ToolbarExtenderSetting
    {
        static SerializedObject _SerializedObject;

        static SerializedProperty _Enable;
        static SerializedProperty _SceneSwitcherConfig;

        [SettingsProvider]
        public static SettingsProvider Provider()
        {
            return new SettingsProvider("Toolbar 扩展", SettingsScope.User)//user 的话就出现再preferences project就出现在projectsetting
            {

                activateHandler = (searchContext, rootElement) =>
                {
                    _SerializedObject = ToolbarExtenderSettings.GetSerializedSettings();
                    if (_SerializedObject != null)
                    {
                        _Enable = _SerializedObject.FindProperty("enable");
                        _SceneSwitcherConfig = _SerializedObject.FindProperty("SceneSwitcherConfig");
                    }
                },
                guiHandler = (searchContext) =>
                {
                    if (_SerializedObject != null)
                    {
                        EditorGUILayout.BeginVertical(GUILayout.MinWidth(500));

                        EditorGUILayout.PropertyField(_Enable, new GUIContent("是否启用Toolbar扩展"));
                        EditorGUILayout.PropertyField(_SceneSwitcherConfig, new GUIContent("场景切换"));

                        EditorGUILayout.EndVertical();

                        ToolbarExtenderSettings.Save();

                        _SerializedObject.ApplyModifiedProperties();
                    }
                }
            };
        }
    }
}

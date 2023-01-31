using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityToolbarExtender
{
    [System.Serializable]
    public class SceneSwitcherConfig
    {
        [InspectorName("是否开启")] public bool enable = true;
        public List<SceneAsset> scenes = new List<SceneAsset>();
    }
}

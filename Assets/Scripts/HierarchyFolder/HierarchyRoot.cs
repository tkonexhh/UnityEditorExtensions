using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

//拥有此脚本的物体会成为一个特殊的物体 不能保存预制体 只能作为目录结构
[DisallowMultipleComponent]
public class HierarchyRoot : MonoBehaviour
{

}







#if UNITY_EDITOR
// https://github.com/Unity-Technologies/UnityCsReference/blob/master/Editor/Mono/DragAndDrop.bindings.cs
public static class HookUpDragAndDrop
{
    [InitializeOnLoadMethod]
    public static void HookUpDragAndDropToProjectBrowser()
    {
        var dropHandler = (DragAndDrop.ProjectBrowserDropHandler)OnProjectBrowserDrop;

        if (!DragAndDrop.HasHandler(DragAndDropWindowTarget.projectBrowser, dropHandler))
        {
            DragAndDrop.AddDropHandler(dropHandler);
        }
    }

    private static DragAndDropVisualMode OnProjectBrowserDrop(int dragInstanceId, string dropUponPath, bool perform)
    {
        // 拖拽到Project窗口的物体 如果含有HierarchyRoot 则不允许放下
        foreach (var v in DragAndDrop.objectReferences)
        {
            var obj = v as GameObject;
            if (obj != null)
            {
                var folders = obj.GetComponentsInChildren<HierarchyRoot>(true);
                // 返回非DragAndDropVisualMode.None 表示这层对Drag进行了处理
                if (folders != null && folders.Length > 0) return DragAndDropVisualMode.Rejected;
            }
        }
        // 返回DragAndDropVisualMode.None表示 这层啥也没干 交给其他Handler继续处理
        return DragAndDropVisualMode.None;
    }
}

//──────────────────────────────────────────────────────────────────────────────────────────────────────
//https://forum.unity.com/threads/free-hierarchy-window-icons-on-objects-via-interfaces.436548/
[InitializeOnLoad]
static public class HierarchyIcons
{
    static private Texture HierarchyFolderIcon;

    static HierarchyIcons()
    {
        EditorApplication.hierarchyWindowItemOnGUI -= EvaluateIcons;
        EditorApplication.hierarchyWindowItemOnGUI += EvaluateIcons;
    }

    static private void EvaluateIcons(int instanceId, Rect selectionRect)
    {
        if (Application.isPlaying) return;
        var go = EditorUtility.InstanceIDToObject(instanceId) as GameObject;
        if (go == null) return;

        if (go.TryGetComponent<HierarchyRoot>(out var component))
        {
            DrawIcon(selectionRect, go, component);
        }
    }

    static private void DrawIcon(Rect rect, GameObject go, HierarchyRoot component)
    {
        var iconRect = rect;
        iconRect.width = rect.height;

        Texture icon = null;
        bool isValid = true;//component.IsValid();
        //可以在此添加一些逻辑 切换ICON

        HierarchyFolderIcon = HierarchyFolderIcon ?? AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Scripts/HierarchyFolder/Icon/HierarchyFolderIcon.png");
        icon = HierarchyFolderIcon;

        GUI.DrawTexture(iconRect, icon);
    }

}

#endif

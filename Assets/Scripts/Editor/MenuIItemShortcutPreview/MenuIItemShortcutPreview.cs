using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MenuIItemShortcutPreview : Editor
{
    //一定要再名字的结尾加上 " _**"才能热键生效
    // %  CTRL
    // #  SHIFT

    [MenuItem(ToolsDefine.ToolRoot + "快捷键预览/CTRL 预览 _%]")]
    static void HotKeyCtrl()
    {
        Debug.LogError("快捷键预览 CTRL+]");
    }

    [MenuItem(ToolsDefine.ToolRoot + "快捷键预览/SHIFT 预览 _#]")]
    static void HotKeyShift()
    {
        Debug.LogError("快捷键预览 SHIFT+]");
    }

    [MenuItem(ToolsDefine.ToolRoot + "快捷键预览/CTRL+SHIFT 预览 _%#]")]
    static void HotKeyCTRLShift()
    {
        Debug.LogError("快捷键预览 CTRL+SHFIT+]");
    }

    [MenuItem(ToolsDefine.ToolRoot + "快捷键预览/_SHIFT+F12 预览 _#F11")]
    static void HotKeyCTRLShiftF12()
    {
        Debug.LogError("快捷键预览 _SHIFT+F11");
    }
}

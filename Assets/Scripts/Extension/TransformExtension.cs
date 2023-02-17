using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtension
{
    public static K[] FindAllInChildren<T, K>(this T root, bool includeInactive = false) where T : Component where K : Component
    {
        if (root == null) return null;
        K[] temp = root.GetComponentsInChildren<K>(includeInactive);
        return temp.RemoveAll(x => x == root);
    }

    public static Transform[] FindChildrensByName(this Transform root, string gameobjectName, bool includeDepth, bool includeInactive)
    {
        if (root == null) return null;
        Transform[] transforms = null;
        if (includeDepth) transforms = root.FindAllInChildren<Transform, Transform>(includeInactive);
        else
        {
            List<Transform> list = new List<Transform>();
            for (int i = 0; i < root.childCount; i++)
            {
                Transform t = root.GetChild(i);
                if (t == null) continue;
                if (!t.gameObject.activeInHierarchy && !includeInactive) continue;
                list.Add(t);
            }
            transforms = list.ToArray();
        }
#if UNITY_EDITOR
        return transforms.FindAll(x => x.gameObject.name.Trim() == gameobjectName);
#else
        return transforms.FindAll(x => x.gameObject.name == gameobjectName);
#endif
    }

    public static Transform FindChildrenByName(this Transform root, string gameobjectName, bool includeDepth, bool includeInactive)
    {
        if (root == null) return null;
        Transform[] transforms = null;
        if (includeDepth) transforms = root.FindAllInChildren<Transform, Transform>(includeInactive);
        else
        {
            List<Transform> list = new List<Transform>();
            for (int i = 0; i < root.childCount; i++)
            {
                Transform t = root.GetChild(i);
                if (t == null) continue;
                if (!t.gameObject.activeInHierarchy && !includeInactive) continue;
                list.Add(t);
            }
            transforms = list.ToArray();
        }
#if UNITY_EDITOR
        var res = transforms.FindAll(x => x.gameObject.name.Trim() == gameobjectName);
        if (res == null || res.Length == 0) { return null; }
        return res[0];
#else
        var res = transforms.FindAll(x => x.gameObject.name == gameobjectName);
        if (res == null || res.Length == 0) { return null; }
        return res[0];
#endif
    }

    public static void Reset(this Transform self)
    {
        self.localPosition = Vector3.zero;
        self.localScale = Vector3.one;
        self.localRotation = Quaternion.identity;
    }
}

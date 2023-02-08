using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtension
{
    //     public static Transform[] FindChildrenByName(this Transform root, string gameobjectName, bool includeDepth, bool includeInactive)
    //     {
    //         if (root == null) return null;
    //         Transform[] transforms = null;
    //         if (includeDepth) transforms = root.FindAllInChildren<Transform, Transform>(includeInactive);
    //         else
    //         {
    //             List<Transform> list = new List<Transform>();
    //             for (int i = 0; i < root.childCount; i++)
    //             {
    //                 Transform t = root.GetChild(i);
    //                 if (t == null) continue;
    //                 if (!t.gameObject.activeInHierarchy && !includeInactive) continue;
    //                 list.Add(t);
    //             }
    //             transforms = list.ToArray();
    //         }
    // #if UNITY_EDITOR
    //         return transforms.FindAll(x => x.gameObject.name.Trim() == gameobjectName);
    // #else
    //         return transforms.FindAll(x => x.gameObject.name == gameobjectName);
    // #endif
    //     }
}

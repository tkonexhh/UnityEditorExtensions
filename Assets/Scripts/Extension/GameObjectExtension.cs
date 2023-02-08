using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtension
{
    public static void Reset(this GameObject self)
    {
        self.transform.localPosition = Vector3.zero;
        self.transform.localScale = Vector3.one;
        self.transform.localRotation = Quaternion.identity;
    }

    public static T AddMissingComponent<T>(this GameObject self) where T : Component
    {
        T retT = self.GetComponent<T>();
        return retT ?? self.AddComponent<T>();
    }
}

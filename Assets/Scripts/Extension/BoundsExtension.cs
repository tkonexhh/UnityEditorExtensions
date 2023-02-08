using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoundsExtension
{
    /// <summary>
    /// 获取物体的Bounds
    /// </summary>
    /// <param name="bounds"></param>
    /// <param name="go"></param>
    public static void GetRenderableBoundsRecurse(ref Bounds bounds, GameObject go)
    {
        MeshRenderer meshRenderer = go.GetComponent(typeof(MeshRenderer)) as MeshRenderer;
        MeshFilter meshFilter = go.GetComponent(typeof(MeshFilter)) as MeshFilter;
        if (meshRenderer && meshFilter && meshFilter.sharedMesh)
        {
            if (bounds.extents == Vector3.zero)
            {
                bounds = meshRenderer.bounds;
            }
            else
            {
                // 扩展包围盒，以让包围盒能够包含另一个包围盒
                bounds.Encapsulate(meshRenderer.bounds);
            }
        }
        SkinnedMeshRenderer skinnedMeshRenderer = go.GetComponent(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer;
        if (skinnedMeshRenderer && skinnedMeshRenderer.sharedMesh)
        {
            if (bounds.extents == Vector3.zero)
            {
                bounds = skinnedMeshRenderer.bounds;
            }
            else
            {
                bounds.Encapsulate(skinnedMeshRenderer.bounds);
            }
        }
        foreach (Transform transform in go.transform)
        {
            GetRenderableBoundsRecurse(ref bounds, transform.gameObject);
        }
    }
}

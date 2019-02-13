using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrustumPreview : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.cyan;
        Gizmos.DrawFrustum(Vector3.zero, 60, 5, 1, 1.3f);
        Gizmos.DrawWireCube(-Vector3.up * 1, new Vector3(0.6f, 2, 0.5f));
    }
}
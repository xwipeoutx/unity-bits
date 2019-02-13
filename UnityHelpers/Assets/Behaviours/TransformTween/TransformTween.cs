using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TransformTween : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public Transform target;

    [Range(0, 1)] public float t;

    void Update()
    {
        if (target == null || pointA == null || pointB == null)
            return;

        var time = t;
        target.localPosition = Position(time);
        target.localRotation = Rotation(time);
        target.localScale = Scale(time);
    }

    private Quaternion Rotation(float time)
    {
        return Quaternion.Lerp(pointA.localRotation, pointB.localRotation, time);
    }

    private Vector3 Position(float time)
    {
        return Vector3.Lerp(pointA.localPosition, pointB.localPosition, time);
    }

    private Vector3 Scale(float time)
    {
        return Vector3.Lerp(pointA.localScale, pointB.localScale, time);
    }

    private void OnDrawGizmosSelected()
    {
        var mesh = target.GetComponentInChildren<MeshFilter>()?.sharedMesh;
        
        if (target == null || pointA == null || pointB == null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position, Vector3.one * 0.2f);
            return;
        }

        var numSteps = 20f;
        for (var i = 0; i < numSteps; i++)
        {
            var t = i / numSteps;
            DrawSnapshot(t, mesh);
        }
    }

    private void DrawSnapshot(float time, Mesh mesh)
    {
        Gizmos.color = Color.Lerp(Color.black, Color.white, time);
        var localTransform = Matrix4x4.TRS(Position(time), Rotation(time), Scale(time));
        Gizmos.matrix = transform.localToWorldMatrix * localTransform;

        if (mesh != null)
            Gizmos.DrawWireMesh(mesh);
        else
            Gizmos.DrawFrustum(Vector3.zero, 50, 1, 0.1f, 1);
    }
}
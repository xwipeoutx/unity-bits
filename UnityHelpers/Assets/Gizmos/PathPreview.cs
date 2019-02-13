using UnityEngine;

public class PathPreview : MonoBehaviour
{
    private class SimpleTransform
    {
        public Vector3 position;
        public Quaternion rotation;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.matrix = transform.localToWorldMatrix;

        var numIntervals = 50f;
        
        for (var i = 0; i < numIntervals; i++)
        {
            Gizmos.color = Color.magenta;
            var p1 = Transform(i / numIntervals);
            var p2 = Transform((i + 1) / numIntervals);
            Gizmos.DrawLine(p1.position, p2.position);

            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(p2.position, 0.1f);
            Gizmos.DrawLine(p2.position, p2.position + p2.rotation * Vector3.back * 0.5f);
        }
    }

    private SimpleTransform Transform(float time)
    {
        var xLoops = 3;
        var zLoops = 2;
        var extents = new Vector3(3, 0, 8);

        var degrees = 2 * Mathf.PI * time;

        var x = Mathf.Cos(xLoops * degrees) * extents.x;
        var z = Mathf.Sin(zLoops * degrees) * extents.z;

        var dx = -xLoops * Mathf.Sin(xLoops * degrees) * extents.x;
        var dz = zLoops * Mathf.Cos(zLoops * degrees) * extents.z;

        var position = new Vector3(x, 0, z);
        var rotation = Quaternion.FromToRotation(Vector3.forward, new Vector3(dx, 0, dz));

        return new SimpleTransform
        {
            position = position,
            rotation = rotation
        };
    }
}
using UnityEngine;

public class CircleHitBox : MonoBehaviour
{
    [SerializeField] private MeshCollider meshCollider;
    [Space]
    public float range;
    public float radius;
    public byte points;

    void Start()
    {
        SetCircleMesh();
    }


    void SetCircleMesh()
    {
        Vector3[] vertices = new Vector3[points + 1];
        vertices[0] = Vector3.zero;

        for (int i = 0; i < points; i++)
        {
            float angleRatio = i / (float)(points - 1);
            float angleInRadians = Mathf.Lerp(0f, radius, angleRatio) * Mathf.Deg2Rad;
            float x = range * Mathf.Cos(angleInRadians);
            float z = range * Mathf.Sin(angleInRadians);
            vertices[i + 1] = new Vector3(x, 0f, z);
        }

        int[] triangles = new int[(points - 1) * 3];
        for (int i = 0; i < points - 1; i++)
        {
            int triIndex = i * 3;
            triangles[triIndex] = 0;
            triangles[triIndex + 1] = i + 1;
            triangles[triIndex + 2] = i + 2;
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        meshCollider.sharedMesh = null;
        meshCollider.sharedMesh = mesh;
    }


    public void SetRange(float newRange)
    {
        range = newRange;
        SetCircleMesh();
    }

    public void SetRadius(float newRadius)
    {
        radius = newRadius;
        SetCircleMesh();
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Vector3 center = transform.position;
        float step = 1f / (points - 1);

        for (int i = 0; i < points - 1; i++)
        {
            float t1 = i * step;
            float t2 = (i + 1) * step;

            float angle1 = Mathf.Lerp(0f, radius, t1) * Mathf.Deg2Rad;
            float angle2 = Mathf.Lerp(0f, radius, t2) * Mathf.Deg2Rad;

            Vector3 point1 = center + new Vector3(Mathf.Cos(angle1), 0f, Mathf.Sin(angle1)) * range;
            Vector3 point2 = center + new Vector3(Mathf.Cos(angle2), 0f, Mathf.Sin(angle2)) * range;

            Gizmos.DrawLine(point1, point2);
            Gizmos.DrawLine(center, point1);
        }

        float finalAngle = Mathf.Lerp(0f, radius, 1f) * Mathf.Deg2Rad;
        Vector3 finalPoint = center + new Vector3(Mathf.Cos(finalAngle), 0f, Mathf.Sin(finalAngle)) * range;
        Gizmos.DrawLine(center, finalPoint);
    }
}

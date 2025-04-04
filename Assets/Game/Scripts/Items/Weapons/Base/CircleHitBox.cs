using UnityEngine;

public class CircleHitBox : MonoBehaviour
{
    [SerializeField] private MeshCollider meshCollider;
    [Space]
    [SerializeField] private float range;
    [SerializeField] private float radius;
    [SerializeField] private byte points;

    void Start()
    {
        SetCircleMesh();
    }


    void SetCircleMesh()
    {
        Vector3[] vertices = new Vector3[points + 1];
        vertices[0] = Vector3.zero;

        for (int i = 1; i <= points; i++) // Создаем вершины вдоль полукруга
        {
            float angleInRadians = Mathf.Lerp(0, radius, i / (float)points) * Mathf.Deg2Rad;
            float x = range * Mathf.Cos(angleInRadians);
            float z = range * Mathf.Sin(angleInRadians);
            vertices[i] = new Vector3(x, 0f, z);
        }

        int[] triangles = new int[(points - 1) * 3];
        for (int i = 1; i < points; i++) // Создаем треугольники для MeshCollider (это будет наш полукруг)
        {
            int triIndex = (i - 1) * 3;
            triangles[triIndex] = 0;
            triangles[triIndex + 1] = i;
            triangles[triIndex + 2] = i + 1;
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

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
}

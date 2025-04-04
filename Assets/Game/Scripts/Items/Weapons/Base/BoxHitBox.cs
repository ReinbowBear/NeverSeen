using UnityEngine;

public class BoxHitBox : MonoBehaviour
{
    [SerializeField] private MeshCollider meshCollider;
    [Space]
    [SerializeField] private float height;
    [SerializeField] private float width;
    [SerializeField] private float cornerRadius;
    [SerializeField] private byte cornerPoints;

    private Vector3 basePos;

    void Start()
    {
        SetBoxMesh();
        basePos = transform.position;
    }


    void SetBoxMesh()
    {
        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(-width / 2, 0, height / 2); // Верхний левый угол
        vertices[1] = new Vector3(width / 2, 0, height / 2);  // Верхний правый угол
        vertices[2] = new Vector3(width / 2, 0, -height / 2); // Нижний правый угол
        vertices[3] = new Vector3(-width / 2, 0, -height / 2); // Нижний левый угол

        int[] triangles = new int[6];
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;


        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        meshCollider.sharedMesh = mesh;
        transform.localPosition = new Vector3 (basePos.x, basePos.y, basePos.z + height / 2);
    }
}

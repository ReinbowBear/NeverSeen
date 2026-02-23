using UnityEngine;

public class Chain : MonoBehaviour
{
    [HideInInspector] public Vector3 StartPoint;
    [HideInInspector] public Vector3 EndPoint;

    [SerializeField] private LineRenderer line;


    public void SetLine() // почему то не работает корректно, провод не провисает
    {
        for (int i = 0; i < line.positionCount; i++)
        {
            float segment = (float)i / (line.positionCount - 1);
            Vector3 pointPos = Vector3.Lerp(StartPoint, EndPoint, segment);

            Vector3 gravityDir = Vector3.down;
            float sagAmount = Mathf.Sin(segment * Mathf.PI) * 2.0f; // sagging провисание с силой 2.0f

            line.SetPosition(i, pointPos);
        }
    }
}

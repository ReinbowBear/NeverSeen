using UnityEngine;

public class Wire : MonoBehaviour
{
    [HideInInspector] public Vector3 StartPoint;
    [HideInInspector] public Vector3 EndPoint;

    [SerializeField] private LineRenderer line;


    public void SetLine() // почему то не работает корректно, провод не провисает
    {
        for (int i = 0; i < line.positionCount; i++)
        {
            float segment = i / (line.positionCount - 1);
            Vector3 pointPos = Vector3.Lerp(StartPoint, EndPoint, segment);


            float sagAmount = Mathf.Sin(segment * Mathf.PI) * 2.0f; // sagging провисание
            pointPos.y -= sagAmount;

            line.SetPosition(i, pointPos);
        }
    }
}

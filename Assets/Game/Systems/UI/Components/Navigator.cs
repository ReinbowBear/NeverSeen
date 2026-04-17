using UnityEngine;

public class Navigator : MonoBehaviour
{
    public RectTransform NavigateObj;
    public float NavigateTime = 0.1f;

    [Space]
    [HideInInspector] public Vector3 StartPos;
    [HideInInspector] public Vector3 EndPos;
    [HideInInspector] public float Elapsed;
}

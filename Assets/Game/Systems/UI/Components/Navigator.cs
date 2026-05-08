using System;
using UnityEngine;

public class Navigator : MonoBehaviour
{
    public RectTransform NavigateObj;
    public float NavigateTime = 0.1f;

    [Space]
    [NonSerialized] public Vector3 StartPos;
    [NonSerialized] public Vector3 EndPos;
    [NonSerialized] public float Elapsed;
}

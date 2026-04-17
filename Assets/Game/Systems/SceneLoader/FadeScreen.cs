using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 0.5f;

    [HideInInspector] public float From;
    [HideInInspector] public float To;

    [HideInInspector] public bool IsRunning;
    [HideInInspector] public float Time;
}

using DG.Tweening;
using UnityEngine;

public class MyCam : MonoBehaviour
{
    public static MyCam instance;

    void Awake()
    {
        instance = this;
    }


    public void Shake(float strength, float time) // Shake(0.025f, 0.4f);
    {
        Camera.main.DOShakePosition(time, strength, 10, 45f);
    }
}

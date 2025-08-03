using UnityEngine;

public class RotateToCam : MonoBehaviour
{
    private Camera cam;

    void Awake()
    {
        cam = Camera.main;
    }


    private void LateUpdate()
    {
        Vector3 direction = transform.position - cam.transform.position;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}

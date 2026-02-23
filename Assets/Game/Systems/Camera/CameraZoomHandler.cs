using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class CameraZoomHandler
{
    public Transform transform;
    public float zoomSensitivity = 1f;
    public float zoomVelocity = 10f;
    public float minZoom = 7.5f;
    public float maxZoom = 15f;

    private Camera cam;
    private float targetZoom;

    void Awake()
    {
        cam = Camera.main;
        targetZoom = Vector3.Distance(transform.position, cam.transform.position);
    }


    public void OnZoom(CallbackContext context)
    {
        Vector2 scroll = context.ReadValue<Vector2>();

        //targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
        targetZoom = Mathf.Clamp(targetZoom - scroll.y * zoomSensitivity, minZoom, maxZoom);

        _ = DoZoom();
    }


    private async Task DoZoom()
    {
        while (Mathf.Abs(Vector3.Distance(cam.transform.position, transform.position) - targetZoom) > 0.01f)
        {
            Vector3 dir = (cam.transform.position - transform.position).normalized;
            float current = Vector3.Distance(cam.transform.position, transform.position);
            float smooth = Mathf.Lerp(current, targetZoom, Time.deltaTime * zoomVelocity);

            cam.transform.position = transform.position + dir * smooth;
            await Task.Yield();
        }

        cam.transform.position = transform.position + (cam.transform.position - transform.position).normalized * targetZoom;
    }
}

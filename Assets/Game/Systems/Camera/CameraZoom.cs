using UnityEngine;

public class CameraZoom : ISystem
{
    public float zoomSensitivity = 1f;
    public float zoomVelocity = 10f;
    public float minZoom = 7.5f;
    public float maxZoom = 15f;

    private Camera cam;
    public CameraTarget camTarget;

    private Vector3 camTargetPos => camTarget.transform.position;
    private float targetZoom;

    private GamePlayInput gamePlayInput;

    public CameraZoom(GamePlayInput gamePlayInput, CameraTarget camTarget)
    {
        cam = Camera.main;
        targetZoom = Vector3.Distance(camTarget.transform.position, cam.transform.position);

        this.gamePlayInput = gamePlayInput;
        this.camTarget = camTarget;
    }

    public void SetSubs(SystemSubs subs)
    {
        subs.AddListener(UpdateZoom);
    }


    public void UpdateZoom()
    {
        if(gamePlayInput.Scroll.sqrMagnitude > 0.01f)
        {
            targetZoom = Mathf.Clamp(targetZoom - gamePlayInput.Scroll.y * zoomSensitivity, minZoom, maxZoom);
        }

        if (Mathf.Abs(Vector3.Distance(cam.transform.position, camTargetPos) - targetZoom) > 0.01f)
        {
            ZoomCamera();
        }
    }


    private void ZoomCamera()
    {
        Vector3 dir = (cam.transform.position - camTargetPos).normalized;

        float current = Vector3.Distance(cam.transform.position, camTargetPos);
        float smooth = Mathf.Lerp(current, targetZoom, Time.deltaTime * zoomVelocity);

        cam.transform.position = camTargetPos + dir * smooth;
    }
}

using UnityEngine;

public class CameraDrag : ISystem
{
    private Camera camera;
    private Transform transform => camera.transform;
    private Vector3 lastPosition;
    private Vector3 currentDragVelocity;

    private float maxPosX = 20;
    private float maxPosZ = 20;
    private float dragSensitivity = 15f;
    private float dragVelocity = 5f;

    private GamePlayInput gamePlayInput;

    public CameraDrag(GamePlayInput gamePlayInput)
    {
        camera = Camera.main;
        this.gamePlayInput = gamePlayInput;
    }

    public void SetSubs(SystemSubs subs)
    {
        subs.AddListener(UpdateCam);
    }


    public void UpdateCam()
    {
        if (!gamePlayInput.MouseRight) // MouseRightDown
        {
            lastPosition = UnityEngine.Input.mousePosition;
            currentDragVelocity = Vector3.zero;
        }

        if(gamePlayInput.MouseRight)
        {
            DragCamera();
        }
        else if(currentDragVelocity.magnitude > 0.01f)
        {
            CameraInertion();
        }
    }


    private void DragCamera()
    {
        Vector3 mouseDelta = UnityEngine.Input.mousePosition - lastPosition;
        lastPosition = UnityEngine.Input.mousePosition;

        float dx = mouseDelta.x;
        float dy = mouseDelta.y;

        dx *= dragSensitivity * Time.deltaTime;
        dy *= dragSensitivity * Time.deltaTime;

        Vector3 move = -transform.right * dx + -transform.forward * dy;
        move.y = 0f;

        currentDragVelocity = move;
        move.y = 0;

        MoveCamera(currentDragVelocity);
    }

    private void CameraInertion()
    {
        currentDragVelocity = Vector3.Lerp(currentDragVelocity, Vector3.zero, dragVelocity * Time.deltaTime);
        MoveCamera(currentDragVelocity);        
    }


    private void MoveCamera(Vector3 velocity)
    {
        Vector3 newPos = transform.position + velocity;

        newPos.x = Mathf.Clamp(newPos.x, -maxPosX, maxPosX);
        newPos.z = Mathf.Clamp(newPos.z, -maxPosZ, maxPosZ);

        transform.position = newPos;
    }
}

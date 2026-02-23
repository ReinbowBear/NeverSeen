using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class CameraDragHandler
{
    private Camera camera;

    private float maxPosX = 20;
    private float maxPosZ = 20;
    private float dragSensitivity = 15f;
    private float dragVelocity = 5f;

    private Transform transform;
    private bool isDragging;
    private Vector3 currentDragVelocity;


    public void OnDragStarted(CallbackContext context)
    {
        isDragging = false;
        _ = DoDragging(context);
    }


    private async Task DoDragging(CallbackContext context)
    {
        isDragging = true;
        Vector3 lastPosition = UnityEngine.Input.mousePosition;

        while (context.action.IsPressed() && isDragging)
        {
            Vector3 delta = UnityEngine.Input.mousePosition - lastPosition;
            lastPosition = UnityEngine.Input.mousePosition;

            delta.x /= Screen.width;
            delta.y /= Screen.height;

            Vector3 move = -transform.right * delta.x - transform.forward * delta.y;
            move.y = 0;

            currentDragVelocity = move.normalized * delta.magnitude * dragSensitivity;
            MoveCamera(currentDragVelocity);
            await Task.Yield();
        }


        while (currentDragVelocity.magnitude > 0.01f && isDragging) // инерция
        {
            currentDragVelocity = Vector3.Lerp(currentDragVelocity, Vector3.zero, Time.deltaTime * dragVelocity);
            MoveCamera(currentDragVelocity);
            await Task.Yield();
        }

        currentDragVelocity = Vector3.zero;
    }

    private void MoveCamera(Vector3 velocity)
    {
        Vector3 newPos = transform.position + velocity;
        newPos.x = Mathf.Clamp(newPos.x, -maxPosX, maxPosX);
        newPos.z = Mathf.Clamp(newPos.z, -maxPosZ, maxPosZ);
        transform.position = newPos;
    }
}

using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class CameraRotateHandler
{
    public Transform transform;
    public float rotateSensitivity = 140f;
    public float rotateVelocity = 7.5f;


    public void OnRotateLeft(CallbackContext context)
    {
        _ = RotateLeft(context);
    }

    public void OnRotateRight(CallbackContext context)
    {
        _ = RotateRight(context);
    }


    private async Task RotateLeft(CallbackContext context)
    {
        float currentVelocity = 0;

        while (context.action.IsPressed() || Mathf.Abs(currentVelocity) > 0.01f)
        {
            currentVelocity = Mathf.Lerp(currentVelocity, rotateSensitivity, Time.deltaTime * rotateVelocity);
            transform.rotation *= Quaternion.Euler(0f, currentVelocity * Time.deltaTime, 0f);
            await Task.Yield();
        }
    }

    private async Task RotateRight(CallbackContext context)
    {
        float currentVelocity = 0;

        while (context.action.IsPressed() || Mathf.Abs(currentVelocity) > 0.01f)
        {
            currentVelocity = Mathf.Lerp(currentVelocity, -rotateSensitivity, Time.deltaTime * rotateVelocity);
            transform.rotation *= Quaternion.Euler(0f, currentVelocity * Time.deltaTime, 0f);
            await Task.Yield();
        }
    }
}

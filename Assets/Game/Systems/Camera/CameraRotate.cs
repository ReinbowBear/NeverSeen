using UnityEngine;

public class CameraRotate : ISystem
{
    private Camera camera;
    private Transform transform => camera.transform;
    private float currentVelocity;

    public float rotateSensitivity = 140f;
    public float rotateVelocity = 7.5f;

    private GamePlayInput gamePlayInput;

    public CameraRotate(GamePlayInput gamePlayInput)
    {
        camera = Camera.main;
        this.gamePlayInput = gamePlayInput;
    }

    public void SetSubs(SystemSubs subs)
    {
        subs.AddListener(UpdateRotation);
    }


    public void UpdateRotation()
    {
        var result = Mathf.Abs(currentVelocity);

        if(gamePlayInput.Q || result > 0.01f)
        {
            RotateCamera(-1);
        }
        else if (gamePlayInput.E || result > 0.01f)
        {
            RotateCamera(1);
        }
    }


    private void RotateCamera(int direction)
    {
        float targetVelocity = rotateSensitivity * direction;

        currentVelocity = Mathf.Lerp(currentVelocity, targetVelocity, Time.deltaTime * rotateVelocity);
        transform.rotation *= Quaternion.Euler(0f, currentVelocity * Time.deltaTime, 0f);        
    }
}

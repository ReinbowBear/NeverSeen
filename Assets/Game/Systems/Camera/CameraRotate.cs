using UnityEngine;

public class CameraRotate : ISystem
{
    private Camera camera;
    private Transform transform => camera.transform;
    private float currentVelocity;

    public float rotateSensitivity = 140f;
    public float rotateVelocity = 7.5f;

    public CameraRotate()
    {
        camera = Camera.main;
    }

    public void Execute(World world, EntityCommands commands)
    {
        foreach (var (rotate, entity) in world.Query<IntentCameraRotate>())
        {
            var result = Mathf.Abs(currentVelocity);

            if(result > 0.01f)
            {
                RotateCamera(rotate.Direction);
            }
        }
    }


    private void RotateCamera(int direction)
    {
        float targetVelocity = rotateSensitivity * direction;

        currentVelocity = Mathf.Lerp(currentVelocity, targetVelocity, Time.deltaTime * rotateVelocity);
        transform.rotation *= Quaternion.Euler(0f, currentVelocity * Time.deltaTime, 0f);        
    }
}

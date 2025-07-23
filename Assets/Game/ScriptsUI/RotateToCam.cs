using UnityEngine;

public class RotateToCam : MonoBehaviour
{
    private Vector3 direction;
    
    void Awake()
    {
        direction = Camera.main.transform.forward;
        LookToCam();
    }

    private void LookToCam()
    {
        Quaternion newRotation = Quaternion.LookRotation(direction);
        transform.rotation = newRotation;
    }
}

using UnityEngine;

public class CamFolow : MonoBehaviour
{
    [SerializeField] private Transform playerModel;
    [SerializeField] private float speed;

    void LateUpdate()
    {
        Vector3 targetPos = playerModel.position;
        transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
    }
}

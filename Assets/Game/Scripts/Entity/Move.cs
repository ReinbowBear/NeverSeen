using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject model;
    [Space]
    [SerializeField] private float moveSpeed;
    //[SerializeField] private float rotationSpeed;

    public void MoveTo(Vector2 direction)
    {
        Vector3 newDirection = new Vector3(direction.x, 0, direction.y).normalized;
        rb.AddForce(newDirection * moveSpeed, ForceMode.Impulse);

        //Quaternion targetRotation = Quaternion.LookRotation(newDirection);
        //model.transform.rotation = Quaternion.RotateTowards(model.transform.rotation, targetRotation, rotationSpeed);
        model.transform.LookAt(model.transform.position + newDirection);
    }
}

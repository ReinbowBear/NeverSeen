using System.Collections;
using UnityEngine;

public class EnMove : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject model;
    [Space]
    [SerializeField] private Transform target;
    [SerializeField] private float keepDist;
    [Space]
    [SerializeField] private float speed;
    [SerializeField] private float rotate;

    void FixedUpdate()
    {
        Vector3 direction = target.position - transform.position;

        if (direction.magnitude > keepDist)
        {
            //rb.AddForce(direction.normalized * speed, ForceMode.Impulse);

            Vector3 velocity = direction.normalized * speed;
            rb.linearVelocity = velocity;
        }

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        model.transform.rotation = Quaternion.RotateTowards(model.transform.rotation, targetRotation, rotate);
    }

    public IEnumerator RotateTo(Transform target, float duration)
    {
        Quaternion startRotation = model.transform.rotation;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            Quaternion targetRotation = Quaternion.LookRotation(target.position - model.transform.position);
            model.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, timeElapsed / duration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
    }
}

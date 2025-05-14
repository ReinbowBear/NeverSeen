using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [HideInInspector] public string ownerTag;
    [HideInInspector] public Vector3 direction;


    [HideInInspector] public short damage;
    [HideInInspector] public float speed;

    void FixedUpdate()
    {
        rb.linearVelocity = direction * speed;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == ownerTag)
        {
            return;
        }

        if (other.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage(damage);
        }

        if (other.tag != "Reflect")
        {
            Destroy(gameObject);
        }
    }
}

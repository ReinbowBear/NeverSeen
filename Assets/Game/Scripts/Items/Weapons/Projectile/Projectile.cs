using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public GameObject shootingEntity;

    [HideInInspector] public short damage;
    [HideInInspector] public float speed;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == shootingEntity)
        {
            Debug.Log("shootingEntity");
            return;
        }

        if (other.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage(damage);
        }

        Destroy(gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        shootingEntity = null;
    }
}

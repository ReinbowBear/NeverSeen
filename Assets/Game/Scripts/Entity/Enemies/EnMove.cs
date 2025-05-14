using System.Collections;
using UnityEngine;

public class EnMove : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject model;
    [Space]
    [SerializeField] private Transform target;
    [SerializeField] private float keepDistToPlayer;
    [SerializeField] private float keepDistToEnemy;
    [Space]
    [SerializeField] private float speed;
    [SerializeField] private float rotate;

    [HideInInspector] public SphereCollider trigerCollider;

    void Start()
    {
        trigerCollider = gameObject.AddComponent<SphereCollider>(); // коллайдер для тригера на персонажа
        trigerCollider.isTrigger = true;

        trigerCollider.radius = keepDistToEnemy;
        trigerCollider.excludeLayers = 1 << LayerMask.NameToLayer("HitBox");
    }


    void FixedUpdate()
    {
        Vector3 toPlayer = target.position - transform.position;
        Vector3 moveDirection = Vector3.zero;

        if (toPlayer.magnitude > keepDistToPlayer)
        {
            moveDirection += toPlayer.normalized;
        }


        Collider[] hits = Physics.OverlapSphere(transform.position, keepDistToEnemy, LayerMask.GetMask("Default"));
        foreach (var other in hits)
        {
            if (other.CompareTag("Enemy"))
            {
                if (other.gameObject == this.gameObject) continue;

                Vector3 toEnemy = transform.position - other.transform.position;
                float dist = toEnemy.magnitude;

                moveDirection += toEnemy.normalized * (keepDistToEnemy / dist);
            }
        }

        moveDirection = moveDirection.normalized;
        rb.linearVelocity = moveDirection * speed;

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            model.transform.rotation = Quaternion.RotateTowards(model.transform.rotation, targetRotation, rotate);
        }
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


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //TryAttack(other.transform);
        }
    }
}

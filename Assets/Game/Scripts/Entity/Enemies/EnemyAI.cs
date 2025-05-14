using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Character character;
    [Space]
    [SerializeField] private float attackDelay;
    [SerializeField] private float stunnTime;
    [SerializeField] private float stunnDelay;

    private SphereCollider trigerCollider;

    void Start()
    {
        trigerCollider = gameObject.AddComponent<SphereCollider>(); // коллайдер для тригера на персонажа
        trigerCollider.isTrigger = true;

        float dist = 0;
        for (byte i = 0; i < character.inventory.weapons.Length; i++)
        {
            //if (dist < character.inventory.weapons[i].)
            //{
            //    dist = character.inventory.weapons[i].atttackDist;
            //}
        }
        trigerCollider.radius = dist;
        trigerCollider.excludeLayers = 1 << LayerMask.NameToLayer("HitBox");
    }

    private IEnumerator Ai()
    {
        yield return new WaitForSeconds(0);
    }

    public void TryAttack(Transform target)
    {
        if (EnemyManager.instance.isSomeoneAttacking == true)
        {
            return;
        }

        for (byte i = 0; i < character.inventory.weapons.Length; i++)
        {
            //if (Vector3.Distance(transform.position, target.position) < character.inventory.weapons[i].atttackDist)
            //{
            //    weaponToAttack = character.inventory.weapons[i];
            //    break;
            //}
        }

        //if (weaponToAttack != null)
        //{
        //    StartCoroutine(Attack(weaponToAttack, target));
        //}
    }

    private Coroutine coroutine;

    private void OnTakeDamage()
    {
        if (coroutine != null)
        {
            return;
        }

        if (character.state == State.attack)
        {
            coroutine = StartCoroutine(Stunn());
        }
    }

    private IEnumerator Stunn()
    {
        character.state = State.stunn;
        yield return stunnTime;

        character.state = State.None;
        yield return stunnDelay;

        coroutine = null;
    }


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TryAttack(other.transform);
        }
    }


    void OnEnable()
    {
        character.health.onDamageTake += OnTakeDamage;
    }

    void OnDisable()
    {
        character.health.onDamageTake -= OnTakeDamage;
    }
}

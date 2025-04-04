using System.Collections;
using UnityEngine;

public class EnCombat : MonoBehaviour
{
    [SerializeField] private ActionShow actionShow;
    [SerializeField] private EnMove enMove;
    [SerializeField] private EnemyWeaponConfig[] weapons;
    [HideInInspector] public SphereCollider trigerCollider;

    void Start()
    {
        trigerCollider = gameObject.AddComponent<SphereCollider>(); // коллайдер для тригера на персонажа
        trigerCollider.isTrigger = true;

        float dist = 0;
        for (byte i = 0; i < weapons.Length; i++)
        {
            if (dist < weapons[i].atttackDist)
            {
                dist = weapons[i].atttackDist;
            }
        }
        trigerCollider.radius = dist;
        trigerCollider.excludeLayers = 1 << LayerMask.NameToLayer("HitBox");
    }


    public void TryAttack(Transform target)
    {
        if (EnemyManager.instance.isSomeoneAttacking == true)
        {
            return;
        }

        EnemyWeaponConfig weaponToAttack = null;
        for (byte i = 0; i < weapons.Length; i++)
        {
            if (Vector3.Distance(transform.position, target.position) < weapons[i].atttackDist)
            {
                weaponToAttack = weapons[i];
                break;
            }
        }


        if (weaponToAttack != null)
        {
            StartCoroutine(Attack(weaponToAttack, target));
        }
    }


    public IEnumerator Attack(EnemyWeaponConfig weaponToAttack, Transform target)
    {
        EnemyManager.instance.isSomeoneAttacking = true;
        trigerCollider.enabled = false;
        enMove.enabled = false;
        
        StartCoroutine(enMove.RotateTo(target, weaponToAttack.weapon.prepare));
        weaponToAttack.weapon.StartCoroutine(weaponToAttack.weapon.Attack()); // при стане атака оружием не выключается

        actionShow.SetMat(1);
        yield return new WaitForSeconds(weaponToAttack.weapon.prepare);
        actionShow.SetMat(2);
        yield return new WaitForSeconds(weaponToAttack.weapon.attack);
        actionShow.SetMat(3);
        yield return new WaitForSeconds(weaponToAttack.weapon.ending);
        actionShow.SetMat(0);

        EnemyManager.instance.isSomeoneAttacking = false;

        yield return new WaitForSeconds(weaponToAttack.attackDelay);
        trigerCollider.enabled = true;
        enMove.enabled = true;
    }


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TryAttack(other.transform);
        }
    }
}

[System.Serializable]
public class EnemyWeaponConfig
{
    public Weapon weapon;
    public float atttackDist;
    public float attackDelay;
}

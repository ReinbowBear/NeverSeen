using System.Collections;
using UnityEngine;

public class Weapon : Applicable
{
    [HideInInspector] public Character character;
    [SerializeField] protected WeaponSO weaponSO;
    [HideInInspector] public WeaponSO stats;
    protected bool isColldown;
    protected float cooldown;

    protected virtual void Awake()
    {
        stats = Instantiate(weaponSO);
    }


    public override IEnumerator Reload()
    {
        isColldown = true;
        cooldown = 0;
        while (cooldown != stats.reloadTime)
        {
            character.weaponControl.wpBar.ChangeBar(cooldown, stats.reloadTime);

            cooldown += Time.deltaTime;
            yield return null;
        }
        isColldown = false;
    }

    public override void Activate()
    {
        if (isColldown != true)
        {
            character.combatManager.AddAction(this);
        }
    }

    public override IEnumerator Attacking()
    {
        for (byte i = 0; i < character.battleMap.enemyPoints.Length; i++)
        {
            if (character.battleMap.enemyPoints[i].childCount < 0 && stats.targets[i] == true)
            {
                character.battleMap.enemyPoints[i].GetComponent<Enemy>().health.TakeDamage(stats.damage);
                yield return null;
            }
        }
    }
}

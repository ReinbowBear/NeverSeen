using System.Collections;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private Inventory inventory;
    [SerializeField] private ActionShow actionShow;
    [SerializeField] private Move move;
    
    private int previousWeapon;

    public void CheckAttack(int index, Vector3 targetPos)
    {
        Weapon weapon = inventory.weapons[index];

        if (weapon == null || weapon.isInAttack == true)
        {
            return;
        }

        if (inventory.weapons[previousWeapon].isInAttack == true) // кансел атаки другой способностью
        {
            StopAllCoroutines();
            inventory.weapons[previousWeapon].FalseAttack();
        }

        previousWeapon = index;

        StartCoroutine(move.RotateTo(targetPos, weapon.prepare));
        StartCoroutine(DoAttack(weapon));
    }

    private IEnumerator DoAttack(Weapon weapon)
    {
        character.state = CharacterState.attack;

        weapon.StartCoroutine(weapon.Attack());

        actionShow.SetMat(1);
        yield return new WaitForSeconds(weapon.prepare);

        actionShow.SetMat(2);
        yield return new WaitForSeconds(weapon.attack);

        actionShow.SetMat(3);
        yield return new WaitForSeconds(weapon.ending);

        actionShow.SetMat(0);
        character.state = CharacterState.None;
    }
}


using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AbilityControl
{
    public Entity character;

    private List<Ability> Actions = new List<Ability>();
    private Coroutine myCoroutine;


    public void ChoseAbility(byte index)
    {
        Ability ability = character.inventory.abilitys[index];

        if (ability == null || character.currentStats.manna < ability.stats.mannaCost || ability.cooldown == null)
        {
            return;
        }

        character.weaponPoint.SetHandWeapon(ability.stats);
        AddAction(ability);
        character.manna.TakeManna(ability.stats.mannaCost);
        ability.cooldown = character.StartCoroutine(ability.Reload());

        DOTween.Sequence()
            .SetLink(character.gameObject)
            .Append(character.transform.DOScale(new Vector3(1.1f, 0.8f, 1.1f), 0.25f))
            .Append(character.transform.DOScale(new Vector3(1, 1, 1), 0.25f));
    }


    public void AddAction(Ability ability)
    {
        Actions.Add(ability);

        if (myCoroutine == null)
        {
            myCoroutine = character.StartCoroutine(DoAction());
        }
    }

    private IEnumerator DoAction()
    {
        while (Actions.Count > 0)
        {
            yield return character.StartCoroutine(Actions[0].Activate());
            yield return new WaitForSeconds(1);
            Actions.RemoveAt(0);
        }
        
        myCoroutine = null;
    }


    public void AddAbility(Ability newAbility, byte index)
    {
        character.inventory.abilitys[index] = newAbility;
        newAbility.character = character;

        newAbility.transform.SetParent(character.weaponModel.transform);
        newAbility.gameObject.SetActive(false);
    }

    public void RemoveAbility(byte index)
    {
        Address.DestroyAsset(character.inventory.abilitys[index].gameObject);
        character.inventory.abilitys[index] = null;
    }
}

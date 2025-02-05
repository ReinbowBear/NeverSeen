using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AbilityControl
{
    public Entity character;

    private List<Ability> Actions = new List<Ability>();
    private Coroutine myCoroutine;


    public void ChoseAbility(int index)
    {
        Ability ability = character.inventory.abilities[index];

        if (ability == null || ability.cooldown != null)
        {
            return;
        }

        character.weaponPoint.SetHandWeapon(ability.stats);

        AddAction(ability);
        ability.cooldown = character.StartCoroutine(ability.Reload(character.inventoryUI.abilitySlots[index]));

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
            Actions.RemoveAt(0);
        }
        
        myCoroutine = null;
    }


    public void AddAbility(Ability newAbility, byte index)
    {
        character.inventory.abilities[index] = newAbility;
        newAbility.character = character;

        newAbility.transform.SetParent(character.weaponModel.transform);
        newAbility.gameObject.SetActive(false);
    }

    public void RemoveAbility(byte index)
    {
        Address.DestroyAsset(character.inventory.abilities[index].gameObject);
        character.inventory.abilities[index] = null;
    }
}

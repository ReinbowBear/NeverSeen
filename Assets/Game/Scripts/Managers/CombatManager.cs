using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    private List<Ability> Actions = new List<Ability>();
    private Coroutine myCoroutine;


    public void AddAction(Ability ability)
    {
        Actions.Add(ability);

        if (myCoroutine == null)
        {
            myCoroutine = StartCoroutine(DoAction());
        }
    }

    private IEnumerator DoAction()
    {
        while (Actions.Count > 0)
        {
            yield return StartCoroutine(Actions[0].Activate());
            yield return new WaitForSeconds(0.5f);
            Actions.RemoveAt(0);
        }
        myCoroutine = null;
    }


    private void GetCharacter(MyEvent.OnEntityInit CharacterInstantiate)
    {
        CharacterInstantiate.entity.combatManager = this;
    }


    void OnEnable()
    {
        EventBus.Add<MyEvent.OnEntityInit>(GetCharacter);
    }

    void OnDisable()
    {
        EventBus.Remove<MyEvent.OnEntityInit>(GetCharacter);
    }
}

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
            StartCoroutine(DoAction());
        }
    }

    private IEnumerator DoAction()
    {
        while (Actions.Count > 0)
        {
            Actions[0].Activate();
            yield return Actions[0].AttackCorutine;
            
            Actions.RemoveAt(0);
        }
        
        myCoroutine = null;
    }


    private void GetCharacter(MyEvent.OnCharacterInit CharacterInstantiate)
    {
        CharacterInstantiate.character.combatManager = this;
    }


    void OnEnable()
    {
        EventBus.Add<MyEvent.OnCharacterInit>(GetCharacter);
    }

    void OnDisable()
    {
        EventBus.Remove<MyEvent.OnCharacterInit>(GetCharacter);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    private List<Applicable> Actions = new List<Applicable>();
    private Coroutine myCoroutine;


    public void AddAction(Applicable applicable)
    {
        Actions.Add(applicable);

        if (myCoroutine == null)
        {
            StartCoroutine(DoAction());
        }
    }

    private IEnumerator DoAction()
    {
        while (Actions.Count > 0)
        {
            yield return StartCoroutine(Actions[0].Attacking());
            Actions.RemoveAt(0);

            yield return new WaitForSeconds(0.1f);
            
            Debug.Log("тест");
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

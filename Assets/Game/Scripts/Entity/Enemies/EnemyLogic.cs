using System.Collections;
using DG.Tweening;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    [HideInInspector] public Entity character;

    private Ability[] abilities; // просто для удобства что бы не писать длинные ссылки
    private Ability chosenMove;
    private Coroutine myCoroutine;

    public void Init()
    {
        abilities = character.inventory.abilities;
        //CheckMoves();
    }

    
    private void CheckMoves()
    {
        for (byte i = 0; i < abilities.Length; i++)
        {
            if (abilities[i].target.GetTarget(character)[0] != null )
            {
                character.abilityControl.ChoseAbility(i);

                if (myCoroutine == null)
                {
                    myCoroutine = StartCoroutine(WaitForAttack());
                }
                break;
            }
        }

        Stop(null);
    }

    private void ChoseAbility(byte index)
    {
        chosenMove = character.inventory.abilities[index];
    }

    private IEnumerator WaitForAttack()
    {
        //if (character.manna.coroutine == null) // после остановки врага если ему нечего делать, он так же останавливает реген манны...
        //{
        //    character.manna.TakeManna(0);
        //}
//
        //yield return character.manna.coroutine;
        //AttackAbility(chosenMove);
        yield return null;
    }

    private void AttackAbility(Ability ability)
    {
        character.weaponPoint.SetHandWeapon(ability.stats);

        StartCoroutine(ability.Activate());
        //character.manna.TakeManna((byte)character.currentStats.manna);
        StartCoroutine(WaitForAttack());

        DOTween.Sequence()
            .SetLink(gameObject)
            .Append(transform.DOScale(new Vector3(1.1f, 0.8f, 1.1f), 0.25f))
            .Append(transform.DOScale(new Vector3(1, 1, 1), 0.25f));
    }


    private void Stop(MyEvent.OnEndLevel _)
    {
        StopCoroutine(myCoroutine);
        myCoroutine = null;
    }

    void OnEnable()
    {
        EventBus.Add<MyEvent.OnEndLevel>(Stop);
    }

    void OnDisable()
    {
        EventBus.Remove<MyEvent.OnEndLevel>(Stop);
    }

    void OnDestroy()
    {
        EventBus.Invoke<MyEvent.OnEnemyDeath>();
    }
}

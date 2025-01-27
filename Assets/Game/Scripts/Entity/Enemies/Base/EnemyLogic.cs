using System.Collections;
using DG.Tweening;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    [HideInInspector] public Entity character;
    [HideInInspector] public EnemySO enemySO;

    private Ability chosenMove;
    private Coroutine myCoroutine;

    public void Init()
    {
        character.manna.TakeManna((byte)character.currentStats.manna);
        CheckMoves();
    }

    
    private void CheckMoves()
    {
        for (byte i = 0; i < character.inventory.abilitys.Length; i++)
        {
            if (enemySO.trigers[i].CheckTriger(character))
            {
                enemySO.trigers[i].DoAction(character);

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
        chosenMove = character.inventory.abilitys[index];
    }

    private IEnumerator WaitForAttack()
    {
        if (character.manna.coroutine == null) // после остановки врага если ему нечего делать, он так же останавливает реген манны...
        {
            character.manna.TakeManna(0);
        }

        yield return character.manna.coroutine;
        AttackAbility(chosenMove);
    }

    private void AttackAbility(Ability ability)
    {
        character.weaponPoint.SetHandWeapon(ability.stats);

        StartCoroutine(ability.Activate());
        character.manna.TakeManna((byte)character.currentStats.manna);
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
        
        StopCoroutine(character.manna.coroutine);
        character.manna.coroutine = null;
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

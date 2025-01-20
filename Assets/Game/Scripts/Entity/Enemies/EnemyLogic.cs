using System.Collections;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    [HideInInspector] public Entity character;

    public void Init()
    {
        character.currentStats.manna = 0;
        character.manna.mpBar.ChangeBar(character.baseStats.manna ,character.currentStats.manna); //не хочу запускать анимацию так что не брал манну на прямую
        character.manna.coroutine = character.manna.StartCoroutine(character.manna.MannaRegen());

        StartCoroutine(Attack());
    }
    

    public IEnumerator Attack()
    {
        yield return character.manna.coroutine;
        while(true)
        {
            for (byte i = 0; i < character.abilityControl.abilitys.Length; i++)
            {
                if (character.abilityControl.abilitys[i] == null)
                {
                    continue;
                }
                if (character.abilityControl.abilitys[i].target.GetTarget(character.battleMap, character)[0] != null)
                {
                    ChoseAbility(i);
                    yield return character.manna.coroutine;
                }
            }
        }
    }

    public void ChoseAbility(byte index)
    {
        Ability ability = character.abilityControl.abilitys[index];

        ability.gameObject.SetActive(true);
        ability.Prepare();
        character.manna.TakeManna((byte)character.currentStats.manna);
    }


    public void OtherDeath(MyEvent.OnEnemyDeath _)
    {
        character.move.MoveForward();
    }

    private void EndBattle(MyEvent.OnEndLevel _)
    {
        StopAllCoroutines();
    }

    void OnEnable()
    {
        EventBus.Add<MyEvent.OnEnemyDeath>(OtherDeath);
        EventBus.Add<MyEvent.OnEndLevel>(EndBattle);
    }

    void OnDisable()
    {
        EventBus.Remove<MyEvent.OnEnemyDeath>(OtherDeath);
        EventBus.Remove<MyEvent.OnEndLevel>(EndBattle);
    }
}

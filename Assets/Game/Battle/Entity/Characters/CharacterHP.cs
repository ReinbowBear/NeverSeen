using System;
using UnityEngine;

public class CharacterHP : Health
{
    public static Action<GameObject> onDead;

    protected override void Death()
    {
        onDead.Invoke(gameObject); //при смерти выпадают предметы если такие есть, это Instantiate, а потому нельзя пихать событие в OnDestroy
        Debug.Log("ты ведь помнишь что у тебя абстрактный класс Health есть?");
        base.Death();
    }
}

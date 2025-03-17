using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    private List<Weapon> toAttack = new List<Weapon>();
    private Coroutine coroutine;

    void Awake()
    {
        instance = this;   
    }


    public void AddAttack(Weapon attack)
    {
        toAttack.Add(attack);

        if (coroutine == null)
        {
            coroutine = StartCoroutine(DoAttack());
        }
    }

    private IEnumerator DoAttack()
    {
        while (toAttack.Count > 0)
        {
            toAttack[0].Attack();
            yield return toAttack[0].corutine;

            toAttack.RemoveAt(0);
        }
        yield return new WaitForSeconds(0);
        coroutine = null;
    }
}

[Flags]
public enum OverlapAttack
{
    Close,
    Range,
    Splash,
}

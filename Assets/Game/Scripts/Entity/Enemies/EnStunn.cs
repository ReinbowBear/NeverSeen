using System.Collections;
using UnityEngine;

public class EnStunn : MonoBehaviour
{
    [SerializeField] private Health health;
    [Space]
    [SerializeField] private EnCombat enCombat;
    [SerializeField] private EnMove enMove;
    [Space]
    [SerializeField] private float stunnTime;
    [SerializeField] private float stunnDelay;

    private Coroutine coroutine;

    private void OnTakeDamage()
    {
        if (coroutine != null)
        {
            return;
        }

        if (enCombat.trigerCollider.enabled == false) // если тригер колайдер выключен, значит враг был в состоянии атаки
        {
            coroutine = StartCoroutine(Stunn());
        }
    }

    private IEnumerator Stunn()
    {
        enCombat.enabled = false;
        enMove.enabled = false;

        yield return stunnTime;
        enCombat.enabled = true;
        enMove.enabled = true;

        enCombat.trigerCollider.enabled = true;

        yield return stunnDelay;
        coroutine = null;
    }


    void OnEnable()
    {
        health.onDamageTake += OnTakeDamage;
    }

    void OnDisable()
    {
        health.onDamageTake -= OnTakeDamage;
    }
}

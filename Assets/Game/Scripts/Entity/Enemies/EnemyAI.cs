using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Dictionary<string, EnemyState> enemysStates = new Dictionary<string, EnemyState>();
    private EnemyState currentState;

    void Start()
    {
        StartCoroutine(DoState());
    }


    public void SetState(string newState)
    {
        if (enemysStates.ContainsKey(newState))
        {
            currentState = enemysStates[newState];
        }
    }

    private IEnumerator DoState()
    {
        while (this.enabled)
        {
            yield return StartCoroutine(currentState.DoAction(this));
            CheckState();
        }
    }

    public void CheckState()
    {

    }
}

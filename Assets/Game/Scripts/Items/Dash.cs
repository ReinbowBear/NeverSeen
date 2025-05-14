using System.Collections;
using UnityEngine;

public class Dash : Ability
{
    [Space]
    [SerializeField] private float distance;
    [SerializeField] private float time;

    public override IEnumerator Use(Character owner)
    {
        owner.state = State.attack;

        Vector3 startPos = owner.transform.position;
        Vector3 targetPos = startPos + transform.forward * distance; // transform.forward без owner потому что скрипт персонажа на коренном объекте и всегда смотрит глобал Z

        if (Physics.Raycast(startPos, transform.forward, out RaycastHit hit, distance, LayerMask.GetMask("Default")))
        {
            targetPos = hit.point; // остановим дэш на препятствии
        }

        float timeElapsed = 0f;
        while (timeElapsed < time)
        {
            owner.transform.position = Vector3.Lerp(startPos, targetPos, timeElapsed / time); // игрок проходит сквозь препядствия дешом! 
            timeElapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        owner.state = State.None;
    }
}

using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObject/Other/Enemy")]
public class EnemySO : EntitySO
{
    [Space]
    public EnemyTriger[] trigers; // тригеры для действий проверяются по очереди, применяется первый сработавший
}

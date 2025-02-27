using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObject/Entity/Enemy")]
public class EnemySO : CharacterSO
{
    [Space]
    public EnemyTriger[] trigers; // тригеры для действий проверяются по очереди, применяется первый сработавший
}

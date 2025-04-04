using System;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [HideInInspector] public bool isSomeoneAttacking;

    void Awake()
    {
        instance = this;   
    }
}

[Flags]
public enum OverlapAttack
{
    Close,
    Range,
    Splash,
}

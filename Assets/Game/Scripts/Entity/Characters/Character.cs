using UnityEngine;

public class Character : MonoBehaviour
{
    [HideInInspector] public CharacterState state;
}

public enum CharacterState
{
    None,
    attack,
    move,
    stunn,
}

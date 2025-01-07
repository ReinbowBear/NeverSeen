using UnityEngine;

[CreateAssetMenu(fileName = "EffectCont", menuName = "ScriptableObject/EffectCont")]
public class EffectContainer : ScriptableObject
{
    public byte value;
    public float duration;
    [Space]
    public MeshRenderer model;
    public AudioClip sound;
}

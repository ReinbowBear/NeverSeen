using UnityEngine;

[CreateAssetMenu(fileName = "AudioSO", menuName = "Scriptable Objects/AudioSO")]
public class SoundSO : ScriptableObject
{
    public SoundType soundType;
    public AudioClip Sound;

    [Header("Effects")]
    public bool IsPitch;
    public bool IsStacked;
    public bool IsLoud;
}

public enum SoundType
{
    Music, 
    Ambience,
    SFX, 
    UI, 
    Dialogues
}

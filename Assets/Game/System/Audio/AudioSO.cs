using UnityEngine;

[CreateAssetMenu(fileName = "AudioSO", menuName = "Scriptable Objects/AudioSO")]
public class AudioSO : ScriptableObject
{
    [Header("General")]
    public AudioClip Sound;
    public SoundType Type;

    [Header("Effects")]
    public bool IsLoud;
    public bool IsStacked;
}

public enum SoundType
{
    UI, SFX, Ambience, Music, Dialogues
}
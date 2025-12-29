using UnityEngine;

[CreateAssetMenu(fileName = "AudioSO", menuName = "Scriptable Objects/AudioSO")]
public class SoundSO : ScriptableObject
{
    [Header("General")]
    public AudioClip Sound;
    public SoundType Type;
    public SoundTag Tag;

    [Header("Effects")]
    public bool IsLoud;
    public bool IsStacked;
}

public enum SoundType
{
    UI, SFX, Ambience, Music, Dialogues
}

public enum SoundTag
{
    Impact, Interaction, Notification, Success, Failure, Environment, Ambience, Voice
}

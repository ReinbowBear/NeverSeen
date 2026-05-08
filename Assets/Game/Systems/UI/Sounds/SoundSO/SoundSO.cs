using UnityEngine;

[CreateAssetMenu(fileName = "AudioSO", menuName = "Scriptable Objects/AudioSO")]
public class SoundSO : ScriptableObject
{
    public SoundType soundType;
    public AudioClip Clip;

    [Header("Effects")]
    public bool IsPitch;
    public bool IsStacked;
    public bool IsShepard;
    public bool Is3D;
    
    public bool IsLoud; // разумно было бы сделать параметров и высчитывать соотношения по алгоритму
}

public enum SoundType
{
    Music, 
    Ambience,
    SFX, 
    UI, 
    Dialogues
}

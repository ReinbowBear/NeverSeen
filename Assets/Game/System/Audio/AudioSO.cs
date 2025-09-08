using UnityEngine;

[CreateAssetMenu(fileName = "AudioSO", menuName = "Scriptable Objects/AudioSO")]
public class AudioSO : ScriptableObject
{
    public SoundData[] AudioClips;

    public SoundData GetByName(string soundName)
    {
        foreach (var soundData in AudioClips)
        {
            if (soundData.Name == soundName)
            {
                return soundData;
            }
        }
        return default;
    }
}

[System.Serializable]
public struct SoundData
{
    public string Name;
    public AudioClip Sound;
    public SoundType Type;
    public bool IsLoud;
    public bool IsPitchStacked;
}

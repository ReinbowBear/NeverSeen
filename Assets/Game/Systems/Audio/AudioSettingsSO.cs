using UnityEngine;

[CreateAssetMenu(fileName = "AudioSettingsSO", menuName = "Scriptable Objects/AudioSettingsSO")]
public class AudioSettingsSO : ScriptableObject
{
    [Header("Pitch Settings")]
    public float pitchIncrement = 0.15f;
    public float pitchResetDelay = 1.5f;
    public float maxPitch = 2.5f;
    public float minPitchAfterReset = 1f;
    public float maxRandomizedReset = 1.2f;

    [Header("Music Volume Settings")]
    public float musicPauseVolume = -80f;
    public float musicNormalVolume = 0f;
    public float musicFadeOutTime = 1.5f;
    public float musicFadeInTime = 0.5f;
}

public static class AudioMixerParams
{
    public const string MasterVolume = "MasterVolume";

    public const string MusicVolume = "MusicVolume";
    public const string SFXVolume = "SFXVolume";
    public const string UIVolume = "UIVolume";
    public const string DialoguesVolume = "DialoguesVolume";
    public const string AmbienceVolume = "AmbienceVolume";

    public const string DuckingAmount = "DuckingAmount";

    public const string ReverbAmount = "ReverbAmount";
    public const string LowpassCutoff = "LowpassCutoff";
    public const string PitchShift = "PitchShift";
}

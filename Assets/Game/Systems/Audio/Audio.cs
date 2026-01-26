using UnityEngine;
using System.Collections.Generic;

public class Audio
{
    private AudioSettingsSO settings;
    private Dictionary<SoundSO, PitchStackData> pitchStacks = new();

    public Audio(AudioSettingsSO settings)
    {
        this.settings = settings;
    }


    public AudioPlayResult GetAudioPlayResult(SoundSO sound, float currentTime)
    {
        float pitch = 1f;

        if (sound.IsStacked)
        {
            pitch = GetStackedPitch(sound, currentTime);
        }
        else if (sound.IsPitch)
        {
            pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        }

        return new AudioPlayResult
        {
            Pitch = pitch,
            IsLoud = sound.IsLoud,
            Clip = sound.Sound,
        };
    }

    private float GetStackedPitch(SoundSO sound, float time) // не работает корректно, звук мгновенно возращается на нулевой питч а НУЖНО // несколько источников, overlap, fade по громкости, циклическая октава
    {
        if (!pitchStacks.TryGetValue(sound, out var stack))
        {
            stack = new PitchStackData();
            pitchStacks[sound] = stack;
        }

        if (time - stack.LastPlayTime > settings.pitchResetDelay) stack.Pitch = 1f;
        stack.LastPlayTime = time;

        float pitch = stack.Pitch;
        stack.Pitch += settings.pitchIncrement;

        if (stack.Pitch > settings.maxPitch)
        {
            stack.Pitch = UnityEngine.Random.Range(settings.minPitchAfterReset, settings.maxRandomizedReset);
        }

        return pitch;
    }
}

public struct AudioPlayResult
{
    public AudioClip Clip;
    public float Pitch;
    public bool IsLoud;
}


public class PitchStackData
{
    public float Pitch = 1f;
    public float LastPlayTime;
}

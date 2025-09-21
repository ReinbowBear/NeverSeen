using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;
using Zenject;
using DG.Tweening;

public class AudioService : IInitializable
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSettingsSO settings;

    private Dictionary<SoundType, AudioSource> sources = new();
    private Dictionary<AudioSO, PitchStackData> pitchStacks = new();
    private ObjectPool pool = new ObjectPool("Sound", true);

    private class PitchStackData
    {
        public float Pitch = 1f;
        public float LastPlayTime;
    }

    public void Initialize()
    {
        CreateAudioSources();
    }


    public void Play(AudioSO data)
    {
        var source = sources[data.Type];
        source.clip = data.Sound;

        if (data.Type != SoundType.Music)
        {
            source.pitch = Random.Range(0.9f, 1.1f);
        }

        if (data.IsStacked)
        {
            PlayStackedSound(data);
        }
        else
        {
            source.PlayOneShot(data.Sound);
        }

        if (data.IsLoud)
        {
            Tween.MutingVolume(audioMixer, "MusicVolume", 0.4f, 1f).SetLink(source.gameObject);
        }
    }


    private void PlayStackedSound(AudioSO data) // тон шепарда
    {
        if (!pitchStacks.TryGetValue(data, out var stack))
        {
            stack = new PitchStackData();
            pitchStacks[data] = stack;
        }

        float timeSinceLastPlay = Time.time - stack.LastPlayTime;

        if (timeSinceLastPlay > settings.pitchResetDelay)
        {
            stack.Pitch = 1f;
        }

        stack.LastPlayTime = Time.time;

        var source = sources[data.Type];
        source.clip = data.Sound;
        source.pitch = stack.Pitch;
        source.Play();

        stack.Pitch += settings.pitchIncrement;

        if (stack.Pitch > settings.maxPitch)
        {
            stack.Pitch = Random.Range(settings.minPitchAfterReset, settings.maxRandomizedReset);
        }
    }


    private void CreateAudioSources()
    {
        var cam = Camera.main;
        sources.Add(SoundType.UI, cam.gameObject.AddComponent<AudioSource>());
        sources.Add(SoundType.SFX, cam.gameObject.AddComponent<AudioSource>());
        sources.Add(SoundType.Music, cam.gameObject.AddComponent<AudioSource>());
    }
}

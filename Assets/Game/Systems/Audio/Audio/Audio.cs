using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

public class Audio
{
    private AudioMixer Mixer;
    private RandomService random;

    private Dictionary<SoundType, AudioSource> sources = new();
    private Dictionary<SoundSO, PitchStackData> pitchStacks = new();

    public Audio(RandomService random, Factory factory)
    {
        this.random = random;
        Mixer = factory.Load<AudioMixer>("AudioMixer");
    }


    public void Play(SoundSO sound)
    {
        var source = GetAudioSources(sound.soundType);

        source.clip = sound.Clip;
        source.pitch = GetPitch(sound, Time.time);
        source.Play();

        if (sound.IsLoud)
        {
            Tween.MutingVolume(Mixer, "MusicVolume", 0.4f, 1f).SetLink(source.gameObject);
        }
    }


    private AudioSource GetAudioSources(SoundType soundType)
    {
        if(sources.TryGetValue(soundType, out var source)) return source;
        
        var cameraObject = Camera.main.gameObject;
        var newSource = cameraObject.AddComponent<AudioSource>();

        sources.Add(soundType, newSource);
        return newSource;
    }


    private float GetPitch(SoundSO sound, float currentTime)
    {
        float pitch = 1f;

        if (sound.IsShepard) pitch = GetStackedPitch(sound, currentTime);
        else if (sound.IsPitch) pitch = random.NextFloat(0.9f, 1.1f);
        return pitch;
    }

    private float GetStackedPitch(SoundSO sound, float time)
    {
        if (!pitchStacks.TryGetValue(sound, out var data))
        {
            data = new PitchStackData
            {
                Pitch = 1f,
                LastPlayTime = time
            };
        }
    
        float speed = 0.2f; // скорость "вращения" (можешь вынести в SO)
        float phase = time * speed % 1f; // создаём фазу (0..1)
    
        float envelope = Mathf.Sin(phase * Mathf.PI); // логика Shepard-подобного цикла // синус даёт плавное затухание краёв
    
        float minPitch = 0.5f; // диапазон октав
        float maxPitch = 2f;

        float pitch = Mathf.Lerp(minPitch, maxPitch, phase); // логарифмическое движение (октавы ощущаются правильно)
        pitch *= Mathf.Lerp(0.7f, 1.3f, envelope); // маска громкости/восприятия
    
        data.Pitch = pitch;
        data.LastPlayTime = time;
        pitchStacks[sound] = data;
    
        return pitch;
    }
}

public struct PitchStackData
{
    public float Phase;
    public float Pitch;
    public float LastPlayTime;
}

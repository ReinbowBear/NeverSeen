using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

public class Audio2
{
    private AudioMixer Mixer;
    private RandomService random;

    private Dictionary<SoundType, List<AudioSource>> sources = new();
    private Dictionary<SoundSO, PitchStackData> pitchStacks = new();

    public Audio2(RandomService random, Factory factory)
    {
        this.random = random;
        Mixer = factory.Load<AudioMixer>("AudioMixer");
    }


    public void Play(SoundSO sound)
    {
        if (sound.IsShepard)
        {
            PlayStacked(sound);
            return;
        }

        PlayNormal(sound);
    }


    private void PlayNormal(SoundSO sound)
    {
        var source = GetAudioSource(sound.soundType);

        source.clip = sound.Clip;
        source.pitch = sound.IsPitch ? random.NextFloat(0.9f, 1.1f) : 1f;

        source.Play();

        if (sound.IsLoud)
        {
            Tween.MutingVolume(Mixer, "MusicVolume", 0.4f, 1f) .SetLink(source.gameObject);
        }
    }



    private void PlayStacked(SoundSO sound)
    {
        if (!pitchStacks.TryGetValue(sound, out var data))
        {
            data = new PitchStackData();
        }

        float time = Time.time;

        if (time - data.LastPlayTime > 1f)
        {
            data.Phase = 0f;
        }

        data.LastPlayTime = time;
        data.Phase += 0.12f;

        if (data.Phase >= 1f) data.Phase -= 1f;

        pitchStacks[sound] = data;

        PlayStackedVoice(sound, data.Phase);
        PlayStackedVoice(sound, (data.Phase + 0.5f) % 1f);
    }



    private void PlayStackedVoice(SoundSO sound, float phase)
    {
        var source = GetAudioSource(sound.soundType);

        source.clip = sound.Clip;
        source.pitch = Mathf.Pow(2f, phase);
        source.volume = Mathf.Sin(phase * Mathf.PI) * 0.9f;

        source.Play();
    }


    private AudioSource GetAudioSource(SoundType soundType)
    {
        if (!sources.TryGetValue(soundType, out var list))
        {
            list = new List<AudioSource>();
            sources[soundType] = list;
        }

        foreach (var sours in list)
        {
            if (!sours.isPlaying) return sours;
        }

        var newSource = Camera.main.gameObject.AddComponent<AudioSource>();
        list.Add(newSource);

        return newSource;
    }
}

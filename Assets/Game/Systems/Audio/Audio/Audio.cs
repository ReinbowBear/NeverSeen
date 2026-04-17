using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

public class Audio : ISystem
{
    private AudioMixer Mixer;
    private AudioSettings Settings = new();

    private Dictionary<SoundType, AudioSource> sources = new();
    private Dictionary<SoundSO, PitchStackData> pitchStacks = new();

    public async Task AsyncInit(Factory factory)
    {
        Mixer = await factory.LoadAsync<AudioMixer>("AudioMixer");
    }

    public void SetSubs(SystemSubs subs)
    {
        //subs.AddListener();
    }

    public void UpdateSystem(World world)
    {
        //foreach (var (entity, evt) in world.Query<OnNavigate>())
        //foreach (var (entity, evt) in world.Query<OnButtonInvoke>())
        //foreach (var (entity, evt) in world.Query<OnPanelOpen>())
        //foreach (var (entity, evt) in world.Query<OnPanelClose>())
    }


    private void ExecuteSound(SoundSO sound)
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

        if (sound.IsStacked)
        {
            pitch = GetStackedPitch(sound, currentTime);
        }
        else if (sound.IsPitch)
        {
            pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        }
        return pitch;
    }

    private float GetStackedPitch(SoundSO sound, float time) // не работает корректно, звук мгновенно возращается на нулевой питч а НУЖНО // несколько источников, overlap, fade по громкости, циклическая октава
    {
        return 1;
    }
}

public struct PitchStackData
{
    public float Pitch;
    public float LastPlayTime;
}

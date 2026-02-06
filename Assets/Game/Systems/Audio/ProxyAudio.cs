using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

public class ProxyAudio : BaseProxy
{
    public GameObject SoundPref;
    public AudioMixer Mixer;
    public AudioSettingsSO Settings;

    private Dictionary<SoundType, AudioSource> sources = new();
    private Audio Audio;
    private ObjectPool pool;

    public override void Init()
    {
        pool = new(SoundPref);
        Audio = new(Settings);
    }

    public override void Enter()
    {
        eventWorld.AddListener<Sound>(ExecuteSound, Events.UIEvents.OnNavigate);
        eventWorld.AddListener<Sound>(ExecuteSound, Events.UIEvents.OnButtonInvoke);
        eventWorld.AddListener<Sound>(ExecuteSound, Events.UIEvents.OnPanelOpen);
        eventWorld.AddListener<Sound>(ExecuteSound, Events.UIEvents.OnPanelClose);
    }


    private void ExecuteSound(Sound sound)
    {
        var soundSO = sound.soundSO;
        var source = GetAudioSources(soundSO.soundType);
        var result = Audio.GetAudioPlayResult(soundSO, Time.time);

        source.clip = result.Clip;
        source.pitch = result.Pitch;
        source.Play();

        if (result.IsLoud)
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


    private IEnumerator ReturnAfterPlay(AudioSource source)
    {
        yield return new WaitForSeconds(source.clip.length);
        source.Stop();
        pool.Return(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

public class AudioService : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSO backgroundMusic;
    [Space]
    private Dictionary<SoundType, AudioSource> sources = new();
    private Dictionary<SoundData, PitchStackData> pitchStacks = new();
    [SerializeField] private float pitchIncrement = 0.15f;
    [SerializeField] private float pitchResetDelay = 1.5f;
    [SerializeField] private float maxPitch = 2.5f;
    [SerializeField] private float minPitchAfterReset = 1f;
    [SerializeField] private float maxRandomizedReset = 1.2f;
    [Space]
    [SerializeField] private float musicPauseVolume = -80f;
    [SerializeField] private float musicNormalVolume = 0f;
    [SerializeField] private float musicFadeOutTime = 1.5f;
    [SerializeField] private float musicFadeInTime = 0.5f;

    private class PitchStackData
    {
        public float Pitch = 1f;
        public float LastPlayTime;
        public Coroutine Coroutine;
    }

    void Awake()
    {
        var cam = Camera.main;
        sources.Add(SoundType.UI, cam.gameObject.AddComponent<AudioSource>());
        sources.Add(SoundType.SFX, cam.gameObject.AddComponent<AudioSource>());
        //sources.Add(SoundType.Ambience, cam.gameObject.AddComponent<AudioSource>());
        sources.Add(SoundType.Music, cam.gameObject.AddComponent<AudioSource>());
        //sources.Add(SoundType.Dialogues, cam.gameObject.AddComponent<AudioSource>());
    }

    void Start()
    {
        var randomInt = Random.Range(0, backgroundMusic.AudioClips.Length);
        var musicData = backgroundMusic.AudioClips[randomInt];
        Play(musicData);
    }

    [EventHandler(Priority.low)]
    public void Play(SoundData data)
    {
        var source = sources[data.Type];
        source.clip = data.Sound;

        if (data.Type != SoundType.Music)
        {
            source.pitch = Random.Range(0.9f, 1.1f);
        }

        if (data.IsLoud)
        {
            CoroutineManager.Start(DuckVolume("MusicVolume", 0.2f, 1f, 0.5f), this);
        }

        if (data.IsPitchStacked)
        {
            PlayStackedSound(data);
        }
        else
        {
            //source.Play();
            source.PlayOneShot(data.Sound);
        }
    }

    private IEnumerator DuckVolume(string mixerParam, float targetVolume, float duration, float restoreTime) // затухание звука и его востановление позже
    {
        float originalVolume;
        audioMixer.GetFloat(mixerParam, out originalVolume);
        originalVolume = Mathf.Pow(10, originalVolume / 20f); // переводим децибелы в линейное значение как то что то?

        float currentTime = 0f;
        while (currentTime < duration)
        {
            float vol = Mathf.Lerp(originalVolume, targetVolume, currentTime / duration);
            audioMixer.SetFloat(mixerParam, Mathf.Log10(vol) * 20f);

            currentTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(restoreTime);

        currentTime = 0f;
        while (currentTime < duration)
        {
            float vol = Mathf.Lerp(targetVolume, originalVolume, currentTime / duration);
            audioMixer.SetFloat(mixerParam, Mathf.Log10(vol) * 20f);

            currentTime += Time.deltaTime;
            yield return null;
        }

        audioMixer.SetFloat(mixerParam, Mathf.Log10(originalVolume) * 20f);
    }


    private IEnumerator TemporarilyDuckMusic(AudioMixer mixer, string parameter = "MusicVolume") // не помню что делает
    {
        yield return Tween.FadeMixerVolume(mixer, parameter, 0.2f, 0.3f).WaitForCompletion();
        yield return new WaitForSeconds(0.5f);
        yield return Tween.FadeMixerVolume(mixer, parameter, 1f, 0.5f).WaitForCompletion();
    }


    public void PlayStackedSound(SoundData data)
    {
        if (!pitchStacks.TryGetValue(data, out var stack))
        {
            stack = new PitchStackData();
            pitchStacks[data] = stack;
        }

        stack.LastPlayTime = Time.time;

        if (stack.Coroutine == null)
        {
            stack.Coroutine = StartCoroutine(ResetPitchCoroutine(stack));
        }

        var source = sources[data.Type];
        source.clip = data.Sound;
        source.pitch = stack.Pitch;
        source.Play();

        stack.Pitch += pitchIncrement;
        if (stack.Pitch > maxPitch)
        {
            stack.Pitch = Random.Range(minPitchAfterReset, maxRandomizedReset);
        }
    }

    private IEnumerator ResetPitchCoroutine(PitchStackData stack)
    {
        while (Time.time - stack.LastPlayTime < pitchResetDelay)
        {
            yield return null;
        }
        stack.Pitch = 1f;
        stack.Coroutine = null;
    }


    public void PauseMusic()
    {
        CoroutineManager.Start(FadeMusicVolume(AudioMixerParams.MusicVolume, musicPauseVolume, musicFadeOutTime), this);
    }
    
    public void ResumeMusic()
    {
        CoroutineManager.Start(FadeMusicVolume(AudioMixerParams.MusicVolume, musicNormalVolume, musicFadeInTime), this);
    }

    private IEnumerator FadeMusicVolume(string param, float targetDb, float duration)
    {
        audioMixer.GetFloat(param, out float currentDb);
        float currentTime = 0f;

        while (currentTime < duration)
        {
            float t = currentTime / duration;
            float value = Mathf.Lerp(currentDb, targetDb, t);
            audioMixer.SetFloat(param, value);

            currentTime += Time.deltaTime;
            yield return null;
        }

        audioMixer.SetFloat(param, targetDb);
    }
}


public enum SoundType
{
    UI, SFX, Ambience, Music, Dialogues
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

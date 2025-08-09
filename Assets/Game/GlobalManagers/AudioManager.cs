using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;


    [Header("Audio Source")]
    [SerializeField] private AudioSource MainSource;

    [Header("Audio Clips")]
    [SerializeField] private SoundData[] backgroundMusic;
    [SerializeField] private SoundData[] soundEffects;
    private Dictionary<string, AudioClip> soundDictionary;

    [Header("Setting")]
    [SerializeField] private float volume;
    [SerializeField] private float minPitch;
    [SerializeField] private float maxPitch;
    [SerializeField] float spatialBlend;

    private readonly Queue<AudioSource> pool = new();

    void Awake()
    {
        Instance = this;
        InitSoundEffects();
    }

    private void InitSoundEffects()
    {
        soundDictionary = new();
        foreach (var sound in soundEffects)
        {
            soundDictionary.Add(sound.Name, sound.Clip);
        }
    }

    #region func
    public void PlayMusic(AudioClip clip, bool loop = false)
    {
        MainSource.clip = clip;
        MainSource.loop = loop;
        MainSource.Play();
    }

    public void PlayEffect(string name)
    {
        if (!soundDictionary.TryGetValue(name, out AudioClip clip))
        {
            Debug.LogWarning($"SFX '{name}' not found!");
            return;
        }

        MainSource.pitch = Random.Range(0.9f, 1.1f);
        MainSource.PlayOneShot(clip);
    }

    public void PlaySFX(AudioClip clip, Transform owner)
    {
        AudioSource source = GetSource();
        source.transform.SetParent(owner);

        float pitch = Random.Range(minPitch, maxPitch);
        source.pitch = pitch;
        source.volume = volume;
        source.spatialBlend = spatialBlend;

        source.PlayOneShot(clip);
        StartCoroutine(ReturnAfterPlayback(source, clip.length / pitch));
    }
    #endregion

    #region Pool
    private AudioSource CreateNewSource()
    {
        GameObject obj = Instantiate(new GameObject(), transform);
        obj.SetActive(false);

        AudioSource component = obj.AddComponent<AudioSource>();
        pool.Enqueue(component);
        return component;
    }

    private AudioSource GetSource()
    {
        if (pool.Count == 0)
        {
            CreateNewSource();
        }

        AudioSource source = pool.Dequeue();
        source.gameObject.SetActive(true);
        return source;
    }

    private IEnumerator ReturnAfterPlayback(AudioSource source, float delay)
    {
        yield return new WaitForSeconds(delay);
        ReturnSource(source);
    }

    private void ReturnSource(AudioSource source)
    {
        source.Stop();
        source.clip = null;

        source.gameObject.SetActive(false);
        pool.Enqueue(source);
    }
    #endregion

    #region settings
    public void SetVolume(float newVolume)
    {
        volume = newVolume;
        MainSource.volume = Mathf.Clamp01(volume);
    }
    #endregion
}

[System.Serializable]
public struct SoundData
{
    public string Name;
    public AudioClip Clip;
}

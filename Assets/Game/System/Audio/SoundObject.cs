using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class SoundObject : MonoBehaviour
{
    private AudioSource source;
    private AudioMixer mixer;
    private ObjectPool pool;

    public void Init(AudioMixer mixer, ObjectPool pool)
    {
        this.mixer = mixer;
        this.pool = pool;
    }

    public void Play(AudioClip clip, float volume = 1f, float pitch = 1f)
    {
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.Play();

        StartCoroutine(ReturnAfterPlay(clip.length));
    }

    private IEnumerator ReturnAfterPlay(float delay)
    {
        yield return new WaitForSeconds(delay);
        source.Stop();
        source.clip = null;
        pool.Return(gameObject);
    }
}

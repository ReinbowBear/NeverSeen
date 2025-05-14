using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public static Sound instance;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] musics;

    private List<AudioClip> sceneSounds = new List<AudioClip>();

    void Start()
    {
        instance = this;
        StartCoroutine(PlayMusic());
    }


    private IEnumerator PlayMusic()
    {
        while (this.enabled)
        {
            yield return new WaitForSeconds(1);

            int randomID = Random.Range(0, musics.Length);
            audioSource.PlayOneShot(musics[randomID]);

            yield return new WaitForSeconds (musics[randomID].length);
        }
    }


    public void PlayRandom(AudioClip[] sounds, AudioSource soundPoint = null)
    {
        int randomID = Random.Range(0, sounds.Length);
        Play(sounds[randomID], soundPoint);
    }

    public void Play(AudioClip sound, AudioSource soundPoint = null)
    {
        if (soundPoint = null) {soundPoint = audioSource; }

        soundPoint.pitch = Random.Range(0.9f, 1.1f);
        soundPoint.PlayOneShot(sound);
    }
}

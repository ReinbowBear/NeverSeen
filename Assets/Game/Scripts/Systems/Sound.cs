using System;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public static Sound data;
    private System.Random random;
    [SerializeField] private AudioSource audioSource;
    [Space]
    public AudioClip[] systemSounds;
    public AudioClip[] buttonSounds;
    public AudioClip[] sceneSounds;

    Sound()
    {
        data = this;
        random = new System.Random(DateTime.Now.Millisecond);
    }


    public void PlaySound(byte index, AudioSource soundPoint = null)
    {
        if (soundPoint == null)
        {
            soundPoint = audioSource;
        }

        audioSource.PlayOneShot(systemSounds[index]);
        Debug.Log("звуки ещё не готовы");
    }

    public void PlayRandom()
    {
        int randomID = random.Next(0, systemSounds.Length);

        audioSource.PlayOneShot(systemSounds[randomID]);
        Debug.Log("звуки ещё не готовы");
    }
}

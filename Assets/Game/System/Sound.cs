using System;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public static Sound instance;
    private System.Random random;
    [SerializeField] private AudioSource audioSource;
    [Space]
    public AudioClip[] systemSounds;
    public AudioClip[] buttonSounds;
    public AudioClip[] sceneSounds;

    //private Dictionary<string, float> lastPlayTime = new Dictionary<string, float>();
    //private List<AudioSource> audioSourcePool = new List<AudioSource>();
    //private int initialPoolSize = 10;

    void Awake()
    {
        instance = this;
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

public enum AudioType
{
    sfx,
    music,
    ambient
}

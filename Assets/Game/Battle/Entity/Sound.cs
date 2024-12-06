using System;
using UnityEngine;

public class Sound : MonoBehaviour
{
    private System.Random random;

    [SerializeField] private AudioSource audioSource;
    [Space]
    [SerializeField] private AudioClip[] sounds;

    void Awake()
    {
        random = new System.Random(DateTime.Now.Millisecond);
    }

    public void makeSound(byte index)
    {
        audioSource.PlayOneShot(sounds[index]);
    }

    public void makeRandomSound()
    {
        int randomID = random.Next(0, sounds.Length);

        audioSource.PlayOneShot(sounds[randomID]);
    }
}

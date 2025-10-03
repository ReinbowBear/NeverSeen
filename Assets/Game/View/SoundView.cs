using UnityEngine;
using Zenject;

public class SoundView : MonoBehaviour
{
    [SerializeField] private SoundSO[] Sounds;

    [Inject] private AudioService audioService;

    public void playSound(int soundIndex)
    {
        audioService.Play(Sounds[soundIndex]);
    }

    public void playRandom()
    {
        var index = Random.Range(0, Sounds.Length);
        audioService.Play(Sounds[index]);
    }
}

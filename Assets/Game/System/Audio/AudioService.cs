using UnityEngine;
using Zenject;

public class AudioService : MonoBehaviour
{
    [SerializeField] private AudioSource MainSource;
    [SerializeField] private AudioSO backgroundMusic;

    private Factory objectFactory;

    [Inject]
    public void Construct(Factory objectFactory) // обджект пул бы ему по идеи
    {
        this.objectFactory = objectFactory;
    }

    void Start()
    {
        var randomInt = Random.Range(0, backgroundMusic.AudioClips.Length);
        var music = backgroundMusic.AudioClips[randomInt].Sound;
        PlayMusic(music);
    }


    public void PlayMusic(AudioClip clip, bool loop = false)
    {
        MainSource.volume = 1;
        MainSource.clip = clip;
        MainSource.loop = loop;
        MainSource.Play();
    }
    
    [EventHandler(Priority.low)]
    public void PlayEffect(OnSound soundEvent)
    {
        PlayEffect(soundEvent.SoundData.Sound);
    }

    public void PlayEffect(AudioClip clip)
    {
        var obj = objectFactory.Create("Sound");

        var script = obj.GetComponent<SoundObject>();
        var source = script.Source;

        source.pitch = Random.Range(0.9f, 1.1f);
        source.volume = 1;
        source.PlayOneShot(clip);

        //script.StartCoroutine(script.ReturnAfterPlayback(source.clip.length / source.pitch));
    }

    public void PlayEffect(AudioClip clip, Vector3 pos)
    {
        var obj = objectFactory.Create("Sound");
        obj.transform.position = pos;

        var script = obj.GetComponent<SoundObject>();
        var source = script.Source;

        source.pitch = Random.Range(0.9f, 1.1f);
        source.volume = 1;
        source.PlayOneShot(clip);

        script.StartCoroutine(script.ReturnAfterPlayback(source.clip.length / source.pitch));
    }
}

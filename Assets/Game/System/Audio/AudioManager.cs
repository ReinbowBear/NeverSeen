using UnityEngine;

public class AudioManager
{
    private AudioSource MainSource;
    private AudioSO backgroundMusic;

    private float volume;
    private float minPitch;
    private float maxPitch;
    float spatialBlend;

    public AudioManager(AudioSource newMainSource)
    {
        MainSource = newMainSource;
    }

    public void PlayMusic(AudioClip clip, bool loop = false)
    {
        MainSource.volume = volume;
        MainSource.clip = clip;
        MainSource.loop = loop;
        MainSource.Play();
    }


    //public void OnWeaponHit(OnDoSound soundEvent)
    //{
    //    PlayEffect(soundEvent.sound, soundEvent.pos);
    //}

    public void PlayEffect(AudioClip clip, Vector3 pos)
    {
        var obj = ObjectPool.Get("SoundObject");

        var script = obj.GetComponent<SoundObject>();
        var source = script.Source;

        source.pitch = Random.Range(minPitch, maxPitch);
        source.volume = volume;
        source.spatialBlend = spatialBlend;
        source.PlayOneShot(clip);

        script.StartCoroutine(script.ReturnAfterPlayback(source.clip.length / source.pitch));
    }
}

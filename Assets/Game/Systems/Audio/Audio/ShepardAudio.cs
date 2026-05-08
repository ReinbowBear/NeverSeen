using UnityEngine;

public class ShepardAudio
{
    [Header("Setup")]
    public AudioClip clip;
    public int voicesCount = 5;

    [Header("Tuning")]
    public float minPitch = 0.5f;
    public float maxPitch = 2.0f;
    public float speed = 0.15f;

    private ShepardVoice[] voices;

    public ShepardAudio()
    {
        voices = new ShepardVoice[voicesCount];

        for (int i = 0; i < voicesCount; i++)
        {
            var obj = new GameObject("ShepardVoice_" + i);

            var source = obj.AddComponent<AudioSource>();
            var phase = (float)i / voicesCount;

            voices[i] = new ShepardVoice(source, phase);
        }
    }


    void Play(AudioClip clip)
    {
        float delta = Time.deltaTime;

        for (int i = 0; i < voices.Length; i++)
        {
            var voise = voices[i];
            float phase = voise.Phase;

            phase += delta * speed;

            if (phase > 1f) phase -= 1f;

            float pitch = Mathf.Lerp(minPitch, maxPitch, phase);
            float volume = Mathf.Sin(phase * Mathf.PI);

            voise.Source.pitch = pitch;
            voise.Source.volume = volume * 0.6f;
        }
    }
}

public struct ShepardVoice
{
    public AudioSource Source;
    public float Phase;

    public ShepardVoice(AudioSource source, float phase)
    {
        Source = source;
        Phase = phase;
    }
}

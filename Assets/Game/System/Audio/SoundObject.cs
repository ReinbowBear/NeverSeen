using System.Collections;
using UnityEngine;

public class SoundObject : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    public AudioSource Source => source;

    public IEnumerator ReturnAfterPlayback(float delay)
    {
        yield return new WaitForSeconds(delay);

        Source.Stop();
        Source.clip = null;

        Destroy(gameObject);
        //ObjectFactory.Return(gameObject); // ранее тут был пул объектов
    }
}

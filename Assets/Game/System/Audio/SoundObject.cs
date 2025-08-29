using System.Collections;
using UnityEngine;

public class SoundObject : MonoBehaviour
{
    [SerializeField] public AudioSource Source { get; private set; }

    public IEnumerator ReturnAfterPlayback(float delay)
    {
        yield return new WaitForSeconds(delay);

        Source.Stop();
        Source.clip = null;

        ObjectPool.Return(gameObject);
    }
}

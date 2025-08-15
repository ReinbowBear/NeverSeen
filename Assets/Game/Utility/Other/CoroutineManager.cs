using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoroutineManager
{
    private static readonly Dictionary<MonoBehaviour, Dictionary<IEnumerator, Coroutine>> runningCoroutines = new();

    static CoroutineManager()
    {
        EventBus.Subscribe<OnSceneRelease>(Clear);
    }


    public static void Start(IEnumerator coroutine, MonoBehaviour key)
    {
        if (!runningCoroutines.TryGetValue(key, out var keyDictionary))
        {
            keyDictionary = new Dictionary<IEnumerator, Coroutine>();
            runningCoroutines[key] = keyDictionary;
        }

        if (keyDictionary.TryGetValue(coroutine, out Coroutine existing))
        {
            key.StopCoroutine(existing);
            keyDictionary.Remove(coroutine);
        }

        Coroutine wrapped = key.StartCoroutine(WrappedCoroutine(coroutine, key));
        keyDictionary[coroutine] = wrapped;
    }

    public static void Stop(IEnumerator coroutine, MonoBehaviour key)
    {
        if (runningCoroutines.TryGetValue(key, out var keyDictionary))
        {
            if (keyDictionary.TryGetValue(coroutine, out Coroutine running))
            {
                key.StopCoroutine(running);
                keyDictionary.Remove(coroutine);

                if (keyDictionary.Count == 0)
                {
                    runningCoroutines.Remove(key);
                }
            }
        }
    }


    public static IEnumerator Wait(IEnumerator coroutine, MonoBehaviour key)
    {
        if (runningCoroutines.TryGetValue(key, out var keyDictionary) && keyDictionary.TryGetValue(coroutine, out Coroutine running))
        {
            yield return running;
        }
    }


    private static IEnumerator WrappedCoroutine(IEnumerator coroutine, MonoBehaviour key)
    {
        yield return coroutine;

        if (runningCoroutines.TryGetValue(key, out var keyDictionary))
        {
            keyDictionary.Remove(coroutine);

            if (keyDictionary.Count == 0)
            {
                runningCoroutines.Remove(key);
            }
        }
    }


    public static void Clear(OnSceneRelease _ = null)
    {
        runningCoroutines.Clear();
    }
}

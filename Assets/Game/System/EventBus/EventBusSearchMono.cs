using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventBusSearchMono : MonoBehaviour
{
    private readonly List<MonoBehaviour> monoBehaviours = new();
    private readonly List<MonoBehaviour> tempBehaviours = new();

    void Awake()
    {
        FindAllMono();
        SubMono(true);
    }

    private void FindAllMono()
    {
        monoBehaviours.Clear();

        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject root in rootObjects)
        {
            TraverseHierarchy(root.transform);
        }
    }

    private void TraverseHierarchy(Transform current)
    {
        tempBehaviours.Clear();

        current.GetComponents(tempBehaviours); // есть смысл проверять по интерфейсу что бы снизить нагрузку
        monoBehaviours.AddRange(tempBehaviours);

        for (int i = 0; i < current.childCount; i++)
        {
            TraverseHierarchy(current.GetChild(i));
        }
    }

    private void SubMono(bool isSubscribe)
    {
        foreach (MonoBehaviour mono in monoBehaviours)
        {
            EventReflection.SubscribeClass(mono, isSubscribe);
        }
    }


    void OnDestroy()
    {
        SubMono(false);
    }
}

using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private GlobalMap globalMap;

    public void StartLayer(MyEvent.OnEntryMap _)
    {

    }


    public void NewLayer()
    {

    }


    void OnEnable()
    {
        EventBus.Add<MyEvent.OnEntryMap>(StartLayer);
    }

    void OnDisable()
    {
        EventBus.Remove<MyEvent.OnEntryMap>(StartLayer);
    }
}

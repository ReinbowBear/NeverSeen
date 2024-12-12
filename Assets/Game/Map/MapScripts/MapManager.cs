using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private GlobalMap globalMap;

    public void StartLayer()
    {

    }


    public void NewLayer()
    {

    }


    void OnEnable()
    {
        EventBus.Add<MyEvent.OnEntryMap>(StartLayer);
    }
}

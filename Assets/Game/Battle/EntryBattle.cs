using System.IO;
using UnityEngine;

public class EntryBattle : MonoBehaviour
{
    void Start()
    {
        if (File.Exists(SaveSystem.GetFileName()))
        {
            SaveSystem.onLoad.Invoke();
            EventBus.Invoke<MyEvent.OnEntryBattle>();
        }
        else
        {
            Debug.Log("сохранения нету!");
        }
    }
}

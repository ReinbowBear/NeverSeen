using System.IO;
using UnityEngine;

public class EntryBattle : MonoBehaviour
{
    void Start()
    {
        if (File.Exists(SaveSystem.GetFileName()))
        {
            EventBus.Invoke<MyEvent.OnLoad>();
        }

        EventBus.Invoke<MyEvent.OnEntryBattle>();
    }
}

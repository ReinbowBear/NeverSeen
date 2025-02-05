using System.IO;
using UnityEngine;

public class EntryMap : MonoBehaviour
{
    void Start()
    {
        CheckSave();
    }


    private void CheckSave()
    {
        if (File.Exists(SaveSystem.GetFileName()))
        {
            EventBus.Invoke<MyEvent.OnLoad>();
        }
        else
        {
            EventBus.Invoke<MyEvent.OnEntryMap>(null);
            EventBus.Invoke<MyEvent.OnSave>();
            
            SaveSystem.SaveFile();
        }
    }
}

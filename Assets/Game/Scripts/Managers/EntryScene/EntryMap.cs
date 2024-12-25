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
            SaveSystem.onLoad.Invoke();
            EventBus.Invoke<MyEvent.OnEntryMap>(null);
        }
        else
        {
            EventBus.Invoke<MyEvent.OnEntryMap>(null);

            SaveSystem.onSave.Invoke();
            SaveSystem.SaveFile();
        }
    }
}

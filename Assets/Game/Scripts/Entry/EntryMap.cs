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
        if (File.Exists(SaveSystem.filePath))
        {
            EventBus.Invoke<OnLoad>();
        }
        else
        {
            EventBus.Invoke<OnEntryMap>(null);

            EventBus.Invoke<OnSave>();
            SaveSystem.SaveFile();
        }
    }
}

using System.IO;
using UnityEngine;

public class EntryMeta : MonoBehaviour
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
            EventBus.Invoke<OnEntryScene>();

            EventBus.Invoke<OnSave>();
            SaveSystem.SaveFile();
        }
    }
}

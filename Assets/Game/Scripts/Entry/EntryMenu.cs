using System.IO;
using UnityEngine;

public class EntryMenu : MonoBehaviour
{
    void Start()
    {
        CheckSave();
    }


    private void CheckSave()
    {
        if (File.Exists(SaveSystem.filePath))
        {
            SaveSystem.LoadFile();
            EventBus.Invoke<OnLoad>();
        }
    }
}

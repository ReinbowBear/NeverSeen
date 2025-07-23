using System.IO;
using UnityEngine;

public class EntryBattle : MonoBehaviour
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

        EventBus.Invoke<OnEntryBattle>();
    }
}

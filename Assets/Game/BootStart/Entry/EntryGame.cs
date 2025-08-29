using System.IO;
using UnityEngine;

public class EntryGame : MonoBehaviour
{
    private static bool isGameInit;

    void Awake()
    {
        if (isGameInit) return;

        CheckSave();
        isGameInit = true;
    }


    private void CheckSave()
    {
        if (File.Exists(SaveLoad.filePath))
        {
            SaveLoad.LoadFile();
        }
        else
        {
            SaveLoad.SaveFile();
        }
    }


    void OnDestroy()
    {
        EventBus.Invoke<OnSceneRelease>();
    }
}

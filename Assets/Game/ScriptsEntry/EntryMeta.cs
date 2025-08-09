using System.IO;
using UnityEngine;

public class EntryMeta : MonoBehaviour
{
    void Start()
    {
        CheckSave();
        //MyInput.Instance.SetInputMode(MyInput.Instance.GameInput.);
    }


    private void CheckSave()
    {
        if (File.Exists(SaveLoad.filePath))
        {
            EventBus.Invoke<OnLoad>();
        }
        else
        {
            EventBus.Invoke<OnEntryScene>();

            EventBus.Invoke<OnSave>();
            SaveLoad.SaveFile();
        }
    }
}

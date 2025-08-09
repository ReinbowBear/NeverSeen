using System.IO;
using UnityEngine;

public class EntryMenu : MonoBehaviour
{
    void Start()
    {
        CheckSave();
        Input.Instance.SetInputMode(Input.Instance.GameInput.UI);
    }


    private void CheckSave()
    {
        if (File.Exists(SaveLoad.filePath))
        {
            SaveLoad.LoadFile();
            EventBus.Invoke<OnLoad>();
        }
    }
}

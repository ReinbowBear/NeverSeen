using System.IO;
using UnityEngine;

public class EntryPlay : MonoBehaviour
{
    void Start()
    {
        CheckSave();
        Input.Instance.SetInputMode(Input.Instance.GameInput.Gameplay);
    }

    private void CheckSave()
    {
        if (File.Exists(SaveLoad.filePath))
        {
            EventBus.Invoke<OnLoad>();
        }

        EventBus.Invoke<OnEntryScene>();
    }
}

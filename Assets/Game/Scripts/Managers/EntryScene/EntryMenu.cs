using System.IO;
using UnityEngine;

public class EntryMenu : MonoBehaviour
{
    [SerializeField] private MenuKeyboard menuKeyboard;

    void Start()
    {
        CheckSave();
    }


    private void CheckSave()
    {
        if (File.Exists(SaveSystem.GetFileName()))
        {
            menuKeyboard.buttons[0].gameObject.SetActive(true);
            menuKeyboard.MoveTo(0);

            SaveSystem.LoadFile();
        }
    }
}

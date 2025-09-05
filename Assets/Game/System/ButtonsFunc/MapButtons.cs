using UnityEngine;
using UnityEngine.SceneManagement;

public class MapButtons : MonoBehaviour
{
    public void GoBattle()
    {
        EventBus.Invoke<OnSave>();

        SceneManager.LoadScene(2);
    }

    public void ExitToMenu()
    {
        EventBus.Invoke<OnSave>();
        //SaveLoad.SaveFile();

        SceneManager.LoadScene(0);
    }
}

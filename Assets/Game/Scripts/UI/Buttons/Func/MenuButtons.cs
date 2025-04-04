using UnityEngine;

public class MenuButtons : MonoBehaviour
{    
    public void ContinueGame()
    {
        Scene.Continue();
    }

    public void NewGame()
    {
        SaveSystem.DeleteSave();
        Scene.Load(1);
    }

    public void ExitGame()
    {
        Debug.Log("Отсюда нет выхода.. x_x");
        Application.Quit();
    }
}

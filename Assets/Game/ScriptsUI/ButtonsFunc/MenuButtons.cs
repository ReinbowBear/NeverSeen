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
        EventBus.Invoke<OnSave>(); // нужен что бы условно сохранить выбранного персонажа для нового забега, но пока это может быть не нужно

        Scene.Load(1);
    }

    public void ExitGame()
    {
        Debug.Log("Отсюда нет выхода.. x_x");
        Application.Quit();
    }
}

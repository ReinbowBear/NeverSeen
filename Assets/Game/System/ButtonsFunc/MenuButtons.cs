using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class MenuButtons : MonoBehaviour
{
    private GameData gameState;

    [Inject]
    public void Construct(GameData gameState)
    {
        this.gameState = gameState;
    }


    public void ContinueGame()
    {
        SceneManager.LoadScene(gameState.General.SceneIndex);
    }

    public void NewGame()
    {
        //SaveLoad.DeleteSave();
        EventBus.Invoke<OnSave>(); // нужен что бы условно сохранить выбранного персонажа для нового забега, но пока это может быть не нужно

        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Debug.Log("Отсюда нет выхода.. x_x");
        Application.Quit();
    }
}

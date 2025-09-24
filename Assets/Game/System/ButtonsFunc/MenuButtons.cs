using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class MenuButtons : MonoBehaviour
{
    private GeneralData generalData;

    [Inject]
    public void Construct(GeneralData generalData)
    {
        this.generalData = generalData;
    }


    public void ContinueGame()
    {
        SceneManager.LoadScene(generalData.SceneIndex);
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

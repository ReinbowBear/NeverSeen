using UnityEngine;

public class UIManager : MonoBehaviour // кароче этот класс должен быть главным кубиком конструктора всего юай, но это на потом
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;

    public void OpenPauseMenu() => pauseMenu.SetActive(true);
    public void ClosePauseMenu() => pauseMenu.SetActive(false);

    public void ToggleSettings() => settingsMenu.SetActive(!settingsMenu.activeSelf);
}

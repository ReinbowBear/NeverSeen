using UnityEngine;

public class Pause : MonoBehaviour
{
    public void SetPause(bool isPause)
    {
        if (isPause == true)
        {
            BattleKeyboard.gameInput.Disable();
            Time.timeScale = 0;
        }
        else
        {
            BattleKeyboard.gameInput.Enable();
            Time.timeScale = 1;
        }
    }


    void OnEnable()
    {
        SetPause(true);
    }

    void OnDisable()
    {
        SetPause(false);
    }

    //функции для кнопок в меню
    public void ExitToMenu()
    {
        Time.timeScale = 1;
        
        EventBus.Invoke<OnSave>();
        SaveSystem.SaveFile();

        Scene.Load(0);
    }
}

using UnityEngine;

public class TimeManager
{
    private bool isPaused => Time.timeScale == 0f;
    private Input input;

    public TimeManager(Input input)
    {
        this.input = input;
    }


    public void SetTime(float timeValue)
    {
        Time.timeScale = timeValue;
    }

    public void ToglePause()
    {
        SetPause(!isPaused);
    }

    public void SetPause(bool paused)
    {
        Time.timeScale = paused ? 0f : 1f;
        input.SetInputMode(paused ? input.UI : input.GamePlay);
    }
}

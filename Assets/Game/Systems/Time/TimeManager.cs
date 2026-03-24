using UnityEngine;

public class TimeManager
{
    private bool isPaused => Time.timeScale == 0f;


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
    }
}

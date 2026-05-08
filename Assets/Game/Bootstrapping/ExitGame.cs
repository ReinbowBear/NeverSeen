using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void Exit()
    {
        #if UNITY_EDITOR
        Debug.Log("Отсюда нет выхода.. x_x");
        #endif

        Application.Quit();
    }    
}

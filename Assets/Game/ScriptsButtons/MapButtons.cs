using UnityEngine;

public class MapButtons : MonoBehaviour
{
    public void GoBattle()
    {
        EventBus.Invoke<OnSave>();
        Scene.Load(2);
    }

    public void ExitToMenu()
    {
        EventBus.Invoke<OnSave>();
        SaveLoad.SaveFile();
        Scene.Load(0);
    }
}

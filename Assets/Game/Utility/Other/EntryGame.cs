using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class EntryGame : MonoBehaviour
{
    private static readonly List<IAsyncInitializable> ToInit = new();

    void Awake()
    {
        //CheckSave();
        InitGame();

        //Input.Instance.SetInputMode(Input.Instance.GameInput.UI); // на разных сценах разное управление
    }


    private void CheckSave() // стоит подумать как скрипты создают данные, сразу или при получении той или иной команды (инициализация с сейвом и без)
    {
        if (File.Exists(SaveLoad.filePath))
        {
            SaveLoad.LoadFile();
        }
        else
        {
            SaveLoad.SaveFile();
        }
    }

    private async void InitGame()
    {
        EventBus.Invoke<OnSceneEntry>();

        var tasks = ToInit.Select(i => i.AsyncInit());
        await Task.WhenAll(tasks);

        EventBus.Invoke<OnSceneStart>();
        ToInit.Clear();
    }


    private void OnDestroy()
    {
        EventBus.Invoke<OnSceneRelease>();
    }
}

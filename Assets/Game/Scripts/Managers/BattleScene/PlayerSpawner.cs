using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PlayerSpawner : MonoBehaviour
{
    private void LoadCharacter(OnLoad _)
    {
        string characterName = SaveSystem.gameData.generalData.character;
        StartCoroutine(CreatePlayer(characterName));
    }

    private IEnumerator CreatePlayer(string newCharacter)
    {
        var handle = Addressables.InstantiateAsync(newCharacter);
        yield return handle;

        var release = handle.Result.AddComponent<ReleaseOnDestroy>();
        release.handle = handle;
    }


    void OnEnable()
    {
        EventBus.Add<OnLoad>(LoadCharacter);
    }

    void OnDisable()
    {
        EventBus.Remove<OnLoad>(LoadCharacter);
    }
}

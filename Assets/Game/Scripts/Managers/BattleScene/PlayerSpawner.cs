using System.Collections;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private void LoadCharacter(OnLoad _)
    {
        string characterName = SaveSystem.gameData.generalData.character;
        StartCoroutine(CreatePlayer(characterName));
    }

    private IEnumerator CreatePlayer(string newCharacter)
    {
        var handle = Address.GetAssetByName(newCharacter);
        yield return new WaitUntil(() => handle.IsCompleted);
        //GameObject player = handle.Result;
//
        //yield return new WaitForSeconds(0);
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

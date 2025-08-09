using UnityEngine;

public class EntryGame : MonoBehaviour
{
    private static bool isInitGame;

    void Awake()
    {
        InitGame();
    }


    private void InitGame()
    {
        if (!isInitGame)
        {
            //AddressImporter.ImportAddressable();
            RandomManager.SetSeed();

            isInitGame = true;
        }
    }
}

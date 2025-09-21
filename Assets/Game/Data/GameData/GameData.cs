
[System.Serializable]
public class GameData // есть подорение что лучше не держать впамяти всё и сразу всегда
{
    public GeneralData General = new();
    public GameMapData GameMap = new();
    public InventoryData Inventory = new();

    public void LoadData(GameData loaded) // без обёртки и айди может легко поломатся!
    {
        General = loaded.General;
        GameMap = loaded.GameMap;
        Inventory = loaded.Inventory;
    }
}

[System.Serializable]
public class GeneralData
{
    public bool IsGameInit;
    public byte SceneIndex;
    public int Seed;
}

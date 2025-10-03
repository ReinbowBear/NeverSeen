using System.Collections.Generic;

[System.Serializable]
public class InventoryData
{
    public Dictionary<string, string> buildings = new();// label и сам ключ здания в адресейбл
    public List<string> items;
}

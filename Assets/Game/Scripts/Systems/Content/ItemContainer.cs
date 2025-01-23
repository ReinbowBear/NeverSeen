using UnityEngine;

public class ItemSO : ScriptableObject
{
    public InterfaceSO UI;
}

[System.Serializable]
public class InterfaceSO //вывел в отдельный класс, что бы можно было скрывать данные в инспекторе, удобно
{
    public Sprite sprite;
    public string itemName;
    [Space]
    [TextArea(5, 0)]
    public string description;
}

public enum ItemType
{
    None,
    AbilitySO,
    RingSO,
    ArmorSO
}

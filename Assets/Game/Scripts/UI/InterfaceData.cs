using UnityEngine;

[System.Serializable]
public class InterfaceData
{
    public Sprite sprite;
    public string itemName;
    [Space]
    [TextArea(5, 0)]
    public string description;
}

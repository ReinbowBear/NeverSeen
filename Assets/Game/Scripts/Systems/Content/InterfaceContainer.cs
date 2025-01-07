using UnityEngine;

[System.Serializable]
public class InterfaceContainer
{
    public Sprite sprite;
    public string itemName;
    [Space]
    [TextArea(5, 0)]
    public string description;
}

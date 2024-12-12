using UnityEngine;

[CreateAssetMenu(fileName = "InterfaceData", menuName = "ScriptableObject/InterfaceData")]
public class InterfaceSO : ScriptableObject
{
    public Sprite sprite;
    public string itemName;
    [Space]
    [TextArea(5, 0)]
    public string description;
}

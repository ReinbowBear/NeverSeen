using UnityEngine;

[CreateAssetMenu(fileName = "InterfaceCont", menuName = "ScriptableObject/InterfaceCont")]
public class InterfaceSO : ScriptableObject
{
    public Sprite sprite;
    public string itemName;
    [Space]
    [TextArea(5, 0)]
    public string description;
}

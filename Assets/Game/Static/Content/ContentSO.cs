using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "Content", menuName = "ScriptableObject/Content")]
public class ContentSO : ScriptableObject
{
    public Container[] data;
}

[System.Serializable]
public struct Container
{
    public AssetReference prefab;
    public ScriptableObject stats;
    public InterfaceSO UI;
}

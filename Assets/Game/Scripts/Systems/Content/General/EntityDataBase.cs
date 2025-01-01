using UnityEngine;

[CreateAssetMenu(fileName = "entityDB", menuName = "ScriptableObject/entityDB")]
public class EntityDataBase : ScriptableObject
{
    public EntityContainer[] containers;
}

[System.Serializable]
public class EntityContainer
{
    public EntitySO stats;
    [Space]
    public Sprite sprite;
    public string name;
    [Space]
    [TextArea(5, 0)]
    public string description;
}

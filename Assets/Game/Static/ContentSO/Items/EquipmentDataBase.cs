using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentDB", menuName = "ScriptableObject/EquipmentDB")]
public class EquipmentDataBase : ScriptableObject
{
    public EquipmentContainer[] containers;
}

public class EquipmentContainer : ItemContainer
{
    //public EquipmentSO stats;
}

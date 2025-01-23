using UnityEngine;

public class Content : MonoBehaviour
{
    public static Content data;

    public MapDataBase maps;
    public WavesDataBase waves;
    [Space]
    public EntityDataBase characters;
    public EntityDataBase enemys;
    [Space]
    public AbilityDataBase abilityDataBase;
    public AbilityDataBase ringDataBase;
    public AbilityDataBase armorDataBase;

    Content()
    {
        data = this;
    }


    public ScriptableObject GetItemDB(ItemType type)
    {
        switch (type)
        {
            case ItemType.AbilitySO:
                return abilityDataBase;

            case ItemType.RingSO:
                return ringDataBase;
                
            case ItemType.ArmorSO:
                return armorDataBase;
        }
        return null;
    }
}
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
    public AbilityDataBase abilitys;
    public AbilityDataBase rings;
    public AbilityDataBase armors;

    Content()
    {
        data = this;
    }


    public ScriptableObject GetItemDB(ItemType type)
    {
        switch (type)
        {
            case ItemType.ability:
                return abilitys;

            case ItemType.equipment:
                return rings;
                
            case ItemType.armor:
                return armors;
        }
        return null;
    }
}
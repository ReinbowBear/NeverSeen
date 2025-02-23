using UnityEngine;

public class Content : MonoBehaviour
{
    public static Content instance;

    public MapDataBase maps;
    public WavesDataBase waves;
    [Space]
    public EntityDataBase characters;
    public EntityDataBase enemys;
    [Space]
    public ItemDataBase abilityDataBase;
    public ItemDataBase ringDataBase;
    public ItemDataBase armorDataBase;

    Content()
    {
        instance = this;
    }
}
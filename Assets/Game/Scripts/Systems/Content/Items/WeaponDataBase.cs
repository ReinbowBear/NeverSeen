using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDB", menuName = "ScriptableObject/WeaponDB")]
public class WeaponDataBase : ScriptableObject
{
    public WeaponContainer[] containers;
}

[System.Serializable]
public class WeaponContainer : ItemContainer
{
    public WeaponSO stats;
}

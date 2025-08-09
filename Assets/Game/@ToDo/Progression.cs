using System.Collections.Generic;
using UnityEngine;

public class Progression : MonoBehaviour // шаблон через ГПТ
{
    public static Progression Instance;

    private List<UnlockableItem> allItems;
    private HashSet<string> unlockedItems = new();

    private void Awake()
    {
        Instance = this;
        LoadUnlocks();
    }


    private void LoadUnlocks()
    {
        string data = PlayerPrefs.GetString("unlocks", "");

        if (!string.IsNullOrEmpty(data))
        {
            string[] itemIDs = data.Split(',');
            unlockedItems = new HashSet<string>(itemIDs);
        }
    }


    public void Unlock(string id)
    {
        if (!unlockedItems.Contains(id))
        {
            unlockedItems.Add(id);
            SaveUnlocks();
            Debug.Log("Unlocked: " + id);
        }
    }

    public bool IsUnlocked(string id)
    {
        return unlockedItems.Contains(id);
    }

    public List<UnlockableItem> GetUnlockedItems()
    {
        return allItems.FindAll(item => unlockedItems.Contains(item.id));
    }

    private void SaveUnlocks()
    {
        var data = string.Join(",", unlockedItems);
        PlayerPrefs.SetString("unlocks", data);
        PlayerPrefs.Save();
    }
}


[System.Serializable]
public class UnlockableItem
{
    public string id;
    public string displayName;
    public string description;
    public bool unlocked;
}
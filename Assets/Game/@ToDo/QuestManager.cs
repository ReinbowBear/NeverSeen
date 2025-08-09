using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    private List<Quest> Quests = new();
    private byte currentQuests;

    public QuestUI questUI; // Ссылка на UI

    void Awake()
    {
        Instance = this;
    }

    public void AddQuest(Quest quest)
    {
        Quests.Add(quest);
        questUI.AddQuest(quest);
    }
}


public class Quest : ICondition
{
    public string Title;
    public string Description;

    private short requiredValue;
    private short currentValue;

    public Quest(short newRequiredValue)
    {
        requiredValue = newRequiredValue;
    }

    public string GetProgress()
    {
        return $"{Mathf.Min(currentValue, requiredValue)} / {requiredValue}";
    }

    public bool IsConditionMet()
    {
        return true;
    }
}


public class QuestUI : MonoBehaviour
{
    public Transform questListParent;
    public GameObject questItemPrefab;

    private Dictionary<Quest, GameObject> questItems = new();

    public void AddQuest(Quest quest)
    {
        GameObject item = Instantiate(questItemPrefab, questListParent);
        questItems[quest] = item;
        UpdateQuestItem(quest, item);
    }

    public void UpdateUI(List<Quest> quests)
    {
        foreach (var quest in quests)
        {
            if (questItems.TryGetValue(quest, out var item))
            {
                UpdateQuestItem(quest, item);
            }
        }
    }

    public void RemoveQuest(Quest quest)
    {
        if (questItems.TryGetValue(quest, out var item))
        {
            Destroy(item);
            questItems.Remove(quest);
        }
    }

    private void UpdateQuestItem(Quest quest, GameObject item)
    {
        item.transform.Find("Title").GetComponent<Text>().text = quest.Title;
        item.transform.Find("Description").GetComponent<Text>().text = quest.Description;
        item.transform.Find("Progress").GetComponent<Text>().text = quest.GetProgress();
    }
}

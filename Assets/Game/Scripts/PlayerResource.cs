using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerResource : MonoBehaviour
{
    public static PlayerResource Instance;

    private List<Building> buildings;
    private Dictionary<ResourceType, int> resources;
    private Dictionary<ResourceType, int> resourcesStack;
    [SerializeField] private float TickRate;

    [SerializeField] private SerializableDictionary<ResourceType, TextMeshProUGUI> UI_texts;
    private Coroutine coroutine;

    void Awake()
    {
        Instance = this;
    }


    public void AddBuilding(Building newBuilding)
    {
        buildings.Add(newBuilding);
        if (buildings.Count > 0 && coroutine == null)
        {
            coroutine = StartCoroutine(CollectResources());
        }
    }

    public void RemoveBuilding(Building newBuilding)
    {
        buildings.Remove(newBuilding);
        if (buildings.Count == 0)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    private IEnumerator CollectResources()
    {
        while (buildings.Count != 0)
        {
            foreach (var Building in buildings) // собираем ресурсы
            {
                Building.Work(resourcesStack);
            }

            foreach (var key in resourcesStack.Keys) // сохраняем ресурсы и обновляем UI
            {
                resources[key] += resourcesStack[key];
                UI_texts[key].text = resources[key].ToString();
            }

            foreach (var key in resourcesStack.Keys) // очищаем список собранных ресурсов (для дальнейших сборов)
            {
                resourcesStack[key] = 0;
            }

            yield return new WaitForSeconds(TickRate);
        }
    }

    #region Other
    private void Load(OnLoad _)
    {

    }

    private void Save(OnSave _)
    {

    }


    void OnEnable()
    {
        EventBus.Add<OnLoad>(Load);
        EventBus.Add<OnSave>(Save);
    }

    void OnDisable()
    {
        EventBus.Remove<OnLoad>(Load);
        EventBus.Remove<OnSave>(Save);
    }
    #endregion
}

public enum ResourceType
{
    money,
}
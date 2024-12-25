using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DangeonPanel : MonoBehaviour
{
    [SerializeField] private Image[] enemyImages;
    [SerializeField] private TextMeshProUGUI[] enemyCount;

    [HideInInspector] public Dangeon chosenDangeon;

    public void StartMap()
    {
        if (chosenDangeon.mapState == Dangeon.MapState.activate)
        {
            ClosePanel();

            //onNewBattle.Invoke();
            SaveSystem.onSave.Invoke();
            Scene.Load(2);
        }
    }

    public void ShowMap(Dangeon map)
    {
        chosenDangeon = map;

        for (byte i = 0; i < enemyImages.Length; i++)
        {
            if (i < map.mapData.enemyIndex.Length)
            {
                Debug.Log("тут надо настроить");
                //enemyImages[i].sprite = Content.data.enemys[map.mapData.enemyIndex[i]].sprite;
                enemyCount[i].text = map.mapData.enemyCount[i].ToString();
            }
            else
            {
                enemyImages[i].sprite = null;
                enemyCount[i].text = null;
            }
        }
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }


    private void SaveMap()
    {
        SaveDangeonPanel saveDangeonPanel = new SaveDangeonPanel();
        saveDangeonPanel.mapData = chosenDangeon.mapData;

        SaveSystem.gameData.saveDangeonPanel = saveDangeonPanel;
    }


    void OnEnable()
    {
        SaveSystem.onSave += SaveMap;
    }

    void OnDisable()
    {
        SaveSystem.onSave -= SaveMap;
    }
}

[System.Serializable]
public struct SaveDangeonPanel
{
    public DangeonData mapData; //классы явно не сохраняются в этой штуке... но как переход для сцены терпимо
}

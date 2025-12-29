using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnumPanel : MonoBehaviour
{
    public GameObject ItemPref;

    private InventoryData inventory;

    private Dictionary<string, Button> Buttons = new();
    private Dictionary<Button, UnityAction> SubLamdas = new();

    public void CheckItems(OnSceneStart _)
    {
        foreach (var value in inventory.buildings.Values)
        {
            if (Buttons.ContainsKey(value)) continue;
            //AddPref(value);
        }
    }

    //public async void AddPref(string buildingName)
    //{
    //    var asset = await factory.GetAsset(buildingName);
//
    //    var buttonObj = factory.Instantiate(ItemPref, transform);
    //    var Button = buttonObj.GetComponent<Button>();
    //    var GetBuildingSript = buttonObj.GetComponent<GetBuilding>();
    //    var modelView = buttonObj.GetComponent<MeshButtonView>();
//
    //    UnityAction action = () => GetBuildingSript.GetBuildingToMouse(buildingName);
    //    SubLamdas.Add(Button, action);
    //    Button.onClick.AddListener(action);
//
    //    modelView.SetModel(asset);
    //}


    void OnDestroy()
    {
        foreach (var keyButton in SubLamdas.Keys)
        {
            var action = SubLamdas[keyButton];
            keyButton.onClick.RemoveListener(action);
        }
    }
}

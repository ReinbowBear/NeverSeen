using UnityEngine;

public class BuildButton : ButtonView
{
    private string BuildingPrefName;


    public void SetPrefab(string prefName)
    {
        BuildingPrefName = prefName;
    }
}

using UnityEngine;

public class BuildButton : ButtonOffsetView
{
    private string BuildingPrefName;


    public void SetPrefab(string prefName)
    {
        BuildingPrefName = prefName;
    }
}

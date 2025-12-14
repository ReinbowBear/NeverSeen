using UnityEngine;

public class BuildButton : OffsetView
{
    private string BuildingPrefName;


    public void SetPrefab(string prefName)
    {
        BuildingPrefName = prefName;
    }
}

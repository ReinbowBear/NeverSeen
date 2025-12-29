using System;
using System.Collections.Generic;

public class ChangedTracker
{
    private List<List<Entity>> changedLists = new(); // лист на каждый тип компонента, хранящий сущность которая изменялась

    public void RegisterComponent(int compIndex)
    {
        if (changedLists[compIndex] == null)
        {
            changedLists[compIndex] = new();
        }
    }

    public List<Entity> GetChangedList(int compIndex)
    {
        return changedLists[compIndex];
    }


    public void ClearAll()
    {
        foreach (var list in changedLists)
        {
            list.Clear();
        }
    }
}

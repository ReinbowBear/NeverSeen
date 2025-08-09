using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NodeGroup : Group
{
    public string ID;
    public string oldTitle; // для проверок на переименирование, устроенно иначе чем у нодов потому что тут мы работает с уже существующим классом Group

    private Color defaultColor;
    private float defaultBorderWidght;

    public NodeGroup(string groupTitle, Vector2 pos)
    {
        ID = Guid.NewGuid().ToString();

        title = groupTitle;
        oldTitle = groupTitle;

        SetPosition(new Rect(pos, Vector2.zero));

        defaultColor = contentContainer.style.borderBottomColor.value;
        defaultBorderWidght = contentContainer.style.borderBottomWidth.value;
    }


    public void SetDefaultColor()
    {
        contentContainer.style.borderBottomColor = defaultColor;
        contentContainer.style.borderBottomWidth = defaultBorderWidght;
    }

    public void SetErrorColor(Color color)
    {
        contentContainer.style.borderBottomColor = color;
        contentContainer.style.borderBottomWidth = 2f;
    }
}

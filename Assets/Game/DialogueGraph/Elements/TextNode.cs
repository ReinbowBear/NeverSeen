using UnityEngine;

[System.Serializable]
public class TextNode : BaseNode
{
    protected override void SetColor()
    {
        mainContainer.style.backgroundColor = new Color(0.100f, 0.100f, 0.200f);
    }

    protected override void SetOutput()
    {
        base.SetOutputPort();
    }
}

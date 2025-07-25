using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TextNode : DialogueNode
{
    public override void Init(DialogueGraph graph, Vector2 pos)
    {
        NodeType = NodeType.TextNode;

        ChoiceData choiceData = new ChoiceData()
        {
            Text = $"Choice: {Choices.Count}"
        };

        Choices.Add(choiceData);

        base.Init(graph, pos);
    }
}

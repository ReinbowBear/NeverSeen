using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class DialogueGraphTool : EditorWindow
{
    private ProxyDialogueGraph proxyGraph;


    [MenuItem("Tools/Dialogue Graph")]
    private static void ShowDialogueGraph()
    {
        var graphTool = GetWindow<DialogueGraphTool>("Dialogue Graph");

        graphTool.InitGraph();
        graphTool.InitToolBar();
    }


    private void InitGraph()
    {
        proxyGraph = new();
        proxyGraph.Graph.StretchToParentSize();
        rootVisualElement.Add(proxyGraph.Graph);
    }

    private void InitToolBar()
    {
        var toolbar = new Toolbar();

        var saveButton = new Button(() => proxyGraph.Save()) { text = "Save" };
        var loadButton = new Button(() => proxyGraph.Load()) { text = "Load" };
        var clearButton = new Button(() => proxyGraph.ClearGraph()) { text = "Clear" };

        toolbar.Add(proxyGraph.Graph.FileName);

        toolbar.Add(saveButton);
        toolbar.Add(loadButton);
        toolbar.Add(clearButton);

        rootVisualElement.Add(toolbar);
    }
}

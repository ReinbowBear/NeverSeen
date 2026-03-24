using System.IO;
using UnityEditor;
using UnityEngine;

public class ProxyDialogueGraph
{
    public DialogueGraph Graph;
    private DialogueGraphManipulators Manipulators;
    private string fileName => Graph.FileName.value;

    private GraphSave graphSave;

    public ProxyDialogueGraph()
    {
        Graph = new();
        Manipulators = new(Graph);

        graphSave = new();
    }


    public void Save()
    {
        if (string.IsNullOrEmpty(fileName)) { Debug.Log("Invalid file Name"); return; }
        graphSave.Save(Graph);
    }

    public void Load()
    {
        var path = SaveLoad.GetPath(ConfigType.Graph);

        string filePath = EditorUtility.OpenFilePanel("Dialogue Graph", path, "asset");
        if (string.IsNullOrEmpty(filePath)) return;

        Graph.ClearGraph();

        graphSave.Load(Graph, Path.GetFileNameWithoutExtension(filePath));
    }


    public void ClearGraph()
    {
        Graph.ClearGraph();
    }
}

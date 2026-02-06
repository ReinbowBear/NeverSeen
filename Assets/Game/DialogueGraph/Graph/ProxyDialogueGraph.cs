using System.IO;
using UnityEditor;
using UnityEngine;

public class ProxyDialogueGraph
{
    public DialogueGraph Graph;
    public DialogueGraphManipulators Manipulators;
    public string fileName => Graph.FileName.value;

    public ProxyDialogueGraph()
    {
        Graph = new();
        Manipulators = new(Graph);
    }


    public void Save()
    {
        if (string.IsNullOrEmpty(fileName)) { Debug.Log("Invalid file Name"); return; }

        var graphSave = new DialogueGraphSave(Graph, fileName);
        graphSave.Save();
    }

    public void Load()
    {
        string filePath = EditorUtility.OpenFilePanel("Dialogue Graph", MyPaths.GRAPHS, "asset");
        if (string.IsNullOrEmpty(filePath)) return;

        Graph.ClearGraph();

        var graphSave = new DialogueGraphSave(Graph, Path.GetFileNameWithoutExtension(filePath));
        graphSave.Load();
    }


    public void ClearGraph()
    {
        Graph.ClearGraph();
    }
}

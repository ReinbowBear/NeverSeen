using System.IO;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueGraphTool : EditorWindow
{
    private DialogueGraph graphView;
    private TextField fileName;


    [MenuItem("Tools/Dialogue Graph")]
    private static void ShowDialogueGraph()
    {
        DialogueGraphTool graphTool = GetWindow<DialogueGraphTool>("Dialogue Graph");

        graphTool.InitGraphView();
        graphTool.InitToolBar();
    }


    private void InitGraphView()
    {
        graphView = new DialogueGraph();
        graphView.StretchToParentSize();

        rootVisualElement.Add(graphView);
    }

    private void InitToolBar()
    {
        Toolbar toolbar = new Toolbar();

        fileName = new TextField()
        {
            value = graphView.Name,
            label = "File Name:"
        };

        UnityEngine.UIElements.Button saveButton = new UnityEngine.UIElements.Button(() => Save()) { text = "Save" };
        UnityEngine.UIElements.Button loadButton = new UnityEngine.UIElements.Button(() => Load()) { text = "Load" };
        UnityEngine.UIElements.Button clearButton = new UnityEngine.UIElements.Button(() => graphView.ClearGraph()) { text = "clear" };

        toolbar.Add(fileName);

        toolbar.Add(saveButton);
        toolbar.Add(loadButton);
        toolbar.Add(clearButton);

        rootVisualElement.Add(toolbar);
    }


    private void Save()
    {
        if (string.IsNullOrEmpty(fileName.value))
        {
            Debug.Log("Invalid file Name");
            return;
        }

        DialogueGraphSave dialogueGraphSave = new DialogueGraphSave(graphView, fileName.value);
        dialogueGraphSave.Save();
    }

    private void Load()
    {
        string filePath = EditorUtility.OpenFilePanel("Dialogue Graph", MyPaths.GRAPHS, "asset");

        if (string.IsNullOrEmpty(filePath))
        {
            return;
        }

        graphView.ClearGraph();

        DialogueGraphSave dialogueGraphSave = new DialogueGraphSave(graphView, Path.GetFileNameWithoutExtension(filePath));
        dialogueGraphSave.Load();

        fileName.value = graphView.Name;
    }
}

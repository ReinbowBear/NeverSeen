using System.IO;

public static class MyPaths
{
    public static readonly string SAVE_ROOT = "Assets/Game/Save";
    public static readonly string DATA_ROOT = "Assets/Game/Data";

    public static readonly string SAVE = Path.Combine(SAVE_ROOT, "GameSave");
    public static readonly string INPUTS = Path.Combine(SAVE_ROOT, "Inputs");
    public static readonly string GRAPHS = Path.Combine(SAVE_ROOT, "Graphs");
    public static readonly string GENERATE = Path.Combine(DATA_ROOT, "Generated");
    public static readonly string EDITOR = "Assets/Editor/Resources";
}

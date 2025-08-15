using System.IO;

public static class MyPaths
{
    public static readonly string SAVE_ROOT = "Assets/Game/Save";

    public static readonly string SAVE = Path.Combine(SAVE_ROOT, "GameSave");
    public static readonly string INPUTS = Path.Combine(SAVE_ROOT, "Inputs");
    public static readonly string ADRESS = Path.Combine(SAVE_ROOT, "Addressable");
    public static readonly string GRAPHS = Path.Combine(SAVE_ROOT, "Graphs");
    public static readonly string EDITOR = Path.Combine(SAVE_ROOT, "Resources");
}

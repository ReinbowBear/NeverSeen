using System.IO;
using UnityEditor;
using UnityEngine;

public class CodeStats : MonoBehaviour
{
    [MenuItem("Tools/Count My Code Lines")]
    public static void CountLines()
    {
        string path = Application.dataPath + "/Game";
        int totalLines = 0;

        foreach (string file in Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories))
        {
            int lineCount = File.ReadAllLines(file).Length;
            totalLines += lineCount;
        }

        Debug.Log($"Строчек кода в проекте: {totalLines}");
    }
}

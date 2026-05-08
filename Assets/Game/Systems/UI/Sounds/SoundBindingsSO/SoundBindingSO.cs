using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundBindingSO", menuName = "Scriptable Objects/SoundBindingSO")]
public class SoundBindingSO : ScriptableObject
{
    public TypeReference EventType;
    public SoundSO Sound;



    #if UNITY_EDITOR
    private void OnValidate()
    {
        if (Sound == null) return;

        string newName = $"{Sound.name}Bind";

        string assetPath = AssetDatabase.GetAssetPath(this);
        string currentName = System.IO.Path.GetFileNameWithoutExtension(assetPath);

        if (currentName != newName)
        {
            AssetDatabase.RenameAsset(assetPath, newName);
            AssetDatabase.SaveAssets();
        }
    }
    #endif
}

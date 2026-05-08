using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEngine;

public static class AddressablesGenerator
{
    [MenuItem("Tools/Export addressables keys")]
    public static void Generate()
    {
        var settings = AddressableAssetSettingsDefaultObject.Settings;

        foreach (var group in settings.groups)
        {
            var genClass = new CodeClass(group.name);


            foreach (var entry in group.entries)
            {
                var key = entry.address;
                genClass.AddField<string>(key);
            }

            SaveLoad.Save(genClass, group.name, ConfigType.Generated);
        }

        Debug.Log("// не работает почему то!");
    }
}

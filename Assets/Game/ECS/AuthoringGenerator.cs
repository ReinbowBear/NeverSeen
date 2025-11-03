#if UNITY_EDITOR
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;

public static class AuthoringGenerator
{
    private static string GeneratedPath = MyPaths.GENERATE;

    [MenuItem("Tools/Generate Authorings")]
    public static void GenerateAuthorings()
    {
        if (!Directory.Exists(GeneratedPath))
        {
            Directory.CreateDirectory(GeneratedPath);
        }

        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var componentTypes = assemblies.SelectMany(a => { try { return a.GetTypes(); } catch (ReflectionTypeLoadException e) { return e.Types.Where(t => t != null); } } )
        .Where(t => typeof(IComponentData).IsAssignableFrom(t) && t.IsValueType && !t.IsAbstract)
        .ToArray();

        foreach (var type in componentTypes)
        {
            GenerateAuthoringFor(type);
        }

        AssetDatabase.Refresh();
    }

    private static void GenerateAuthoringFor(Type componentType)
    {
        string className = componentType.Name + "Authoring";
        string filePath = Path.Combine(GeneratedPath, className + ".cs");

        if (File.Exists(filePath)) return;

        var sb = new StringBuilder();
        sb.AppendLine("using UnityEngine;");
        sb.AppendLine("using Unity.Entities;");
        sb.AppendLine();
        sb.AppendLine($"public class {className} : MonoBehaviour, IAuthoring");
        sb.AppendLine("{");

        var fields = componentType.GetFields(BindingFlags.Public | BindingFlags.Instance);
        foreach (var f in fields)
        {
            sb.AppendLine($"    public {f.FieldType.FullName} {f.Name};");
        }

        sb.AppendLine();
        sb.AppendLine("    public void Bake(Entity entity, World world)");
        sb.AppendLine("    {");
        sb.AppendLine($"        var component = new {componentType.FullName}");
        sb.AppendLine("        {");

        for (int i = 0; i < fields.Length; i++)
        {
            var f = fields[i];
            string comma = i < fields.Length - 1 ? "," : "";
            sb.AppendLine($"            {f.Name} = this.{f.Name}{comma}");
        }

        sb.AppendLine("        };");
        sb.AppendLine("        world.AddComponent(entity, component);");
        sb.AppendLine("    }");
        sb.AppendLine("}");

        File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
    }
}
#endif

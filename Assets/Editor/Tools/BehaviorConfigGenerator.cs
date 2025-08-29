using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;

public static class BehaviorConfigGenerator
{
    private static string path = MyPaths.GENERATE;
    private static List<Type> cachedTypes;
    private static Dictionary<Type, string> cachedEffectSignatures = new();


    [MenuItem("Tools/Generate IBehavior Configs")]
    public static void GenerateEffectConfigs()
    {
        Directory.CreateDirectory(MyPaths.GENERATE);

        var types = GetTypes();
        foreach (var type in types)
        {
            var construct = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault();
            if (construct == null)
            {
                Debug.LogWarning($"Конструктор не найден! : {type.Name}");
                continue;
            }

            string currentSignature = GetConstructorSignature(construct);
            if (cachedEffectSignatures.TryGetValue(type, out var cachedSignature))
            {
                if (cachedSignature == currentSignature) continue;  // скрипт не изменился пропускаем генерацию
            }
            cachedEffectSignatures[type] = currentSignature;

            string configClassName = type.Name + "Config";
            string filePath = Path.Combine(path, configClassName + ".cs");

            string newCode = GenerateCode(type, construct, configClassName);
            if (File.Exists(filePath))
            {
                string existingCode = File.ReadAllText(filePath);
                if (existingCode == newCode) continue;
            }

            File.WriteAllText(filePath, newCode, Encoding.UTF8);
        }

        AssetDatabase.Refresh();
        ConfigSOEditor.ResetCache();
        Debug.Log("Сереализируемые классы поведений сгенерированы!");
    }


    private static List<Type> GetTypes()
    {
        if (cachedTypes != null) return cachedTypes;

        cachedTypes = AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(a =>
        {
            try { return a.GetTypes(); }
            catch { return new Type[0]; }
        })
        .Where(t => typeof(IBehavior).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
        .ToList();

        return cachedTypes;
    }

    private static string GetConstructorSignature(ConstructorInfo construct)
    {
        var parts = construct.GetParameters()
        .Select(p => $"{p.ParameterType.FullName}:{p.Name}");

        return string.Join(";", parts);
    }


    private static string GenerateCode(Type type, ConstructorInfo construct, string configClassName)
    {
        var sb = new StringBuilder();
        sb.AppendLine("using System;");
        sb.AppendLine();
        sb.AppendLine("[Serializable]");
        sb.AppendLine($"public class {configClassName} : {nameof(IConfig)}");
        sb.AppendLine("{");

        foreach (var param in construct.GetParameters())
        {
            string paramTypeName = GetCSharpTypeName(param.ParameterType);
            sb.AppendLine($"    public {paramTypeName} {param.Name};");
        }

        sb.AppendLine();
        sb.AppendLine($"    public {nameof(IConfig)} Build()");
        sb.AppendLine("    {");

        sb.Append("        return new " + type.Name + "(");
        sb.Append(string.Join(", ", construct.GetParameters().Select(p => p.Name)));
        sb.AppendLine(");");

        sb.AppendLine("    }");
        sb.AppendLine("}");

        return sb.ToString();
    }

    private static string GetCSharpTypeName(Type type)
    {
        if (type == typeof(int)) return "int";
        if (type == typeof(float)) return "float";
        if (type == typeof(double)) return "double";
        if (type == typeof(string)) return "string";
        if (type == typeof(bool)) return "bool";

        if (type.IsGenericType)
        {
            var genericTypeName = type.Name.Substring(0, type.Name.IndexOf('`'));
            var genericArgs = string.Join(", ", type.GetGenericArguments().Select(GetCSharpTypeName));
            return $"{genericTypeName}<{genericArgs}>";
        }

        return type.FullName.Replace("+", ".");
    }
}

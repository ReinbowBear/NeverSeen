using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CodeClass
{
    private string name;
    private CodeClassType type;

    private List<Type> interfaces = new();
    private List<CodeField> fields = new();
    private List<CodeMethod> methods = new();

    public CodeClass(string name, CodeClassType type = CodeClassType.Class)
    {
        this.name = name;
        this.type = type;
    }


    public CodeClass AddInterface<T>()
    {
        interfaces.Add(typeof(T));
        return this;
    }

    public CodeClass AddField<T>(string fieldName = null)
    {
        fields.Add(new CodeField(typeof(T), fieldName));
        return this;
    }

    public CodeClass AddMethod(CodeMethod method)
    {
        methods.Add(method);
        return this;
    }


    public string Build()
    {
        var builder = new StringBuilder();

        var clasType = type == CodeClassType.Class ? "class" : "struct";

        builder.Append($"public {clasType} {name}");

        if (interfaces.Count > 0)
        {
            builder.Append(" : ");
            builder.Append(string.Join(", ", interfaces.Select(GetTypeName)));
        }

        builder.AppendLine();
        builder.AppendLine("{");

        foreach (var field in fields)
        {
            builder.AppendLine("    " + field.Build(GetTypeName));
        }

        foreach (var method in methods)
        {
            builder.AppendLine(method.Build(GetTypeName));
        }

        builder.AppendLine("}");

        return builder.ToString();
    }


    private string GetTypeName(Type type)
    {
        switch (Type.GetTypeCode(type))
        {
            case TypeCode.Int32: return "int";
            case TypeCode.String: return "string";
            case TypeCode.Boolean: return "bool";
            case TypeCode.Single: return "float";
            case TypeCode.Object: break;
        }

        if (type == typeof(void)) return "void";

        if (type.IsArray)
        {
            return $"{GetTypeName(type.GetElementType())}[]";
        }

        if (type.IsGenericType)
        {
            var genericName = type.Name[..type.Name.IndexOf('`')];
            var genericArgs = string.Join(", ", type.GetGenericArguments().Select(GetTypeName));

            return $"{genericName}<{genericArgs}>";
        }

        return type.Name;
    }
}

public enum CodeClassType
{
    Class,
    Struct
}

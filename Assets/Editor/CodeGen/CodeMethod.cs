using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CodeMethod
{
    private string name;
    private Type returnType;
    private List<(Type type, string name)> parameters = new();
    private List<string> body = new();

    public CodeMethod(string name, Type returnType = null)
    {
        this.name = name;
        this.returnType = returnType ?? typeof(void);
    }


    public CodeMethod AddParameter(Type type, string name = null)
    {
        parameters.Add((type, name));
        return this;
    }

    public CodeMethod AddBodyLine(string line)
    {
        body.Add(line);
        return this;
    }


    public string Build(Func<Type, string> getTypeNameFunc)
    {
        var builder = new StringBuilder();

        var paramList = parameters.Select(param => $"{getTypeNameFunc(param.type)} {param.name ?? ToCamelCase(param.type.Name)}").ToArray();
        var returnTypeName = getTypeNameFunc(returnType);

        builder.AppendLine($"    public {returnTypeName} {name}({string.Join(", ", paramList)})");
        builder.AppendLine("    {");

        foreach (var line in body)
        {
            builder.AppendLine("        " + line);
        }

        builder.AppendLine("    }");

        return builder.ToString();
    }


    private string ToCamelCase(string value)
    {
        if (string.IsNullOrEmpty(value)) return value;
        return char.ToLower(value[0]) + value[1..];
    }
}

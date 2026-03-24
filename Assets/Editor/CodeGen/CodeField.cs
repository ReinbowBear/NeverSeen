using System;

public struct CodeField
{
    private Type type;
    private string name;

    public CodeField(Type type, string name = null)
    {
        this.type = type;
        this.name = name ?? this.type.Name;
    }


    public string Build(Func<Type, string> getTypeNameFunc)
    {
        var typeName = getTypeNameFunc(type);
        string init = "";

        if (!type.IsValueType && type != typeof(string))
        {
            init = $" = new()";
        }

        return $"public {typeName} {name}{init};";
    }
}

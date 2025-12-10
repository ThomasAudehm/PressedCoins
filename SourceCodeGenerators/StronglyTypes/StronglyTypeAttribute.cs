namespace PressedCoins.SourceCodeGenerators.StronglyTypes;
/// <summary>
/// Defines a Strongly Type of a given Primitive 
/// </summary>
/// 
[AttributeUsage(AttributeTargets.Struct)]
public class StronglyTypeAttribute : Attribute
{
    public Type Type { get; }

    public StronglyTypeAttribute(Type type)
    {
        Type = type;
    }
}
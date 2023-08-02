using System.Reflection;

namespace TollFeeCalculator.Domain.Shared.BaseClasses;

/// <summary>
/// A suitable replacement for C# enum
/// </summary>
/// <typeparam name="TEnum"></typeparam>
public abstract class Enumeration<TEnum> 
    : IEquatable<Enumeration<TEnum>> where TEnum : Enumeration<TEnum>
{
    private static readonly Dictionary<int, TEnum> Enumerations = CreateEnumerations();

    public int Value { get; protected init; }
    public string Name { get; protected init; }

    protected Enumeration()
    {
    }

    protected Enumeration(int value, string name)
    {
        Value = value;
        Name = name;
    }

    public static TEnum FromValue(int value)
    {
        if (Enumerations.TryGetValue(value, out var enumeration) is false)
            throw new ArgumentOutOfRangeException(nameof(value), value, $"Value: {value} is not valid for {typeof(TEnum)}");
        
        return enumeration;
    }
    
    public static TEnum? FromName(string name)
    {
        return Enumerations
            .Values
            .FirstOrDefault(x => x.Name == name);
    }
    
    public bool Equals(Enumeration<TEnum>? other)
    {
        if (ReferenceEquals(null, other)) 
            return false;
        
        if (ReferenceEquals(this, other)) 
            return true;

        return GetType() == other.GetType() && 
               Value == other.Value;
    }

    public override bool Equals(object? obj) 
        => obj is Enumeration<TEnum> other && Equals(other);

    public override int GetHashCode() 
        => Value.GetHashCode();

    public override string ToString() => Name;
    
    private static Dictionary<int, TEnum> CreateEnumerations()
    {
        var enumerationType = typeof(TEnum);

        var fieldsForType = enumerationType
            .GetFields(bindingAttr:
                BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.FlattenHierarchy)
            .Where(fieldInfo => enumerationType.IsAssignableFrom(fieldInfo.FieldType))
            .Select(fieldInfo => (TEnum)fieldInfo.GetValue(default)!);

        return fieldsForType.ToDictionary(x => x.Value);
    }
}
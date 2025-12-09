using System.ComponentModel;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace PressedCoins.Domain.StronglyTypes;

[JsonConverter(typeof(CoinIdJsonConverter))]
[TypeConverter(typeof(CoinIdTypeConverter))]
public readonly struct CoinId(Guid value) : IComparable<CoinId>, IEquatable<CoinId>
{
    public Guid Value { get; } = value;
    
    public static CoinId New() => new(Guid.NewGuid());
    public bool Equals(CoinId other) => this.Value.Equals(other.Value);
    public int CompareTo(CoinId other) => Value.CompareTo(other.Value);

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        return obj is CoinId other && Equals(other);
    }

    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value.ToString();

    public static bool operator ==(CoinId a, CoinId b) => a.CompareTo(b) == 0;
    public static bool operator !=(CoinId a, CoinId b) => !(a == b);
    
    class CoinIdJsonConverter : JsonConverter<CoinId>
    {
        public override CoinId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        { 
           return new CoinId(reader.GetGuid());
        }

        public override void Write(Utf8JsonWriter writer, CoinId value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }

    class CoinIdTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            var stringValue = value as string;
            if (!string.IsNullOrEmpty(stringValue)
                && Guid.TryParse(stringValue, out var guid))
            {
                return new CoinId(guid);
            }

            return base.ConvertFrom(context, culture, value);
        }
    }

    public class EfCoreConverter : ValueConverter<CoinId, Guid>
    {
        public EfCoreConverter(ConverterMappingHints? mappingHints = null) 
            : base( 
                id => id.Value,
                value => new CoinId(value),
                mappingHints) {}
    }
    
}
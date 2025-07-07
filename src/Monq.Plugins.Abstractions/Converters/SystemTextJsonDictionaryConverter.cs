using System.Text.Json;
using System.Text.Json.Serialization;

namespace Monq.Plugins.Abstractions.Converters;

internal class SystemTextJsonDictionaryConverter : JsonConverter<IDictionary<string, object?>>
{
    readonly JsonSerializerOptions _options;

    public SystemTextJsonDictionaryConverter(JsonSerializerOptions options)
    {
        _options = options;
    }

    public override bool CanConvert(Type typeToConvert)
        => typeof(IDictionary<string, object?>).IsAssignableFrom(typeToConvert);

    public override IDictionary<string, object?>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException();

        var dictionary = new Dictionary<string, object?>();
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                return dictionary;

            if (reader.TokenType != JsonTokenType.PropertyName)
                throw new JsonException();

            var propertyName = reader.GetString();
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new JsonException("Failed to get property name");

            reader.Read();
            var value = ReadValue(ref reader, options);

            dictionary[propertyName] = value;
        }

        throw new JsonException();
    }

    public override void Write(Utf8JsonWriter writer, IDictionary<string, object?> value, JsonSerializerOptions options)
        => JsonSerializer.Serialize(writer, value, _options);

    object? ReadValue(ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
                return reader.GetString();
            case JsonTokenType.Number:
                if (reader.TryGetInt32(out var intValue))
                {
                    return intValue;
                }
                if (reader.TryGetInt64(out var longValue))
                {
                    return longValue;
                }
                if (reader.TryGetDouble(out var doubleValue))
                {
                    return doubleValue;
                }
                break;
            case JsonTokenType.True:
                return true;
            case JsonTokenType.False:
                return false;
            case JsonTokenType.Null:
                return null;
            case JsonTokenType.StartObject:
                return Read(ref reader, typeof(Dictionary<string, object?>), options);
            case JsonTokenType.StartArray:
                var list = new List<object?>();
                while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    list.Add(ReadValue(ref reader, options));
                return list;
        }
        return null;
    }
}

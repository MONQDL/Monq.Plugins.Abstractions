using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Monq.Plugins.Abstractions.Converters;

[Obsolete]
internal class DictionaryConverter : CustomCreationConverter<IDictionary<string, object?>>
{
    public override IDictionary<string, object?> Create(Type objectType)
        => new Dictionary<string, object?>();

    public override bool CanConvert(Type objectType)
        => objectType == typeof(object) || base.CanConvert(objectType);

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType is JsonToken.StartObject or JsonToken.Null)
            return base.ReadJson(reader, objectType, existingValue, serializer);

        return serializer.Deserialize(reader);
    }
}

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;

namespace Monq.Plugins.Abstractions.Legacy.Converters;

internal class JsonValueConverter : JsonConverter<JsonValue>
{
    public override JsonValue? ReadJson(JsonReader reader, Type objectType, JsonValue? existingValue, bool hasExistingValue, JsonSerializer serializer)
        => reader.TokenType switch
        {
            JsonToken.Null or JsonToken.Undefined => JsonValue.Create((object?)null),
            JsonToken.Boolean => JsonValue.Create(reader.ReadAsBoolean()),
            JsonToken.Float => JsonValue.Create(reader.ReadAsDouble()),
            JsonToken.Integer => JsonValue.Create(reader.ReadAsInt32()),
            JsonToken.String => JsonValue.Create(reader.ReadAsString()),
            JsonToken => throw new JsonException($"Unsupported token type {reader.TokenType}")
        };

    public override void WriteJson(JsonWriter writer, JsonValue? value, JsonSerializer serializer)
    {
        if (value == null)
            return;
        var jToken = JToken.Parse(value.ToJsonString());
        jToken.WriteTo(writer);
    }
}

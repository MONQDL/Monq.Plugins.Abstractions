using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;

namespace Monq.Plugins.Abstractions.Legacy.Converters;

internal class JsonObjectConverter : JsonConverter<JsonObject>
{
    public override JsonObject? ReadJson(JsonReader reader, Type objectType, JsonObject? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var jToken = JToken.ReadFrom(reader);
        return (JsonObject?)JsonNode.Parse(jToken.ToString());
    }

    public override void WriteJson(JsonWriter writer, JsonObject? value, JsonSerializer serializer)
    {
        if (value == null)
            return;
        var jToken = JToken.Parse(value.ToString());
        jToken.WriteTo(writer);
    }
}

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;

namespace Monq.Plugins.Abstractions.Legacy.Converters;

internal class JsonArrayConverter : JsonConverter<JsonArray>
{
    public override JsonArray? ReadJson(JsonReader reader, Type objectType, JsonArray? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var jToken = JToken.ReadFrom(reader);
        return (JsonArray?)JsonNode.Parse(jToken.ToString());
    }

    public override void WriteJson(JsonWriter writer, JsonArray? value, JsonSerializer serializer)
    {
        if (value == null)
            return;
        var jToken = JToken.Parse(value.ToString());
        jToken.WriteTo(writer);
    }
}

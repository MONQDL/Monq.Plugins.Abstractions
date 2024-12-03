using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Monq.Plugins.Abstractions.Converters;

internal class NewtonsoftJValueConverter : JsonConverter<JValue>
{
    public override JValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var jsonDoc = JsonDocument.ParseValue(ref reader);
        var objStr = jsonDoc.RootElement.GetRawText();
        return (JValue)JToken.Parse(objStr);
    }

    public override void Write(Utf8JsonWriter writer, JValue value, JsonSerializerOptions options)
        => writer.WriteRawValue(Newtonsoft.Json.JsonConvert.SerializeObject(value));
}

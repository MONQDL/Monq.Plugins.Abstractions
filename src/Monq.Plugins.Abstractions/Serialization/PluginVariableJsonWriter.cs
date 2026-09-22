using System.Collections;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Monq.Plugins.Abstractions.Serialization;

internal static class PluginVariableJsonWriter
{
    internal static void Write(Utf8JsonWriter writer, IDictionary<string, object?> variables)
    {
        writer.WriteStartObject();
        foreach (var variable in variables)
        {
            writer.WritePropertyName(variable.Key);
            WriteValue(writer, variable.Value);
        }
        writer.WriteEndObject();
    }

    static void WriteValue(Utf8JsonWriter writer, object? value)
    {
        switch (value)
        {
            case null: writer.WriteNullValue(); break;
            case JsonNode node: node.WriteTo(writer); break;
            case JsonElement element: element.WriteTo(writer); break;
            case string item: writer.WriteStringValue(item); break;
            case bool item: writer.WriteBooleanValue(item); break;
            case byte item: writer.WriteNumberValue(item); break;
            case sbyte item: writer.WriteNumberValue(item); break;
            case short item: writer.WriteNumberValue(item); break;
            case ushort item: writer.WriteNumberValue(item); break;
            case int item: writer.WriteNumberValue(item); break;
            case uint item: writer.WriteNumberValue(item); break;
            case long item: writer.WriteNumberValue(item); break;
            case ulong item: writer.WriteNumberValue(item); break;
            case float item: writer.WriteNumberValue(item); break;
            case double item: writer.WriteNumberValue(item); break;
            case decimal item: writer.WriteNumberValue(item); break;
            case DateTime item: writer.WriteStringValue(item); break;
            case DateTimeOffset item: writer.WriteStringValue(item); break;
            case Guid item: writer.WriteStringValue(item); break;
            case Uri item: writer.WriteStringValue(item.ToString()); break;
            case Enum item: writer.WriteStringValue(item.ToString()); break;
            case IDictionary<string, object?> dictionary: Write(writer, dictionary); break;
            case IEnumerable enumerable: WriteEnumerable(writer, enumerable); break;
            default: throw new JsonException($"Unsupported plugin variable type '{value.GetType()}'.");
        }
    }

    static void WriteEnumerable(Utf8JsonWriter writer, IEnumerable enumerable)
    {
        writer.WriteStartArray();
        foreach (var item in enumerable)
            WriteValue(writer, item);
        writer.WriteEndArray();
    }
}

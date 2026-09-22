using System.Text.Json;

namespace Monq.Plugins.Abstractions.Serialization;

internal static class PluginResultJsonReader
{
    internal static IDictionary<string, object?> Read(JsonElement element)
        => element.ValueKind == JsonValueKind.Object
            ? ReadObject(element)
            : throw new JsonException("Plugin result must be a JSON object.");

    static Dictionary<string, object?> ReadObject(JsonElement element)
    {
        var result = new Dictionary<string, object?>();
        foreach (var property in element.EnumerateObject())
            result.Add(property.Name, ReadValue(property.Value));
        return result;
    }

    static List<object?> ReadArray(JsonElement element)
    {
        var result = new List<object?>(element.GetArrayLength());
        foreach (var item in element.EnumerateArray())
            result.Add(ReadValue(item));
        return result;
    }

    static object? ReadValue(JsonElement element)
        => element.ValueKind switch
        {
            JsonValueKind.Object => ReadObject(element),
            JsonValueKind.Array => ReadArray(element),
            JsonValueKind.String => element.GetString(),
            JsonValueKind.Number when element.TryGetInt32(out var intValue) => intValue,
            JsonValueKind.Number when element.TryGetInt64(out var longValue) => longValue,
            JsonValueKind.Number when element.TryGetDouble(out var doubleValue) => doubleValue,
            JsonValueKind.True => true,
            JsonValueKind.False => false,
            JsonValueKind.Null => null,
            _ => throw new JsonException($"Unsupported JSON element kind '{element.ValueKind}'."),
        };
}

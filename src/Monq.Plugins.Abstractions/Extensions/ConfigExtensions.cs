using Monq.Plugins.Abstractions.Serialization;
using System.Buffers;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace Monq.Plugins.Abstractions.Extensions;

/// <summary>
/// Extension methods for working with plugin configuration.
/// </summary>
public static class ConfigExtensions
{
    /// <summary>
    /// Converts a model to a result dictionary using source-generated JSON metadata.
    /// </summary>
    /// <typeparam name="T">The source model type.</typeparam>
    /// <param name="source">The source model.</param>
    /// <param name="jsonTypeInfo">The source-generated JSON metadata for the model type.</param>
    /// <returns>A dictionary containing the model data.</returns>
    public static IDictionary<string, object?> ToResult<T>(
        this T source,
        JsonTypeInfo<T> jsonTypeInfo)
    {
        var element = JsonSerializer.SerializeToElement(source, jsonTypeInfo);
        return PluginResultJsonReader.Read(element);
    }

    /// <summary>
    /// Converts a variable dictionary to a configuration model using source-generated JSON metadata.
    /// </summary>
    /// <typeparam name="T">The configuration model type.</typeparam>
    /// <param name="vars">The source variable dictionary.</param>
    /// <param name="jsonTypeInfo">The source-generated JSON metadata for the configuration model.</param>
    /// <returns>The populated configuration model.</returns>
    public static T ToConfig<T>(
        this IDictionary<string, object?> vars,
        JsonTypeInfo<T> jsonTypeInfo)
        where T : class, new()
    {
        var buffer = new ArrayBufferWriter<byte>();
        using (var writer = new Utf8JsonWriter(buffer))
        {
            PluginVariableJsonWriter.Write(writer, vars);
        }

        return JsonSerializer.Deserialize(buffer.WrittenSpan, jsonTypeInfo) ?? new();
    }
}

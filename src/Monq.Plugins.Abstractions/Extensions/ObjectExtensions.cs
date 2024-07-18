using Monq.Plugins.Abstractions.Converters;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Monq.Plugins.Abstractions.Extensions;

/// <summary>
/// Методы расширения для работы с объектами.
/// </summary>
public static class ObjectExtensions
{
    static readonly JsonSerializerOptions _serializationOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters = { new JsonStringEnumConverter() },
        PropertyNameCaseInsensitive = true,
    };
    static readonly JsonSerializerOptions _dictOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters = { new DictionaryConverter(), new JsonStringEnumConverter() },
        PropertyNameCaseInsensitive = true,
    };

    /// <summary>
    /// Преобразовать объект в словарь.
    /// </summary>
    /// <param name="source">Исходный объект.</param>
    /// <returns></returns>
    public static IDictionary<string, object?> ToDictionary(this object source)
    {
        var json = JsonSerializer.Serialize(source, _serializationOptions);
        var result = JsonSerializer.Deserialize<IDictionary<string, object?>>(json, _dictOptions)
            ?? new Dictionary<string, object?>();
        return result;
    }

    /// <summary>
    /// Преобразовать словарь в объект.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source">Исходный словарь.</param>
    /// <returns></returns>
    public static T ToObject<T>(this IDictionary<string, object?> source)
        where T : class, new()
    {
        var json = JsonSerializer.Serialize(source, _serializationOptions);
        // TODO: игнорирование ошибок.
        return JsonSerializer.Deserialize<T>(json, _serializationOptions) ?? new();
    }
}

using Monq.Plugins.Abstractions.Converters;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Monq.Plugins.Abstractions.Extensions;

/// <summary>
/// Методы расширения для работы с конфигурацией плагина.
/// </summary>
internal static class ConfigExtensions
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
        Converters = { new SystemTextJsonDictionaryConverter(), new JsonStringEnumConverter() },
        PropertyNameCaseInsensitive = true,
    };

    /// <summary>
    /// Преобразовать объект в словарь.
    /// </summary>
    /// <param name="source">Исходный объект.</param>
    /// <returns></returns>
    public static IDictionary<string, object?> ToResult(this object source)
    {
        var json = JsonSerializer.Serialize(source, _serializationOptions);
        var result = JsonSerializer.Deserialize<IDictionary<string, object?>>(json, _dictOptions)
            ?? new Dictionary<string, object?>();
        return result;
    }

    /// <summary>
    /// Преобразовать словарь переменных в экземпляр класса конфигурации.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="vars">Исходный словарь переменных.</param>
    /// <returns></returns>
    public static T ToConfig<T>(this IDictionary<string, object?> vars)
        where T : class, new()
    {
        var json = JsonSerializer.Serialize(vars, _serializationOptions);
        return JsonSerializer.Deserialize<T>(json, _serializationOptions) ?? new();
    }
}

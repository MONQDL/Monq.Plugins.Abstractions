using Monq.Plugins.Abstractions.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using ErrorEventArgs = Newtonsoft.Json.Serialization.ErrorEventArgs;

namespace Monq.Plugins.Abstractions.Extensions;

/// <summary>
/// Методы расширения для работы с объектами.
/// </summary>
public static class ObjectExtensions
{
    static readonly JsonSerializerSettings _serializerOptions = new()
    {
        NullValueHandling = NullValueHandling.Ignore,
        Converters = { new DictionaryConverter(), new StringEnumConverter() },
        ContractResolver = new CamelCasePropertyNamesContractResolver()
    };

    /// <summary>
    /// Преобразовать объект в словарь.
    /// </summary>
    /// <param name="source">Исходный объект.</param>
    /// <returns></returns>
    [Obsolete("Расширение будет удалено в следующих версиях.")]
    public static IDictionary<string, object?> ToDictionary(this object source)
    {
        var json = JsonConvert.SerializeObject(source, _serializerOptions);
        var result = JsonConvert.DeserializeObject<IDictionary<string, object?>>(json, _serializerOptions)
            ?? new Dictionary<string, object?>();
        return result;
    }

    /// <summary>
    /// Преобразовать словарь в объект.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source">Исходный словарь.</param>
    /// <returns></returns>
    [Obsolete("Расширение будет удалено в следующих версиях.")]
    public static T ToObject<T>(this IDictionary<string, object?> source)
        where T : class, new()
        => source.ToObject<T>(ignoreErrors: true);

    // REM: для плагинных систем стоит перегружать методы определением отдельных сигнатур,
    // а не добавлением необязательных параметров, чтобы не поломать обратную совместимость.
    /// <summary>
    /// Преобразовать словарь в объект.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source">Исходный словарь.</param>
    /// <param name="ignoreErrors">Флаг игнорирования ошибок конвертации.</param>
    /// <returns></returns>
    [Obsolete("Расширение будет удалено в следующих версиях.")]
    public static T ToObject<T>(this IDictionary<string, object?> source, bool ignoreErrors = true)
        where T : class, new()
    {
        var json = JsonConvert.SerializeObject(source, _serializerOptions);

        T Deserialization()
        {
            return JsonConvert.DeserializeObject<T>(json, _serializerOptions) ?? new();
        }
        var result = ignoreErrors
            ? IgnoreErrors(Deserialization)
            : Deserialization();

        return result;
    }

    static T IgnoreErrors<T>(Func<T> operation)
        where T : class, new()
    {
        _serializerOptions.Error += Ignore;
        var result = operation.Invoke();
        _serializerOptions.Error -= Ignore;
        return result;
    }

    static void Ignore(object? sender, ErrorEventArgs args)
        => args.ErrorContext.Handled = true;
}

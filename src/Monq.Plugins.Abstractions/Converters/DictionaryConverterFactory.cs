using System.Text.Json;
using System.Text.Json.Serialization;

namespace Monq.Plugins.Abstractions.Converters;

internal class DictionaryConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
        => typeof(IDictionary<string, object?>).IsAssignableFrom(typeToConvert);

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        // Remove this converter to avoid infinite recursion.
        var newOptions = new JsonSerializerOptions(options);
        for (var i = newOptions.Converters.Count - 1; i >= 0; i--)
            if (newOptions.Converters[i] is DictionaryConverterFactory)
                newOptions.Converters.RemoveAt(i);
        return new SystemTextJsonDictionaryConverter(newOptions);
    }
}

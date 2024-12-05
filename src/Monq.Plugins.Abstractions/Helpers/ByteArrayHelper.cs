using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Monq.Plugins.Abstractions.Helpers;

/// <summary>
/// Вспомогательный класс для работы с массивами байтов.
/// </summary>
public static class ByteArrayHelper
{
    /// <summary>
    /// Проверить, является ли массив байтов валидном json объектом.
    /// </summary>
    /// <param name="bytes">Массив байтов.</param>
    /// <returns>true, если массив представляет собой json объект, иначе - false.</returns>
    /// <remarks>
    /// Более оптимизированный вариант по сравнению
    /// с <see cref="TryParseToJsonObject(byte[], out JsonObject?)"/>,
    /// если не важно получение результирующего объекта.
    /// </remarks>
    public static bool IsValidJsonObject(byte[] bytes)
    {
        try
        {
            using var doc = JsonDocument.Parse(bytes);
            return doc.RootElement.ValueKind == JsonValueKind.Object;
        }
        catch (JsonException)
        {
            return false;
        }
    }

    /// <summary>
    /// Попытаться распарсить массив байтов в json объект.
    /// </summary>
    /// <param name="bytes">Массив байтов.</param>
    /// <param name="jsonObj">Полученный json объект.</param>
    /// <returns>true, если массив представляет собой json объект, иначе - false.</returns>
    public static bool TryParseToJsonObject(byte[] bytes, [NotNullWhen(true)] out JsonObject? jsonObj)
    {
        try
        {
            jsonObj = JsonNode.Parse(bytes)!.AsObject();
            return true;
        }
        catch (Exception)
        {
            jsonObj = null;
            return false;
        }
    }

    /// <summary>
    /// Убрать байты из <paramref name="trimChars"/> с начала и конца массива <paramref name="bytes"/>.
    /// </summary>
    /// <param name="bytes">Исходный массив байтов.</param>
    /// <param name="trimChars">Байты, которые нужно убрать.</param>
    /// <returns>Целевой массив.</returns>
    public static ReadOnlySpan<byte> Trim(ReadOnlySpan<byte> bytes, params byte[] trimChars)
    {
        var start = 0;
        var end = bytes.Length - 1;

        while (start <= end && trimChars.Contains(bytes[start]))
            start++;

        while (end >= start && trimChars.Contains(bytes[end]))
            end--;

        return bytes.Slice(start, end - start + 1);
    }
}

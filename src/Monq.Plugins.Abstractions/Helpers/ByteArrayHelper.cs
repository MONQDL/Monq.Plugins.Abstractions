using System.Text.Json;

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

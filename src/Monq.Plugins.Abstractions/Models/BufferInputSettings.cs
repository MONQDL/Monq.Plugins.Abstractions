namespace Monq.Plugins.Abstractions.Models;

/// <summary>
/// Настройки входа данных для буферизации.
/// </summary>
public class BufferInputSettings
{
    /// <summary>
    /// Название.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Ключ потока данных.
    /// </summary>
    public string StreamKey { get; init; }

    /// <summary>
    /// Тип буфера.
    /// </summary>
    public string BufferType { get; init; }

    /// <summary>
    /// Формат данных.
    /// </summary>
    public string Format { get; init; }

    /// <summary>
    /// Размер чанка в байтах.
    /// </summary>
    public int ChunkSize { get; init; }

    /// <summary>
    /// Разделитель записей.
    /// </summary>
    public byte[] Separator { get; init; } = Array.Empty<byte>();

    /// <summary>
    /// Функция парсинга.
    /// </summary>
    public Func<byte[], Task<byte[]>>? HandleRecord { get; init; }

    /// <summary>
    /// Конструктор источника данных для буферизации.
    /// </summary>
    public BufferInputSettings(string name, string streamKey, string bufferType, string format)
    {
        Name = name;
        StreamKey = streamKey;
        BufferType = bufferType;
        Format = format;
    }
}

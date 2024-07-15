namespace Monq.Plugins.Abstractions.Models;

/// <summary>
/// Настройки источника данных для буферизации.
/// </summary>
public class BufferSourceSettings
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
    /// Дата получения последней записи.
    /// </summary>
    public DateTimeOffset LastRecordDate { get; private set; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// Функция парсинга.
    /// </summary>
    public Func<byte[], Task<byte[]>>? HandleRecord { get; init; }

    /// <summary>
    /// Обновить дату получения последней записи.
    /// </summary>
    /// <param name="lastRecordDate">Дата последней записи.</param>
    public void UpdateLastRecordDate(DateTimeOffset lastRecordDate)
        => LastRecordDate = lastRecordDate;

    /// <summary>
    /// Конструктор источника данных для буферизации.
    /// </summary>
    public BufferSourceSettings(string name, string streamKey, string bufferType, string format)
    {
        Name = name;
        StreamKey = streamKey;
        BufferType = bufferType;
        Format = format;
    }
}

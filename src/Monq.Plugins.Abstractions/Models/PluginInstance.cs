namespace Monq.Plugins.Abstractions.Models;

/// <summary>
/// Экземпляр плагина.
/// </summary>
public class PluginInstance
{
    /// <summary>
    /// Название.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Ключ потока данных.
    /// </summary>
    public string StreamKey { get; init; } = string.Empty;

    /// <summary>
    /// Размер чанка в байтах.
    /// </summary>
    public int ChunkSize { get; init; }

    /// <summary>
    /// Тип буфера.
    /// </summary>
    public BufferTypes BufferType { get; init; } = BufferTypes.Memory;

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
}

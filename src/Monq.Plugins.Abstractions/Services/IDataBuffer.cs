using Monq.Plugins.Abstractions.Models;

namespace Monq.Plugins.Abstractions.Services;

/// <summary>
/// Интерфейс буфера данных.
/// </summary>
public interface IDataBuffer
{
    /// <summary>
    /// Записать данные.
    /// </summary>
    /// <param name="pluginInstance">Название экземпляра плагина.</param>
    /// <param name="streamKey">Ключ потока данных.</param>
    /// <param name="data">Массив байтов.</param>
    /// <param name="chunkSize">Размер чанка в байтах.</param>
    /// <param name="bufferType">Тип буфера.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns><see cref="Task"/>, показывающий завершение операции.</returns>
    Task Write(
        string pluginInstance,
        string streamKey,
        byte[] data,
        int chunkSize,
        BufferTypes bufferType = BufferTypes.Memory,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Записать данные с разделитилем.
    /// </summary>
    /// <param name="pluginInstance">Название экземпляра плагина.</param>
    /// <param name="streamKey">Ключ потока данных.</param>
    /// <param name="data">Массив байтов.</param>
    /// <param name="chunkSize">Размер чанка в байтах.</param>
    /// <param name="delimiter">Массив байтов разделителя.</param>
    /// <param name="bufferType">Тип буфера.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns><see cref="Task"/>, показывающий завершение операции.</returns>
    Task WriteWithDelimiter(
        string pluginInstance,
        string streamKey,
        byte[] data,
        int chunkSize,
        byte[] delimiter,
        BufferTypes bufferType = BufferTypes.Memory,
        CancellationToken cancellationToken = default);
}

using Monq.Plugins.Abstractions.Models;

namespace Monq.Plugins.Abstractions.Services;

/// <summary>
/// Интерфейс буфера данных.
/// </summary>
public interface IDataBuffer
{
    /// <summary>
    /// Поддерживаемые типы буфера.
    /// </summary>
    IList<string> BufferTypes { get; }

    /// <summary>
    /// Поддерживаемые форматы буфера.
    /// </summary>
    IList<string> Formats { get; }

    /// <summary>
    /// Записать данные.
    /// </summary>
    /// <param name="bufferSource">Источник данных для буферизации.</param>
    /// <param name="data">Массив байтов.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns><see cref="Task"/>, показывающий завершение операции.</returns>
    Task Write(
        BufferSource bufferSource,
        byte[] data,
        CancellationToken cancellationToken = default);
}

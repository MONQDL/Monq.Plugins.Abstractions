using Monq.Plugins.Abstractions.Models;

namespace Monq.Plugins.Abstractions.Services;

/// <summary>
/// Абстрактный класс источника данных для буферизации.
/// </summary>
public abstract class BufferSource : IDisposable
{
    readonly DataBuffer _dataBuffer;

    /// <summary>
    /// Настройки источника данных.
    /// </summary>
    public BufferSourceSettings Settings { get; }

    /// <summary>
    /// Конструктор абстрактного класс источника данных для буферизации.
    /// </summary>
    public BufferSource(
        BufferSourceSettings settings,
        DataBuffer dataBuffer)
    {
        Settings = settings;
        _dataBuffer = dataBuffer;
        _dataBuffer.AddSource(this);
    }

    /// <summary>
    /// Записать данные.
    /// </summary>
    /// <param name="data">Массив байтов.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns><see cref="Task"/>, показывающий завершение операции.</returns>
    public abstract Task Write(
        byte[] data,
        CancellationToken cancellationToken = default);

    /// <inheritdoc/>
    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _dataBuffer.RemoveSource(this);
    }
}

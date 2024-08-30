using Monq.Plugins.Abstractions.Models;

namespace Monq.Plugins.Abstractions.Services;

/// <summary>
/// Абстрактный класс входа данных для буферизации.
/// </summary>
public abstract class BufferInput : IDisposable
{
    readonly DataBuffer _dataBuffer;

    /// <summary>
    /// Настройки входа данных.
    /// </summary>
    public BufferInputSettings Settings { get; }

    /// <summary>
    /// Конструктор абстрактного класс входа данных для буферизации.
    /// </summary>
    public BufferInput(
        BufferInputSettings settings,
        DataBuffer dataBuffer)
    {
        Settings = settings;
        _dataBuffer = dataBuffer;
        _dataBuffer.AddInput(this);
    }

    /// <summary>
    /// Записать данные.
    /// </summary>
    /// <param name="data">Данные в байтах.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns><see cref="Task"/>, показывающий завершение операции.</returns>
    public abstract Task Write(
        byte[] data,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Записать данные.
    /// </summary>
    /// <param name="data">Данные в байтах.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns><see cref="Task"/>, показывающий завершение операции.</returns>
    public abstract Task Write(
        ReadOnlyMemory<byte> data,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Принудительно сбросить буфер.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns><see cref="Task"/>, показывающий завершение операции.</returns>
    protected Task ForceFlush(CancellationToken cancellationToken = default)
        => _dataBuffer.Flush(cancellationToken);

    /// <inheritdoc/>
    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _dataBuffer.RemoveInput(this);
    }
}

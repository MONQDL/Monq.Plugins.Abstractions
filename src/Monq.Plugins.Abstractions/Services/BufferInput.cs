using Monq.Plugins.Abstractions.Models;

namespace Monq.Plugins.Abstractions.Services;

/// <summary>
/// Buffered data input abstract class.
/// </summary>
public abstract class BufferInput : IDisposable
{
    readonly DataBuffer _dataBuffer;
    int _disposed;

    /// <summary>
    /// Input settings.
    /// </summary>
    public BufferInputSettings Settings { get; }

    /// <summary>
    /// Buffered data input abstract class constructor.
    /// </summary>
    /// <param name="settings">The input settings.</param>
    /// <param name="dataBuffer">The data buffer that owns this input.</param>
    protected BufferInput(
        BufferInputSettings settings,
        DataBuffer dataBuffer)
    {
        Settings = settings;
        _dataBuffer = dataBuffer;
        _dataBuffer.AddInput(this);
    }

    /// <summary>
    /// Writes an arbitrary block of input data.
    /// </summary>
    /// <param name="data">The input data.</param>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public abstract Task Write(
        byte[] data,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Writes an arbitrary block of input data.
    /// </summary>
    /// <param name="data">The input data.</param>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public abstract Task Write(
        ReadOnlyMemory<byte> data,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Writes one complete input record.
    /// </summary>
    /// <param name="data">The complete record data.</param>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public virtual Task WriteRecord(
        ReadOnlyMemory<byte> data,
        CancellationToken cancellationToken = default)
        => Write(data, cancellationToken);

    /// <summary>
    /// Flushes the data buffer immediately.
    /// </summary>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    protected Task ForceFlush(CancellationToken cancellationToken = default)
        => _dataBuffer.Flush(cancellationToken);

    /// <inheritdoc/>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Releases the resources used by the input.
    /// </summary>
    /// <param name="disposing">
    /// A value indicating whether managed resources should be released.
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposing || Interlocked.Exchange(ref _disposed, 1) != 0)
            return;

        _dataBuffer.RemoveInput(this);
    }
}

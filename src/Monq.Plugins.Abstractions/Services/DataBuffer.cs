using Monq.Plugins.Abstractions.Models;
using System.Collections.Concurrent;

namespace Monq.Plugins.Abstractions.Services;

/// <summary>
/// Data buffer abstract class.
/// </summary>
public abstract class DataBuffer
{
    /// <summary>
    /// Initialized buffer inputs.
    /// </summary>
    protected ConcurrentDictionary<string, BufferInput> BufferInputs { get; } = new();

    /// <summary>
    /// Supported buffer types.
    /// </summary>
    protected abstract IEnumerable<string> BufferTypes { get; }

    /// <summary>
    /// Supported buffer formats.
    /// </summary>
    protected abstract IEnumerable<string> Formats { get; }

    /// <summary>
    /// Initializes a buffered data input.
    /// </summary>
    /// <param name="settings">The input settings.</param>
    /// <returns>The initialized buffer input.</returns>
    public abstract BufferInput InitInput(BufferInputSettings settings);

    /// <summary>
    /// Flushes buffered data.
    /// </summary>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    protected internal abstract Task Flush(CancellationToken cancellationToken);

    /// <summary>
    /// Registers a buffer input.
    /// </summary>
    /// <param name="bufferInput">The buffer input to register.</param>
    /// <exception cref="ArgumentException">
    /// The input settings are unsupported or an input with the same name is already registered.
    /// </exception>
    protected internal virtual void AddInput(BufferInput bufferInput)
    {
        if (!BufferTypes.Contains(bufferInput.Settings.BufferType))
            throw new ArgumentException($"Buffer input type {bufferInput.Settings.BufferType} is not supported.");
        if (!Formats.Contains(bufferInput.Settings.Format))
            throw new ArgumentException($"Buffer input format {bufferInput.Settings.Format} is not supported.");
        if (!BufferInputs.TryAdd(bufferInput.Settings.Name, bufferInput))
            throw new ArgumentException($"Buffer input with name {bufferInput.Settings.Name} already exists.");
    }

    /// <summary>
    /// Removes a registered buffer input.
    /// </summary>
    /// <param name="bufferInput">The buffer input to remove.</param>
    protected internal virtual void RemoveInput(BufferInput bufferInput)
        => _ = BufferInputs.TryRemove(bufferInput.Settings.Name, out _);
}

using Monq.Plugins.Abstractions.Models;
using System.Collections.Concurrent;

namespace Monq.Plugins.Abstractions.Services;

/// <summary>
/// Абстрактный класс буфера данных.
/// </summary>
public abstract class DataBuffer
{
    /// <summary>
    /// Инициализированные входы данных.
    /// </summary>
    protected ConcurrentDictionary<string, BufferInput> BufferInputs { get; } = new();

    /// <summary>
    /// Поддерживаемые типы буфера.
    /// </summary>
    protected abstract IEnumerable<string> BufferTypes { get; }

    /// <summary>
    /// Поддерживаемые форматы буфера.
    /// </summary>
    protected abstract IEnumerable<string> Formats { get; }

    /// <summary>
    /// Инициализировать вход данных.
    /// </summary>
    /// <param name="settings">Настройки входа данных.</param>
    /// <returns>Вход данных.</returns>
    public abstract BufferInput InitInput(BufferInputSettings settings);

    internal void AddInput(BufferInput bufferInput)
    {
        if (BufferInputs.ContainsKey(bufferInput.Settings.Name))
            throw new ArgumentException($"Buffer source with name {bufferInput.Settings.Name} already exists.");
        if (!BufferTypes.Contains(bufferInput.Settings.BufferType))
            throw new ArgumentException($"Buffer source type {bufferInput.Settings.BufferType} is not supported.");
        if (!Formats.Contains(bufferInput.Settings.Format))
            throw new ArgumentException($"Buffer source format {bufferInput.Settings.Format} is not supported.");
        _ = BufferInputs.TryAdd(bufferInput.Settings.Name, bufferInput);
    }

    internal void RemoveInput(BufferInput bufferInput)
        => _ = BufferInputs.TryRemove(bufferInput.Settings.Name, out _);
}

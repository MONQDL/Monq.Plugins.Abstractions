using Monq.Plugins.Abstractions.Models;
using System.Collections.Concurrent;

namespace Monq.Plugins.Abstractions.Services;

/// <summary>
/// Абстрактный класс буфера данных.
/// </summary>
public abstract class DataBuffer
{
    readonly ConcurrentDictionary<string, BufferSource> _bufferSources = new();

    /// <summary>
    /// Поддерживаемые типы буфера.
    /// </summary>
    protected abstract IEnumerable<string> BufferTypes { get; }

    /// <summary>
    /// Поддерживаемые форматы буфера.
    /// </summary>
    protected abstract IEnumerable<string> Formats { get; }

    /// <summary>
    /// Инициализировать источник данных.
    /// </summary>
    /// <param name="settings">Настройки источника данных.</param>
    /// <returns>Источник данных.</returns>
    public abstract BufferSource InitSource(BufferSourceSettings settings);

    internal void AddSource(BufferSource bufferSource)
    {
        if (_bufferSources.ContainsKey(bufferSource.Settings.Name))
            throw new ArgumentException($"Buffer source with name {bufferSource.Settings.Name} already exists.");
        if (!BufferTypes.Contains(bufferSource.Settings.BufferType))
            throw new ArgumentException($"Buffer source type {bufferSource.Settings.BufferType} is not supported.");
        if (!Formats.Contains(bufferSource.Settings.Format))
            throw new ArgumentException($"Buffer source format {bufferSource.Settings.Format} is not supported.");
    }

    internal void RemoveSource(BufferSource bufferSource)
        => _ = _bufferSources.TryRemove(bufferSource.Settings.Name, out _);
}

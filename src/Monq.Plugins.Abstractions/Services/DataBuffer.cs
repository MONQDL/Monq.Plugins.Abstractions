﻿using Monq.Plugins.Abstractions.Models;
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

    /// <summary>
    /// Сбросить буфер.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns><see cref="Task"/>, показывающий завершение операции.</returns>
    protected internal abstract Task Flush(CancellationToken cancellationToken);

    /// <summary>
    /// Добавить вход данных.
    /// </summary>
    /// <param name="bufferInput">Вход данных.</param>
    /// <exception cref="ArgumentException"></exception>
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
    /// Удалить вход данных.
    /// </summary>
    /// <param name="bufferInput">Вход данных.</param>
    protected internal virtual void RemoveInput(BufferInput bufferInput)
        => _ = BufferInputs.TryRemove(bufferInput.Settings.Name, out _);
}

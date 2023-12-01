namespace Monq.Plugins.Abstractions;

/// <summary>
/// Интерфейс обработчика результата задания после его выполнения.
/// </summary>
public interface IPluginTaskPostProcessor
{
    /// <summary>
    /// Выполнить обработку задания после его выполнения.
    /// </summary>
    /// <param name="outputs">Выходные данные задания.</param>
    /// <returns><see cref="Task"/>, показывающий выполнение операции.</returns>
    Task Process(IDictionary<string, object?> outputs);
}

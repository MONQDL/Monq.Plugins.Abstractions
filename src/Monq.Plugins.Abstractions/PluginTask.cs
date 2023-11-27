namespace Monq.Plugins.Abstractions.Models;

/// <summary>
/// Задание плагина.
/// </summary>
public class PluginTask
{
    /// <summary>
    /// Название задания.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Команда задания.
    /// </summary>
    public string Command { get; }

    /// <summary>
    /// Тип стратегии выполнения задания.
    /// </summary>
    public Type ProcessorStrategyType { get; }

    /// <summary>
    /// Тип стратегии пост-обработки задания.
    /// </summary>
    public Type? PostProcessorStrategyType { get; init; }

    /// <summary>
    /// Конструктор задания.
    /// </summary>
    /// <param name="name">Название задания.</param>
    /// <param name="command">Команда задания.</param>
    /// <param name="processorStrategyType">Тип стратегии выполнения задания.</param>
    public PluginTask(string name, string command, Type processorStrategyType)
        => (Name, Command, ProcessorStrategyType) = (name, command, processorStrategyType);
}

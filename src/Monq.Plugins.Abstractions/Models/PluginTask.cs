namespace Monq.Plugins.Abstractions.Models;

/// <summary>
/// Plugin task.
/// </summary>
public class PluginTask
{
    /// <summary>
    /// Task name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Task command.
    /// </summary>
    public string Command { get; }

    /// <summary>
    /// Task execution strategy type.
    /// </summary>
    public Type ProcessorStrategyType { get; }

    /// <summary>
    /// Plugin task constructor.
    /// </summary>
    /// <param name="name">The display name of the task.</param>
    /// <param name="command">The command used to select the task.</param>
    /// <param name="processorStrategyType">The task execution strategy type.</param>
    public PluginTask(string name, string command, Type processorStrategyType)
        => (Name, Command, ProcessorStrategyType) = (name, command, processorStrategyType);
}

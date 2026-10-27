using System.Text.Json.Nodes;

namespace Monq.Plugins.Abstractions.Models;

/// <summary>
/// Plugin task execution context.
/// </summary>
public sealed class PluginTaskContext
{
    readonly IPluginTaskOutputWriter _outputWriter;

    /// <summary>
    /// Plugin task variables.
    /// </summary>
    public JsonObject Variables { get; }

    /// <summary>
    /// Agent system variables.
    /// </summary>
    public JsonObject SystemVariables { get; }

    /// <summary>
    /// Names of secured variables.
    /// </summary>
    public IReadOnlySet<string> SecuredVariables { get; }

    /// <summary>
    /// Plugin task execution context constructor.
    /// </summary>
    /// <param name="variables">The plugin task variables.</param>
    /// <param name="systemVariables">The agent system variables.</param>
    /// <param name="securedVariables">The names of secured variables.</param>
    /// <param name="outputWriter">The task output writer.</param>
    public PluginTaskContext(
        JsonObject variables,
        JsonObject systemVariables,
        IReadOnlySet<string> securedVariables,
        IPluginTaskOutputWriter outputWriter)
    {
        Variables = variables;
        SystemVariables = systemVariables;
        SecuredVariables = securedVariables;
        _outputWriter = outputWriter;
    }

    /// <summary>
    /// Writes an intermediate or streaming plugin task output.
    /// </summary>
    /// <param name="output">The task output.</param>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task WriteOutput(
        JsonObject output,
        CancellationToken cancellationToken = default)
        => _outputWriter.Write(output, cancellationToken);
}

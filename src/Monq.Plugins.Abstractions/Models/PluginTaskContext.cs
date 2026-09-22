using System.Text.Json.Nodes;

namespace Monq.Plugins.Abstractions.Models;

/// <summary>
/// Plugin task execution context.
/// </summary>
public sealed class PluginTaskContext
{
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
    public PluginTaskContext(
        JsonObject variables,
        JsonObject systemVariables,
        IReadOnlySet<string> securedVariables)
    {
        Variables = variables;
        SystemVariables = systemVariables;
        SecuredVariables = securedVariables;
    }
}

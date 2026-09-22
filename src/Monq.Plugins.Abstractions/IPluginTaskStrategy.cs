namespace Monq.Plugins.Abstractions;

using System.Text.Json.Nodes;
using Monq.Plugins.Abstractions.Models;

/// <summary>
/// Plugin task execution strategy.
/// </summary>
public interface IPluginTaskStrategy
{
    /// <summary>
    /// Executes the plugin task.
    /// </summary>
    /// <param name="context">The plugin task execution context.</param>
    /// <param name="cancellationToken">The token used to cancel task execution.</param>
    /// <returns>The task output variables.</returns>
    Task<JsonObject> Run(
        PluginTaskContext context,
        CancellationToken cancellationToken);
}

namespace Monq.Plugins.Abstractions;

using Monq.Plugins.Abstractions.Models;
using System.Text.Json.Nodes;

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
    /// <returns>The final task result.</returns>
    Task<JsonObject> Run(
        PluginTaskContext context,
        CancellationToken cancellationToken);
}

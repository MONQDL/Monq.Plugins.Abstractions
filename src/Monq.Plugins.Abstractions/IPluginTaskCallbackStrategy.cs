namespace Monq.Plugins.Abstractions;

using System.Text.Json.Nodes;
using Monq.Plugins.Abstractions.Models;

/// <summary>
/// Plugin task execution strategy with a callback function.
/// </summary>
public interface IPluginTaskCallbackStrategy
{
    /// <summary>
    /// Executes the plugin task.
    /// </summary>
    /// <param name="context">The plugin task execution context.</param>
    /// <param name="callback">The callback that processes an intermediate record.</param>
    /// <param name="cancellationToken">The token used to cancel task execution.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task Run(
        PluginTaskContext context,
        Func<JsonObject, Task> callback,
        CancellationToken cancellationToken);
}

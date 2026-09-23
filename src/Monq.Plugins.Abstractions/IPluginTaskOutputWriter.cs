using System.Text.Json.Nodes;

namespace Monq.Plugins.Abstractions;

/// <summary>
/// Plugin task output writer.
/// </summary>
public interface IPluginTaskOutputWriter
{
    /// <summary>
    /// Writes an intermediate or streaming plugin task output.
    /// </summary>
    /// <param name="output">The task output.</param>
    /// <param name="cancellationToken">The token used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task Write(
        JsonObject output,
        CancellationToken cancellationToken = default);
}

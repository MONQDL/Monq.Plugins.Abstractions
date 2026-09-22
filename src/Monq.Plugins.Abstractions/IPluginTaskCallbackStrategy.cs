namespace Monq.Plugins.Abstractions;

/// <summary>
/// Plugin task execution strategy with a callback function.
/// </summary>
public interface IPluginTaskCallbackStrategy
{
    /// <summary>
    /// Executes the plugin task.
    /// </summary>
    /// <param name="variables">The plugin task variables.</param>
    /// <param name="securedVariables">The names of secured variables.</param>
    /// <param name="callback">The callback that processes an intermediate task result.</param>
    /// <param name="cancellationToken">The token used to cancel task execution.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task Run(
        IDictionary<string, object?> variables,
        IEnumerable<string> securedVariables,
        Func<IDictionary<string, object?>, Task<IDictionary<string, object?>>> callback,
        CancellationToken cancellationToken);
}

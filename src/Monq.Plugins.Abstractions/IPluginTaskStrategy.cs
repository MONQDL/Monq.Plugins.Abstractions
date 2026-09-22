namespace Monq.Plugins.Abstractions;

/// <summary>
/// Plugin task execution strategy.
/// </summary>
public interface IPluginTaskStrategy
{
    /// <summary>
    /// Executes the plugin task.
    /// </summary>
    /// <param name="variables">The plugin task variables.</param>
    /// <param name="securedVariables">The names of secured variables.</param>
    /// <param name="cancellationToken">The token used to cancel task execution.</param>
    /// <returns>The task output variables.</returns>
    Task<IDictionary<string, object?>> Run(
        IDictionary<string, object?> variables,
        IEnumerable<string> securedVariables,
        CancellationToken cancellationToken);
}

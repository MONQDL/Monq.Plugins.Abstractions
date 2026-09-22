namespace Monq.Plugins.Abstractions.Exceptions;

/// <summary>
/// The exception thrown when a plugin task does not contain all required settings.
/// </summary>
public class PluginNotConfiguredException : Exception
{
    const string DefaultMessage = "Plugin is not configured.";

    /// <summary>
    /// Initializes a new instance of the <see cref="PluginNotConfiguredException"/> class.
    /// </summary>
    /// <param name="details">Optional details that describe the missing or invalid setting.</param>
    public PluginNotConfiguredException(string? details = null)
        : base($"{DefaultMessage}{(string.IsNullOrEmpty(details) ? string.Empty : $" Details: {details}")}")
    {
    }
}

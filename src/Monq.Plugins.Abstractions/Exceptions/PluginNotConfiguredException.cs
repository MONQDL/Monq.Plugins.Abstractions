namespace Monq.Plugins.Abstractions.Exceptions;

#pragma warning disable CS1591 // Отсутствует комментарий XML для открытого видимого типа или члена
public class PluginNotConfiguredException : Exception
{
    const string DefaultMessage = "Plugin is not configured.";

    public PluginNotConfiguredException(string? details = null)
        : base($"{DefaultMessage}{(string.IsNullOrEmpty(details) ? string.Empty : $" Details: {details}")}")
    {
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Monq.Plugins.Abstractions.Models;

namespace Monq.Plugins.Abstractions;

/// <summary>
/// Plugin task bootstrap interface.
/// </summary>
public interface IPluginTaskBootstrap
{
    /// <summary>
    /// Registers the services required to execute the plugin task.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The application configuration.</param>
    void RegisterServiceProvider(IServiceCollection services, IConfiguration configuration);

    /// <summary>
    /// Plugin task descriptor.
    /// </summary>
    PluginTask PluginTask { get; }
}

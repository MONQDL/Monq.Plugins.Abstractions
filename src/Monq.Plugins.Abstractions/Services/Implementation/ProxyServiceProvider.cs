using Microsoft.Extensions.DependencyInjection;

namespace Monq.Plugins.Abstractions.Services.Implementation;

/// <summary>
/// Реализация интерфейса прокси-провайдера сервисов.
/// </summary>
public class ProxyServiceProvider : IServiceProvider, IProxyServiceProvider
{
    readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Конструктор реализации интерфейса прокси-провайдера сервисов.
    /// </summary>
    public ProxyServiceProvider(
        IServiceCollection serviceCollection)
    {
        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    /// <inheritdoc/>
    public object? GetService(Type serviceType)
        => _serviceProvider.GetService(serviceType);
}

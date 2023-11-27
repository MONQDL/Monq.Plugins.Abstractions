using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Monq.Plugins.Abstractions.Models;

namespace Monq.Plugins.Abstractions;

/// <summary>
/// Интерфейс загрузчика задания.
/// </summary>
public interface IPluginTaskBootstrap
{
    /// <summary>
    /// Выполнить регистрацию методов работы с заданием.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="configuration">Конфигурация приложения.</param>
    void RegisterServiceProvider(IServiceCollection services, IConfiguration configuration);

    /// <summary>
    /// Задание плагина.
    /// </summary>
    PluginTask PluginTask { get; }
}

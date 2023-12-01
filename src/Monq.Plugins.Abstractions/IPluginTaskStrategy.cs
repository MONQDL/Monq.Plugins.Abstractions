namespace Monq.Plugins.Abstractions;

/// <summary>
/// Стратегия выполнения задания плагина.
/// </summary>
public interface IPluginTaskStrategy
{
    /// <summary>
    /// Выполнить задание для плагина.
    /// </summary>
    /// <param name="variables">Переменные задания плагина.</param>
    /// <param name="securedVariables">Список защищённых переменных.</param>
    /// <param name="cancellationToken">Токен для отмены выполнения задания.</param>
    /// <returns>Выходные данные задания.</returns>
    Task<IDictionary<string, object?>> Run(IDictionary<string, object?> variables, IEnumerable<string> securedVariables, CancellationToken cancellationToken);
}

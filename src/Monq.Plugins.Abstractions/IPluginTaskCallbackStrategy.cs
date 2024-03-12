namespace Monq.Plugins.Abstractions;

/// <summary>
/// Стратегия выполнения задания плагина с функцией обратного вызова.
/// </summary>
public interface IPluginTaskCallbackStrategy
{
    /// <summary>
    /// Выполнить задание.
    /// </summary>
    /// <param name="variables">Переменные задания плагина.</param>
    /// <param name="securedVariables">Список защищённых переменных.</param>
    /// <param name="callback">Функция обратного вызова для обработки результата выполнения задания.</param>
    /// <param name="cancellationToken">Токен для отмены выполнения задания.</param>
    /// <returns><see cref="Task"/>, показывающий выполнение операции.</returns>
    Task Run(
        IDictionary<string, object?> variables,
        IEnumerable<string> securedVariables,
        Func<IDictionary<string, object?>, Task> callback,
        CancellationToken cancellationToken);
}

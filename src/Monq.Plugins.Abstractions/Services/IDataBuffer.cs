namespace Monq.Plugins.Abstractions.Services;

/// <summary>
/// Интерфейс буфера данных.
/// </summary>
public interface IDataBuffer
{
    /// <summary>
    /// Записать данные.
    /// </summary>
    /// <param name="pluginInstance">Название экземпляра плагина.</param>
    /// <param name="data">Массив байтов.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns><see cref="Task"/>, показывающий завершение операции.</returns>
    Task Write(string pluginInstance, byte[] data, CancellationToken cancellationToken = default);
}

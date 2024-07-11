namespace Monq.Plugins.Abstractions.Models;

/// <summary>
/// Тип буфера.
/// </summary>
public enum BufferType
{
    /// <summary>
    /// В оперативной памяти.
    /// </summary>
    Memory = 0,

    /// <summary>
    /// В файловой системе.
    /// </summary>
    Filesystem = 1,
}

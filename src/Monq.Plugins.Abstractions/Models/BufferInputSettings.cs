namespace Monq.Plugins.Abstractions.Models;

/// <summary>
/// Buffered data input settings.
/// </summary>
public class BufferInputSettings
{
    /// <summary>
    /// Input name.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Data stream key.
    /// </summary>
    public string StreamKey { get; init; }

    /// <summary>
    /// Buffer type.
    /// </summary>
    public string BufferType { get; init; }

    /// <summary>
    /// Data format.
    /// </summary>
    public string Format { get; init; }

    /// <summary>
    /// Chunk size in bytes.
    /// </summary>
    public int ChunkSize { get; init; }

    /// <summary>
    /// Record separator.
    /// </summary>
    public byte[] Separator { get; init; } = [];

    /// <summary>
    /// Record handler.
    /// </summary>
    public Func<byte[], Task>? HandleRecord { get; init; }

    /// <summary>
    /// Buffered data input settings constructor.
    /// </summary>
    /// <param name="name">The unique input name.</param>
    /// <param name="streamKey">The destination stream key.</param>
    /// <param name="bufferType">The buffer storage type.</param>
    /// <param name="format">The input data format.</param>
    public BufferInputSettings(string name, string streamKey, string bufferType, string format)
    {
        Name = name;
        StreamKey = streamKey;
        BufferType = bufferType;
        Format = format;
    }
}

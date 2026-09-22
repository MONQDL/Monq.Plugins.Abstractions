using Monq.Plugins.Abstractions.Models;
using Monq.Plugins.Abstractions.Services;
using System.Text.Json.Nodes;
using Xunit;

namespace Monq.Plugins.Abstractions.Tests;

public class PluginTests
{
    [Fact(DisplayName = "Plugin task context exposes variables and secured names.")]
    public void PluginTaskContextShouldExposeInputData()
    {
        var variables = new JsonObject
        {
            ["secret"] = "value",
        };
        var systemVariables = new JsonObject
        {
            ["agentName"] = "agent-1",
        };
        IReadOnlySet<string> securedVariables = new HashSet<string> { "secret" };

        var context = new PluginTaskContext(variables, systemVariables, securedVariables);

        Assert.Same(variables, context.Variables);
        Assert.Same(systemVariables, context.SystemVariables);
        Assert.Same(securedVariables, context.SecuredVariables);
        Assert.Null(context.Variables["agentName"]);
        Assert.Equal("agent-1", context.SystemVariables["agentName"]?.GetValue<string>());
    }

    [Fact(DisplayName = "WriteRecord delegates to Write by default.")]
    public async Task WriteRecordShouldDelegateToWriteByDefault()
    {
        var buffer = new TestDataBuffer();
        using var input = Assert.IsType<TestBufferInput>(buffer.InitInput(CreateSettings()));
        var record = new byte[] { 1, 2, 3 };

        await input.WriteRecord(record);

        Assert.Equal(record, input.LastWrite.ToArray());
    }

    [Fact(DisplayName = "Disposed input is removed from its data buffer.")]
    public void DisposeShouldRemoveInputFromDataBuffer()
    {
        var buffer = new TestDataBuffer();
        var input = buffer.InitInput(CreateSettings());

        input.Dispose();
        using var replacement = buffer.InitInput(CreateSettings());

        Assert.NotNull(replacement);
    }

    static BufferInputSettings CreateSettings()
        => new("input", "stream", "memory", "json")
        {
            HandleRecord = _ => Task.CompletedTask,
        };

    sealed class TestDataBuffer : DataBuffer
    {
        protected override IEnumerable<string> BufferTypes => ["memory"];

        protected override IEnumerable<string> Formats => ["json"];

        public override BufferInput InitInput(BufferInputSettings settings)
            => new TestBufferInput(settings, this);

        protected override Task Flush(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }

    sealed class TestBufferInput : BufferInput
    {
        public ReadOnlyMemory<byte> LastWrite { get; private set; }

        public TestBufferInput(BufferInputSettings settings, DataBuffer dataBuffer)
            : base(settings, dataBuffer)
        {
        }

        public override Task Write(byte[] data, CancellationToken cancellationToken = default)
            => Write(data.AsMemory(), cancellationToken);

        public override Task Write(
            ReadOnlyMemory<byte> data,
            CancellationToken cancellationToken = default)
        {
            LastWrite = data.ToArray();
            return Task.CompletedTask;
        }
    }
}

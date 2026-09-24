using Monq.Plugins.Abstractions.Models;
using Monq.Plugins.Abstractions.Services;
using System.Text.Json.Nodes;
using Xunit;

namespace Monq.Plugins.Abstractions.Tests;

public class PluginTests
{
    [Fact(DisplayName = "Plugin task context exposes input data and writes outputs.")]
    public async Task PluginTaskContextShouldExposeInputDataAndWriteOutputs()
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
        var outputWriter = new TestPluginTaskOutputWriter();

        var context = new PluginTaskContext(variables, systemVariables, securedVariables, outputWriter);
        var result = new JsonObject
        {
            ["result"] = "completed",
        };
        using var cancellationTokenSource = new CancellationTokenSource();

        await context.WriteOutput(result, cancellationTokenSource.Token);

        Assert.Same(variables, context.Variables);
        Assert.Same(systemVariables, context.SystemVariables);
        Assert.Same(securedVariables, context.SecuredVariables);
        Assert.Same(result, outputWriter.Result);
        Assert.Equal(cancellationTokenSource.Token, outputWriter.CancellationToken);
        Assert.Null(context.Variables["agentName"]);
        Assert.Equal("agent-1", context.SystemVariables["agentName"]?.GetValue<string>());
    }

    sealed class TestPluginTaskOutputWriter : IPluginTaskOutputWriter
    {
        public JsonObject? Result { get; private set; }

        public CancellationToken CancellationToken { get; private set; }

        public Task Write(JsonObject output, CancellationToken cancellationToken = default)
        {
            Result = output;
            CancellationToken = cancellationToken;
            return Task.CompletedTask;
        }
    }
}

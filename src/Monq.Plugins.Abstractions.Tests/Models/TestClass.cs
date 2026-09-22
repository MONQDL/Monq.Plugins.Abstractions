namespace Monq.Plugins.Abstractions.Tests.Models;

internal class TestClass
{
    public int A { get; set; }

    public string B { get; set; } = string.Empty;

    public InnerTestClass C { get; set; } = new();

    public string D { get; set; } = string.Empty;
}

internal class InnerTestClass
{
    public int[] CA { get; set; } = [];

    public bool CB { get; set; }
}

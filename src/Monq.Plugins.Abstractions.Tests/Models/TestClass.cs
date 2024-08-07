namespace Monq.Plugins.Abstractions.Tests.Models;

internal class TestClass
{
    public int A { get; set; }

    public string B { get; set; }

    public InnerTestClass C { get; set; }
}

internal class InnerTestClass
{
    public int[] CA { get; set; }

    public bool CB { get; set; }
}

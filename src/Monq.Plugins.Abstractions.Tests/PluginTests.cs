using Monq.Plugins.Abstractions.Extensions;
using Monq.Plugins.Abstractions.Tests.JsonSerializerContexts;
using Monq.Plugins.Abstractions.Tests.Models;
using System.Text.Json.Nodes;
using Xunit;

namespace Monq.Plugins.Abstractions.Tests;

public class PluginTests
{
    [Fact(DisplayName = "Проверка конвертации объекта в словарь.")]
    public void ShouldProperlyCovertObjectToDictionary()
    {
        var obj = new TestClass
        {
            A = 1,
            B = "test",
            C = new()
            {
                CA = [1, 2],
                CB = true
            },
        };
        var dict = obj.ToResult(ApplicationSerializerContext.Default.TestClass);
        var child = Assert.IsType<IDictionary<string, object?>>(dict["c"], exactMatch: false);
        var values = Assert.IsType<IList<object?>>(child["ca"], exactMatch: false);

        Assert.Equal(1, dict["a"]);
        Assert.Equal("test", dict["b"]);
        Assert.Equal(1, values[0]);
        Assert.Equal(2, values[1]);
        Assert.Equal(true, child["cb"]);
    }

    [Fact(DisplayName = "Проверка конвертации словаря в объект.")]
    public void ShouldProperlyCovertDictionaryToObject()
    {
        var dict = new Dictionary<string, object?>()
        {
            ["a"] = 1,
            ["b"] = "test",
            ["c"] = new Dictionary<string, object?>
            {
                ["ca"] = new[] { 1, 2 },
                ["cb"] = true
            },
            ["d"] = JsonValue.Create("jsonString")
        };
        var obj = dict.ToConfig(ApplicationSerializerContext.Default.TestClass);
        Assert.Equal(1, obj.A);
        Assert.Equal("test", obj.B);
        Assert.Equal(1, obj.C.CA[0]);
        Assert.Equal(2, obj.C.CA[1]);
        Assert.True(obj.C.CB);
        Assert.Equal("jsonString", obj.D);
    }
}

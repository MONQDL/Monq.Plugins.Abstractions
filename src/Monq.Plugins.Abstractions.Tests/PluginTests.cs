using Monq.Plugins.Abstractions.Extensions;
using Monq.Plugins.Abstractions.Tests.Models;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Monq.Plugins.Abstractions.Tests;

public class PluginTests
{
    [Fact(DisplayName = "Проверка конвертации объекта в словарь.")]
    public void ShouldProperlyCovertObjectToDictionary()
    {
        var obj = new
        {
            a = 1,
            b = "test", 
            c = new
            {
                ca = new[] { 1, 2 },
                cb = true
            },
        };
        var dict = obj.ToResult();
        Assert.Equal(1, dict["a"]);
        Assert.Equal("test", dict["b"]);
        Assert.Equal(1, ((List<object>)((IDictionary<string, object?>)dict["c"])["ca"])[0]);
        Assert.Equal(2, ((List<object>)((IDictionary<string, object?>)dict["c"])["ca"])[1]);
        Assert.Equal(true, ((IDictionary<string, object?>)dict["c"])["cb"]);
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
            ["d"] = new JValue("jsonString")
        };
        var obj = dict.ToConfig<TestClass>();
        Assert.Equal(1, obj.A);
        Assert.Equal("test", obj.B);
        Assert.Equal(1, obj.C.CA[0]);
        Assert.Equal(2, obj.C.CA[1]);
        Assert.True(obj.C.CB);
        Assert.Equal("jsonString", obj.D);
    }
}

using Monq.Plugins.Abstractions.Tests.Models;
using System.Text.Json.Serialization;

namespace Monq.Plugins.Abstractions.Tests.JsonSerializerContexts;

[JsonSourceGenerationOptions(
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    NumberHandling = JsonNumberHandling.AllowReadingFromString,
    PropertyNameCaseInsensitive = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    UseStringEnumConverter = true)]
[JsonSerializable(typeof(TestClass))]
internal partial class ApplicationSerializerContext : JsonSerializerContext;

# Monq.Plugins.Abstractions

[RU](#ru) · [EN](#en)

<a id="ru"></a>

## RU

`Monq.Plugins.Abstractions` — библиотека с интерфейсами и моделями для разработки пользовательских
плагинов [Monq Agent](https://docs.monq.ru/docs/guide/data-collect/monq-agent).

Плагин добавляет агенту новую команду. При выполнении задания агент находит эту команду, создаёт
соответствующую стратегию и передаёт ей параметры задания.

Библиотека поддерживает .NET 8, .NET 9 и .NET 10.

### Основные компоненты

Для работы плагина необходимы два основных компонента:

1. Bootstrap-класс с реализацией `IPluginTaskBootstrap`.
2. Стратегия с реализацией `IPluginTaskStrategy`.

#### IPluginTaskBootstrap

Bootstrap сообщает агенту, какую команду предоставляет плагин, какой класс должен её выполнять и
какие сервисы необходимо зарегистрировать.

```csharp
public sealed class PluginTaskBootstrap : IPluginTaskBootstrap
{
    public PluginTask PluginTask { get; } = new(
        "Example plugin",
        "example",
        typeof(PluginTaskStrategy));

    public void RegisterServiceProvider(
        IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<PluginTaskStrategy>();
    }
}
```

Параметры `PluginTask`:

- `Name` — отображаемое название плагина.
- `Command` — команда, указанная в поле `plugin` задания. Команда должна быть уникальной среди
  загруженных плагинов и учитывает регистр.
- `ProcessorStrategyType` — класс стратегии, которая выполняет команду.

В `RegisterServiceProvider` регистрируются стратегия и собственные сервисы плагина. Параметр
`configuration` предоставляет конфигурацию запущенного агента.

#### IPluginTaskStrategy

`IPluginTaskStrategy` описывает выполнение команды плагина. Агент вызывает метод `Run` и передаёт
ему:

- `context` — контекст с параметрами задания, системными переменными агента и именами защищённых
  переменных;
- `cancellationToken` — сигнал отмены задания.

Итог выполнения возвращается из `Run`:

```csharp
public sealed class PluginTaskStrategy : IPluginTaskStrategy
{
    public Task<JsonObject> Run(
        PluginTaskContext context,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(new JsonObject
        {
            ["result"] = "Completed",
        });
    }
}
```

Во время выполнения стратегия может опубликовать любое количество промежуточных или потоковых
данных через `context.WriteOutput`. Метод ожидает завершения обработки данных host-приложением и тем
самым поддерживает обратное давление.

```csharp
await context.WriteOutput(record, cancellationToken);
```

Одна стратегия может возвращать только итоговый результат, публиковать поток данных или совмещать
оба варианта. Поэтому модель плагина не зависит от выбранного host-приложением режима выполнения.
`Run` завершается, когда плагин закончил работу, получил отмену через `cancellationToken` или не смог
продолжить выполнение.

### Работа с параметрами и результатами

Параметры, результаты и отдельные потоковые записи представлены как `JsonObject`. Значениями могут
быть строки, числа, логические значения, `null`, вложенные объекты и массивы.

`context.Variables` содержит только параметры плагина, а `context.SystemVariables` — системные
переменные агента. Если плагину нужны системные значения, он получает их из `SystemVariables`
отдельно.

```csharp
if (context.Variables["address"]?.GetValue<string>() is not { } address)
    throw new PluginNotConfiguredException("Address is not defined.");

var result = new JsonObject
{
    ["address"] = address,
    ["connected"] = true,
};
```

Плагин самостоятельно отвечает за:

- проверку обязательных параметров и `null`;
- проверку и преобразование типов;
- обработку вложенных JSON-объектов и массивов;
- защиту секретных значений от попадания в журнал.

При добавлении существующего вложенного объекта или массива в новый результат используйте
`DeepClone()`.

Параметры можно преобразовать в типизированную модель стандартными средствами `System.Text.Json`:

```csharp
var config = JsonSerializer.Deserialize(
    context.Variables,
    PluginJsonSerializerContext.Default.PluginConfig) ?? new();
```

Для типизированных моделей необходим JSON-контекст:

```csharp
using System.Text.Json;
using System.Text.Json.Serialization;

[JsonSourceGenerationOptions(
    NumberHandling = JsonNumberHandling.AllowReadingFromString,
    PropertyNameCaseInsensitive = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    UseStringEnumConverter = true)]
[JsonSerializable(typeof(PluginConfig))]
internal partial class PluginJsonSerializerContext : JsonSerializerContext;
```

### Сервисы агента

`IProxyServiceProvider` предоставляет плагину сервисы агента, например `ILogger<T>` и
`IHttpClientFactory`. Получать следует только те сервисы, которые агент явно разрешает использовать
плагинам.

### Дополнительная информация

- [Документация Monq Agent](https://docs.monq.ru/docs/guide/data-collect/monq-agent)
- [Wiki по разработке плагинов](https://github.com/MONQDL/agent-docs/wiki)
- [Примеры готовых плагинов](https://github.com/MONQDL/agent-docs)
- [Именование плагинов и команд](https://github.com/MONQDL/agent-docs/wiki/%D0%98%D0%BC%D0%B5%D0%BD%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D0%B5-%D0%BF%D0%BB%D0%B0%D0%B3%D0%B8%D0%BD%D0%BE%D0%B2-%D0%B8-%D0%BA%D0%BE%D0%BC%D0%B0%D0%BD%D0%B4#%D0%B8%D0%BC%D0%B5%D0%BD%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D0%B5-%D0%BA%D0%BE%D0%BC%D0%B0%D0%BD%D0%B4)
- [Рабочие конфигурации агента](https://docs.monqlab.com/docs/guide/data-collect/monq-agent/#%D1%80%D0%B0%D0%B1%D0%BE%D1%87%D0%B8%D0%B5-%D0%BA%D0%BE%D0%BD%D1%84%D0%B8%D0%B3%D1%83%D1%80%D0%B0%D1%86%D0%B8%D0%B8-%D0%B0%D0%B3%D0%B5%D0%BD%D1%82%D0%BE%D0%B2)

---

<a id="en"></a>

## EN

`Monq.Plugins.Abstractions` provides the interfaces and models required to build custom
[Monq Agent](https://docs.monq.ru/docs/guide/data-collect/monq-agent) plugins.

A plugin adds a new command to the agent. When a task is executed, the agent finds that command,
creates the corresponding strategy, and passes the task parameters to it.

The library supports .NET 8, .NET 9, and .NET 10.

### Core components

A plugin requires two main components:

1. A bootstrap class implementing `IPluginTaskBootstrap`.
2. A strategy implementing `IPluginTaskStrategy`.

#### IPluginTaskBootstrap

The bootstrap tells the agent which command the plugin provides, which class executes it, and which
services must be registered.

```csharp
public sealed class PluginTaskBootstrap : IPluginTaskBootstrap
{
    public PluginTask PluginTask { get; } = new(
        "Example plugin",
        "example",
        typeof(PluginTaskStrategy));

    public void RegisterServiceProvider(
        IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<PluginTaskStrategy>();
    }
}
```

`PluginTask` parameters:

- `Name` — the plugin display name.
- `Command` — the command specified in the task's `plugin` field. Commands must be unique among
  loaded plugins and are case-sensitive.
- `ProcessorStrategyType` — the strategy class that executes the command.

Register the strategy and the plugin's own services in `RegisterServiceProvider`. The
`configuration` parameter provides the running agent configuration.

#### IPluginTaskStrategy

`IPluginTaskStrategy` defines how a plugin command is executed. The agent calls `Run` and provides:

- `context` — the context containing task parameters, agent system variables, and secured variable
  names;
- `cancellationToken` — the task cancellation signal.

`Run` returns the final execution result:

```csharp
public sealed class PluginTaskStrategy : IPluginTaskStrategy
{
    public Task<JsonObject> Run(
        PluginTaskContext context,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(new JsonObject
        {
            ["result"] = "Completed",
        });
    }
}
```

While running, the strategy may publish any number of intermediate or streaming outputs through
`context.WriteOutput`. The method waits until the host finishes processing the output and therefore
provides backpressure.

```csharp
await context.WriteOutput(record, cancellationToken);
```

The same strategy may return only a final result, publish a stream of outputs, or combine both
approaches. The plugin model therefore does not depend on the execution mode selected by the host.
`Run` completes when the plugin finishes its work, receives cancellation through
`cancellationToken`, or cannot continue.

### Parameters and results

Parameters, results, and individual streaming records use `JsonObject`. Values can be strings,
numbers, booleans, `null`, nested objects, or arrays.

`context.Variables` contains only plugin parameters, while `context.SystemVariables` contains agent
system variables. When a plugin needs a system value, it reads it separately from
`SystemVariables`.

```csharp
if (context.Variables["address"]?.GetValue<string>() is not { } address)
    throw new PluginNotConfiguredException("Address is not defined.");

var result = new JsonObject
{
    ["address"] = address,
    ["connected"] = true,
};
```

The plugin is responsible for:

- checking required parameters and `null` values;
- validating and converting value types;
- handling nested JSON objects and arrays;
- preventing secured values from being written to logs.

Use `DeepClone()` when adding an existing nested object or array to a new result.

Parameters can be converted to a typed model with the standard `System.Text.Json` API:

```csharp
var config = JsonSerializer.Deserialize(
    context.Variables,
    PluginJsonSerializerContext.Default.PluginConfig) ?? new();
```

Typed models require a JSON context:

```csharp
using System.Text.Json;
using System.Text.Json.Serialization;

[JsonSourceGenerationOptions(
    NumberHandling = JsonNumberHandling.AllowReadingFromString,
    PropertyNameCaseInsensitive = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    UseStringEnumConverter = true)]
[JsonSerializable(typeof(PluginConfig))]
internal partial class PluginJsonSerializerContext : JsonSerializerContext;
```

### Agent services

`IProxyServiceProvider` provides access to agent services such as `ILogger<T>` and
`IHttpClientFactory`. A plugin should request only services that the agent explicitly exposes to
plugins.

### More information

- [Monq Agent documentation](https://docs.monq.ru/docs/guide/data-collect/monq-agent)
- [Plugin development wiki](https://github.com/MONQDL/agent-docs/wiki)
- [Plugin examples](https://github.com/MONQDL/agent-docs)
- [Plugin and command naming](https://github.com/MONQDL/agent-docs/wiki/%D0%98%D0%BC%D0%B5%D0%BD%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D0%B5-%D0%BF%D0%BB%D0%B0%D0%B3%D0%B8%D0%BD%D0%BE%D0%B2-%D0%B8-%D0%BA%D0%BE%D0%BC%D0%B0%D0%BD%D0%B4#%D0%B8%D0%BC%D0%B5%D0%BD%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D0%B5-%D0%BA%D0%BE%D0%BC%D0%B0%D0%BD%D0%B4)
- [Agent work configurations](https://docs.monqlab.com/docs/guide/data-collect/monq-agent/#%D1%80%D0%B0%D0%B1%D0%BE%D1%87%D0%B8%D0%B5-%D0%BA%D0%BE%D0%BD%D1%84%D0%B8%D0%B3%D1%83%D1%80%D0%B0%D1%86%D0%B8%D0%B8-%D0%B0%D0%B3%D0%B5%D0%BD%D1%82%D0%BE%D0%B2)

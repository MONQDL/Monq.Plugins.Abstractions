# Monq.Plugins.Abstractions

Это .NET библиотека с набором методов, классов и интерфейсов для реализации пользовательских плагинов [**Monq Agent**](https://docs.monq.ru/docs/guide/data-collect/monq-agent). Здесь вы можете узнать основную информацию о составе этой библиотеки. За подробной информацией по созданию пользовательских плагинов обратитесь к [wiki](https://github.com/MONQDL/agent-docs/wiki).
Также вы можете ознакомиться с [примерами готовых плагинов](https://github.com/MONQDL/agent-docs) в нашем репозитории.

## Ключевой состав библиотеки

* [namespace Monq.Plugins.Abstractions](#namespace-monqpluginsabstractions)
  * interface IPluginTaskBootstrap
  * interface IPluginTaskStrategy
  * interface IPluginCallbackStrategy
* [namespace Monq.Plugins.Abstractions.Extensions](#namespace-monqpluginsabstractionsextensions)
  * class ConfigExtensions
* [namespace Monq.Plugins.Abstractions.Models](#namespace-monqpluginsabstractionsmodels)
  * class PluginTask
* [namespace Monq.Plugins.Abstractions.Services](#namespace-monqpluginsabstractionsservices)
  * interface IProxyServiceProvider
  * class DataBuffer

## namespace Monq.Plugins.Abstractions

### [interface IPluginTaskBootstrap](https://github.com/MONQDL/Monq.Plugins.Abstractions/blob/master/src/Monq.Plugins.Abstractions/IPluginTaskBootstrap.cs)

Интерфейс загрузчика задания.
Предназначен для регистрации сущностей, необходимых для работы плагина:

* Через свойство `PluginTask` регистрирует стратегию выполнения задачи;
* Метод `RegisterServiceProvider` регистрирует зависимости в DI контейнере агента.

### [interface IPluginTaskStrategy](https://github.com/MONQDL/Monq.Plugins.Abstractions/blob/master/src/Monq.Plugins.Abstractions/IPluginTaskStrategy.cs)

Интерфейс стратегии выполнения задания.
Содержит только определение метода `Run`, который вызывается агентом при получении соответствующего задания.
Здесь может быть реализована логика работы с конкретным источником данных, а именно:

* Извлечение данных из источника;
* Преобразование, очистка и обогащение данных, чтобы они соответствовали потребностям дальнейшей обработки в Monq;
* Отправка данных по API, хотя рекомендуемым способом доставки данных в Monq служит артефакт задания.

### [interface IPluginTaskCallbackStrategy](https://github.com/MONQDL/Monq.Plugins.Abstractions/blob/master/src/Monq.Plugins.Abstractions/IPluginTaskCallbackStrategy.cs)

Интерфейс стратегии выполнения задания с обратным вызовом.
Содержит только определение метода `Run`, который вызывается агентом при получении соответствующего задания.
Здесь может быть реализована логика непрерывной работы задания, например, через [рабочие конфигурации агента](https://docs.monqlab.com/docs/guide/data-collect/monq-agent/#%D1%80%D0%B0%D0%B1%D0%BE%D1%87%D0%B8%D0%B5-%D0%BA%D0%BE%D0%BD%D1%84%D0%B8%D0%B3%D1%83%D1%80%D0%B0%D1%86%D0%B8%D0%B8-%D0%B0%D0%B3%D0%B5%D0%BD%D1%82%D0%BE%D0%B2).
Функция обратного вызова (callback) позволит передать промежуточный результат выполнения задания на следующие шаги пользовательского сценария.

## namespace Monq.Plugins.Abstractions.Extensions

### [class ConfigExtensions](https://github.com/MONQDL/Monq.Plugins.Abstractions/blob/master/src/Monq.Plugins.Abstractions/Extensions/ConfigExtensions.cs)

Класс полезных расширений, предназначенных для двусторонней конвертации определяемых в плагине моделей в словари, содержащие названия свойств и их значения. Применение этого метода описано в разделе [конвертация variables в модель конфигурации задания](https://github.com/MONQDL/agent-docs/wiki/%D0%9F%D0%B5%D1%80%D0%B5%D0%B4%D0%B0%D1%87%D0%B0-%D0%BF%D0%BB%D0%B0%D0%B3%D0%B8%D0%BD%D1%83-%D0%BF%D0%B0%D1%80%D0%B0%D0%BC%D0%B5%D1%82%D1%80%D0%BE%D0%B2-%D0%B8%D0%B7-YAML-%D1%81%D0%BA%D1%80%D0%B8%D0%BF%D1%82%D0%B0-%D0%B7%D0%B0%D0%B4%D0%B0%D0%BD%D0%B8%D1%8F#%D0%BA%D0%BE%D0%BD%D0%B2%D0%B5%D1%80%D1%82%D0%B0%D1%86%D0%B8%D1%8F-variables-%D0%B2-%D0%BC%D0%BE%D0%B4%D0%B5%D0%BB%D1%8C-%D0%BA%D0%BE%D0%BD%D1%84%D0%B8%D0%B3%D1%83%D1%80%D0%B0%D1%86%D0%B8%D0%B8-%D0%B7%D0%B0%D0%B4%D0%B0%D0%BD%D0%B8%D1%8F).

## namespace Monq.Plugins.Abstractions.Models

### [class PluginTask](https://github.com/MONQDL/Monq.Plugins.Abstractions/blob/master/src/Monq.Plugins.Abstractions/Models/PluginTask.cs)

Модель задания плагина.
Используется агентом для поиска необходимой стратегии выполнения задания.
Содержит следующие свойства:

* `Name` — публичное название плагина. Показывается в логах.
* `Command` — команда запуска плагина. Должна быть уникальной для каждого плагина, используемого в агенте. За подробной информацией об именовании команд обратитесь к разделу [именование команд плагинов](https://github.com/MONQDL/agent-docs/wiki/%D0%98%D0%BC%D0%B5%D0%BD%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D0%B5-%D0%BF%D0%BB%D0%B0%D0%B3%D0%B8%D0%BD%D0%BE%D0%B2-%D0%B8-%D0%BA%D0%BE%D0%BC%D0%B0%D0%BD%D0%B4#%D0%B8%D0%BC%D0%B5%D0%BD%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D0%B5-%D0%BA%D0%BE%D0%BC%D0%B0%D0%BD%D0%B4-%D0%BF%D0%BB%D0%B0%D0%B3%D0%B8%D0%BD%D0%BE%D0%B2).
* `ProcessorStrategyType` — тип стратегии выполнения задания. Экземпляр этого класса создается агентом при выполнении задания.

## namespace Monq.Plugins.Abstractions.Services

Реализации абстракций данных сервисов уже определены на агенте, поэтому их можно внедрять напрямую в стратегии выполнения плагинов.

### [interface IProxyServiceProvider](https://github.com/MONQDL/Monq.Plugins.Abstractions/blob/master/src/Monq.Plugins.Abstractions/Services/IProxyServiceProvider.cs)

Интерфейс прокси-провайдера сервисов.
Наследует стандартный в .NET Core интерфейс `IServiceProvider`.
Предоставляет доступ к общим для всех плагинов сервисов, реализации которых определены на агенте.
На данный момент через прокси-провайдер можно получать следующие абстракции:

* `ILogger<T>` — стандартный в .NET Core интерфейс для логирования. С его помощью можно пользоваться едиными настройками логирования во всех плагинах.
* `IHttpClientFactory` — стандартный в .NET Core интерфейс для работы с HTTP. Позволяет создавать HTTP клиенты с общей конфигурацией.

```c#
readonly ILogger<PluginTaskStrategy> _logger;
readonly IHttpClientFactory _httpClientFactory;

public PluginTaskStrategy(
    IProxyServiceProvider proxyServiceProvider)
)
{
    _logger = proxyServiceProvider.GetRequiredService<ILogger<PluginTaskStrategy>>();
    _httpClientFactory = proxyServiceProvider.GetRequiredService<IHttpClientFactory>();
}
```

### [class DataBuffer](https://github.com/MONQDL/Monq.Plugins.Abstractions/blob/master/src/Monq.Plugins.Abstractions/Services/DataBuffer.cs)

Абстрактный класс буфера данных.

Документация в процессе создания.

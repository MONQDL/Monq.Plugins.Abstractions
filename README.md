# Monq.Plugins.Abstractions

Это .NET библиотека с набором методов, классов и интерфейсов для реализации пользовательских плагинов [**Monq Agent**](https://docs.monq.ru/docs/guide/data-collect/monq-agent). Здесь вы можете узнать основную информацию о составе этой библиотеки. За подробной информацией по созданию пользовательских плагинов обратитесь к [wiki](https://github.com/MONQDL/agent-docs/wiki). 
Также вы можете ознакомиться с [примерами готовых плагинов](https://github.com/MONQDL/agent-docs) в нашем репозитории.

## Состав библиотеки

* [namespace Monq.Plugins.Abstractions](#namespace-monqpluginsabstractions)
  * interface IPluginTaskStrategy
  * interface IPluginTaskPostProcessor
  * interface IPluginTaskBootstrap
* [namespace Monq.Plugins.Abstractions.Converters](#namespace-monqpluginsabstractionsconverters)
  * class DictionaryConverter
* [namespace Monq.Plugins.Abstractions.Extensions](#namespace-monqpluginsabstractionsextensions)
  * class ObjectExtensions
* [namespace Monq.Plugins.Abstractions.Exceptions](#namespace-monqpluginsabstractionsexceptions)
  * class PluginNotConfiguredException
* [namespace Monq.Plugins.Abstractions.Models](#namespace-monqpluginsabstractionsmodels)
  * class PluginTask

## namespace Monq.Plugins.Abstractions

### [interface IPluginTaskBootstrap](https://github.com/MONQDL/Monq.Plugins.Abstractions/blob/master/src/Monq.Plugins.Abstractions/IPluginTaskBootstrap.cs)
Этот интерфейс предназначен для регистрации сущностей необходимых для работы плагина:
* Cвойство `PluginTask` регистрирует стратегию выполнения задачи;
* Метод `RegisterServiceProvider` регистрирует зависимости в DI контейнере агента.

*Примеры реализации интерфейса: [SystemInfoPlugin](https://github.com/MONQDL/agent-docs/blob/master/SystemInfoPlugin/SystemInfoPlugin/PluginTaskBootstrap.cs), [SystemMetricsPlugin](https://github.com/MONQDL/agent-docs/blob/master/SystemMetricsPlugin/SystemMetricsPlugin/PluginTaskBootstrap.cs).*

### [interface IPluginTaskPostProcessor](https://github.com/MONQDL/Monq.Plugins.Abstractions/blob/master/src/Monq.Plugins.Abstractions/IPluginTaskPostProcessor.cs)
Этот интерфейс содержит только метод `Process` предназначенный для обработки результата задания после его выполнения.

### [interface IPluginTaskStrategy](https://github.com/MONQDL/Monq.Plugins.Abstractions/blob/master/src/Monq.Plugins.Abstractions/IPluginTaskStrategy.cs)
Этот интерфейс содержит только определение метода `Run` который вызывается **Monq Agent**'ом при получении соответствующего задания. Здесь должна выполняться ETL логика плагина для работы с конкретным источником данных, а именно:
* Извлечение данных из источника;
* Преобразование, очистка и обогащение данных, чтобы они соответствовали потребностям дальнейшей обработки в Monq;
* Загрузка преобразованных данных в Monq с помощью API потоков данных для дальнейшей обработки.

*Примеры реализации интерфейса: [SystemInfoPlugin](https://github.com/MONQDL/agent-docs/blob/master/SystemInfoPlugin/SystemInfoPlugin/PluginTaskStrategy.cs), [SystemMetricsPlugin](https://github.com/MONQDL/agent-docs/blob/master/SystemMetricsPlugin/SystemMetricsPlugin/PluginTaskStrategy.cs).*

## namespace Monq.Plugins.Abstractions.Models

### [class PluginTask](https://github.com/MONQDL/Monq.Plugins.Abstractions/blob/master/src/Monq.Plugins.Abstractions/PluginTask.cs)
Это модель задания плагина. Используется агентом для поиска необходимой стратегии выполнения задания. Содержит следующие свойства:
* `Name` Публичное название команды. Показывается в логах.
* `Command` Идентификатор команды. Должен быть уникальным для всех команд всех плагинов используемых в агенте. За подробной информацией об именовании команд обратитесь к разделу  [именование команд плагинов](https://github.com/MONQDL/agent-docs/wiki/%D0%98%D0%BC%D0%B5%D0%BD%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D0%B5-%D0%BF%D0%BB%D0%B0%D0%B3%D0%B8%D0%BD%D0%BE%D0%B2-%D0%B8-%D0%BA%D0%BE%D0%BC%D0%B0%D0%BD%D0%B4#%D0%B8%D0%BC%D0%B5%D0%BD%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D0%B5-%D0%BA%D0%BE%D0%BC%D0%B0%D0%BD%D0%B4-%D0%BF%D0%BB%D0%B0%D0%B3%D0%B8%D0%BD%D0%BE%D0%B2).
* `ProcessorStrategyType` Тип стратегии выполнения задания. Экземпляр этого класса создается агентом при выполнении задания.
* `PostProcessorStrategyType` Опциональное свойство. Тип стратегии обработки результатов задания.  Экземпляр этого класса создается агентом после выполнения основного задания для обработки результатов.

*Примеры использования класса: [SystemInfoPlugin](https://github.com/MONQDL/agent-docs/blob/master/SystemInfoPlugin/SystemInfoPlugin/PluginTaskBootstrap.cs#L25), [SystemMetricsPlugin](https://github.com/MONQDL/agent-docs/blob/master/SystemMetricsPlugin/SystemMetricsPlugin/PluginTaskBootstrap.cs#L25).*

## namespace Monq.Plugins.Abstractions.Extensions

### [class ObjectExtensions](https://github.com/MONQDL/Monq.Plugins.Abstractions/blob/master/src/Monq.Plugins.Abstractions/Extensions/ObjectExtensions.cs)
Класс расширения предназначенный для двусторонней конвертации моделей в словарь содержащий названия свойств и их значения. Применение этого метода описано в разделе [конвертация variables в модель конфигурации задания](https://github.com/MONQDL/agent-docs/wiki/%D0%9F%D0%B5%D1%80%D0%B5%D0%B4%D0%B0%D1%87%D0%B0-%D0%BF%D0%BB%D0%B0%D0%B3%D0%B8%D0%BD%D1%83-%D0%BF%D0%B0%D1%80%D0%B0%D0%BC%D0%B5%D1%82%D1%80%D0%BE%D0%B2-%D0%B8%D0%B7-YAML-%D1%81%D0%BA%D1%80%D0%B8%D0%BF%D1%82%D0%B0-%D0%B7%D0%B0%D0%B4%D0%B0%D0%BD%D0%B8%D1%8F#%D0%BA%D0%BE%D0%BD%D0%B2%D0%B5%D1%80%D1%82%D0%B0%D1%86%D0%B8%D1%8F-variables-%D0%B2-%D0%BC%D0%BE%D0%B4%D0%B5%D0%BB%D1%8C-%D0%BA%D0%BE%D0%BD%D1%84%D0%B8%D0%B3%D1%83%D1%80%D0%B0%D1%86%D0%B8%D0%B8-%D0%B7%D0%B0%D0%B4%D0%B0%D0%BD%D0%B8%D1%8F).

*Примеры использования метода: [SystemInfoPlugin](https://github.com/MONQDL/agent-docs/blob/master/SystemInfoPlugin/SystemInfoPlugin/PluginTaskStrategy.cs#L28), [SystemMetricsPlugin](https://github.com/MONQDL/agent-docs/blob/master/SystemMetricsPlugin/SystemMetricsPlugin/PluginTaskStrategy.cs#L32).*

## namespace Monq.Plugins.Abstractions.Exceptions

### [class PluginNotConfiguredException](https://github.com/MONQDL/Monq.Plugins.Abstractions/blob/master/src/Monq.Plugins.Abstractions/Exceptions/PluginNotConfiguredException.cs)
Исключение предназначенное для информирования о некорректной конфигурации плагина.

*Примеры использования исключения: [SystemInfoPlugin](https://github.com/MONQDL/agent-docs/blob/master/SystemInfoPlugin/SystemInfoPlugin/PluginTaskStrategy.cs#L46), [SystemMetricsPlugin](https://github.com/MONQDL/agent-docs/blob/master/SystemMetricsPlugin/SystemMetricsPlugin/PluginTaskStrategy.cs#L51).*

## namespace Monq.Plugins.Abstractions.Converters

### [class DictionaryConverter](https://github.com/MONQDL/Monq.Plugins.Abstractions/blob/master/src/Monq.Plugins.Abstractions/Converters/DictionaryConverter.cs)
Класс предназначен для внутреннего использования.

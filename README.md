# Monq.Plugins.Abstractions

Библиотека с набором методов, классов и интерфейсов для реализации DLL плагинов для агентов MONQ.

## Руководство к написанию плагинов агента

Основная документация по написанию плагинов в [репозиторие](https://github.com/MONQDL/agent-docs).

### Создание проекта

1. Подключить к проекту библиотеку `Monq.Plugins.Abstractions`.

    Библиотека предоставляет два основных интерфейса, которые должны быть реализованы в каждом плагине:

    - `IPluginTaskStrategy` - интерфейс с методом запуска плагинного задания.
    - `IPluginTaskBootstrap` - интерфейс с методом регистрации сервисов для работы плагинного задания.

2. Версию плагина определить в .csproj файле основного проекта.

    ``` xml
    <Version>1.0.0</Version>
    <VersionSuffix>$(VersionSuffix)</VersionSuffix>
    <Version Condition=" '$(VersionSuffix)' != '' ">$(Version)-$(VersionSuffix)</Version>
    ```

3. Указать название сборки в .csproj файле основного проекта, которое будет совпадать с названием вызывающей команды плагина в скриптах задания.

    ``` xml
    <AssemblyName>pluginCommand</AssemblyName>
    ```

    ``` yaml
    # yaml agent task script
    ...
    steps:
      - plugin: pluginCommand
    ...
    ```

4. Подключаемые библиотеки, которые определены на агенте, необходимо исключать из финальной публикации во избежание конфликтов. Для этого в .csproj файле основного проекта указать настройки пакета:

    ``` xml
    <PackageReference Include="Monq.Plugins.Abstractions" Version="1.0.0">
      <Private>false</Private>
      <ExcludeAssets>runtime</ExcludeAssets>
    </PackageReference>
    ```

    Список подключенных на агенте библиотек указан в разделе [Список библиотек](#список-библиотек).

### Сборка и использование на агенте

Команда для сборки: `dotnet publish -c Release`. В папке с опубликованными файлами будет находиться нужный набор файлов для подключения на агенте.

Чтобы подключить плагин на агенте, нужно в папке с плагинами агента создать папку, совпадающую с названием вызывающей команды плагина в скриптах задания. В папку нужно перенести опубликованные файлы в первозданном виде.

### Список библиотек

| Название | Версия |
|---|---|
| AutoMapper | 10.1.1 |
| Microsoft.Data.SqlClient | 3.0.1 |
| Microsoft.EntityFrameworkCore.SqlServer | 5.0.11 |
| Microsoft.Extensions.Http | 5.0.0 |
| Microsoft.Extensions.Logging.Abstractions | 5.0.0 |
| Microsoft.Extensions.Options.ConfigurationExtensions | 5.0.0 |
| Monq.Core.BasicDotNetMicroservice | 6.1.0 |
| Monq.Plugins.Abstractions | 1.4.2 |

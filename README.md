![Bootstrapper API by Codebelt](.nuget/Codebelt.Bootstrapper/icon.png)
# Bootstrapper API by Codebelt

[![bootstrapper CI/CD Pipeline](https://github.com/codebeltnet/bootstrapper/actions/workflows/pipelines.yml/badge.svg)](https://github.com/codebeltnet/bootstrapper/actions/workflows/pipelines.yml) [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=bootstrapper&metric=alert_status)](https://sonarcloud.io/dashboard?id=bootstrapper) [![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=bootstrapper&metric=sqale_rating)](https://sonarcloud.io/dashboard?id=bootstrapper) [![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=bootstrapper&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=bootstrapper) [![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=bootstrapper&metric=security_rating)](https://sonarcloud.io/dashboard?id=bootstrapper) [![OpenSSF Scorecard](https://api.scorecard.dev/projects/github.com/codebeltnet/bootstrapper/badge)](https://scorecard.dev/viewer/?uri=github.com/codebeltnet/bootstrapper)

An open-source family of assemblies (MIT license) that provide a uniform and consistent way of bootstraping your code with Program.cs paired with Startup.cs -OR- using the new `Minimal` equivalent for all project types.

## Codebelt.Bootstrapper

The core types of this lightweight boostrapper framework optimized for console apps, providing a uniform and consistent implementation of:

+ [console](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new#console)-
+ [worker](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new#web-others)-
+ [web](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new#web)-
+ [mvc](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new#web-options)-
+ [webapp](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new#web-options)-
+ [webapi](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new#webapi)-

-project types.

Common for all project types is that they have both `Program.cs` paired with `Startup.cs`. Also, all are implemented using `IHostedService`; this means that for a traditional `console` you have option for graceful shutdown should you require this (cronjob scenarios or similar).

## Codebelt.Bootstrapper.Console

An implementation optimized for `console` applications.

## Codebelt.Bootstrapper.Web

An implementation optimized for `web`, `webapi`, `webapp`, `razor`, `mvc` applications.

## Codebelt.Bootstrapper.Worker

An implementation optimized for `worker` services.

## 📦 Standalone Packages

Provides a focused API for bootstraping your code with Program.cs paired with Startup.cs -OR- using the new `Minimal` equivalent for all project types.

|Package|vNext|Stable|Downloads|
|:--|:-:|:-:|:-:|
| [Codebelt.Bootstrapper](https://www.nuget.org/packages/Codebelt.Bootstrapper/) | ![vNext](https://img.shields.io/nuget/vpre/Codebelt.Bootstrapper?logo=nuget) | ![Stable](https://img.shields.io/nuget/v/Codebelt.Bootstrapper?logo=nuget) | ![Downloads](https://img.shields.io/nuget/dt/Codebelt.Bootstrapper?color=blueviolet&logo=nuget) |
| [Codebelt.Bootstrapper.Web](https://www.nuget.org/packages/Codebelt.Bootstrapper.Web/) | ![vNext](https://img.shields.io/nuget/vpre/Codebelt.Bootstrapper.Web?logo=nuget) | ![Stable](https://img.shields.io/nuget/v/Codebelt.Bootstrapper.Web?logo=nuget) | ![Downloads](https://img.shields.io/nuget/dt/Codebelt.Bootstrapper.Web?color=blueviolet&logo=nuget) |
| [Codebelt.Bootstrapper.Console](https://www.nuget.org/packages/Codebelt.Bootstrapper.Console/) | ![vNext](https://img.shields.io/nuget/vpre/Codebelt.Bootstrapper.Console?logo=nuget) | ![Stable](https://img.shields.io/nuget/v/Codebelt.Bootstrapper.Console?logo=nuget) | ![Downloads](https://img.shields.io/nuget/dt/Codebelt.Bootstrapper.Console?color=blueviolet&logo=nuget) |
| [Codebelt.Bootstrapper.Worker](https://www.nuget.org/packages/Codebelt.Bootstrapper.Worker/) | ![vNext](https://img.shields.io/nuget/vpre/Codebelt.Bootstrapper.Worker?logo=nuget) | ![Stable](https://img.shields.io/nuget/v/Codebelt.Bootstrapper.Worker?logo=nuget) | ![Downloads](https://img.shields.io/nuget/dt/Codebelt.Bootstrapper.Worker?color=blueviolet&logo=nuget) |


### Contributing to `Bootstrapper API by Codebelt`
[Contributions](.github/CONTRIBUTING.md) are welcome and appreciated.

Feel free to submit issues, feature requests, or pull requests to help improve this library.

### License
This project is licensed under the MIT License - see the [LICENSE](LICENSE.md) file for details.

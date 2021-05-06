# Bootstrapper Framework for Console Apps (NET 5)

## Codebelt.Bootstrapper
The core types of this lightweight boostrapper framework optimized for console apps, providing a uniform and consistent implementation of:

+ [console](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new#console)-
+ [worker](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new#web-others)-
+ [web](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new#web)-
+ [mvc](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new#web-options)-
+ [webapp](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new#web-options)-
+ [webapi](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new#webapi)-

-project types.

Common for all projec types is that they have both `Program.cs` paired with `Startup.cs`. Also, all are implemented using `IHostedService`; this means that for a traditional `console` you have option for graceful shutdown should you require this (cronjob scenarios or similiar).

## Codebelt.Bootstrapper.Common

An implementation of a `console` application.

## Codebelt.Bootstrapper.Web

An implementation of a `web` application and the base for webapi, webapp and mvc.

## Codebelt.Bootstrapper.WebApi

An implementation of a `webapi` application.

## Codebelt.Bootstrapper.WebApp

An implementation of a `webapp`, `razor` application.

## Codebelt.Bootstrapper.WebMvc

An implementation of a `mvc` application.

## Codebelt.Bootstrapper.Worker

An implementation of a `worker` application.

---

Code with passion; love your code; deliver with pride 👨‍💻️🔥❤️🚀🤘

![Extensions for AWS Signature Version 4 API by Codebelt](.nuget/Codebelt.Bootstrapper/icon.png)
# Bootstrapper Framework for Console Apps (>= .NET 6)

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

---

Code with passion; love your code; deliver with confidence 👨‍💻️🔥❤️🚀😎

### Contributing to `Bootstrapper Framework for Console Apps`
[Contributions](.github/CONTRIBUTING.md) are welcome and appreciated.

Feel free to submit issues, feature requests, or pull requests to help improve this library.

### License
This project is licensed under the MIT License - see the [LICENSE](LICENSE.md) file for details.

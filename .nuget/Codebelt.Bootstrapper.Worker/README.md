## About

An open-source family of assemblies (MIT license) that provide a uniform and consistent way of bootstraping your code with `Program.cs` paired with `Startup.cs`.

Also, common for all, is the implementation of the `IHostedService` interface; this means that all project types, including the traditional `console`, now have option for graceful shutdown should your application require this (cronjob scenarios or similar).

It is, by heart, free, flexible and built to extend and boost your agile codebelt.

## Codebelt.Bootstrapper.Worker

An implementation optimized for `worker` services.

## Related Packages

* [Codebelt.Bootstrapper](https://www.nuget.org/packages/Codebelt.Bootstrapper/) 📦
* [Codebelt.Bootstrapper.Console](https://www.nuget.org/packages/Codebelt.Bootstrapper.Console/) 📦
* [Codebelt.Bootstrapper.Web](https://www.nuget.org/packages/Codebelt.Bootstrapper.Web/) 📦
* [Codebelt.Bootstrapper.Worker](https://www.nuget.org/packages/Codebelt.Bootstrapper.Worker/) 📦
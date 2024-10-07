# Changelog

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/), and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

For more details, please refer to `PackageReleaseNotes.txt` on a per assembly basis in the `.nuget` folder.

## [3.0.0] - TBD

This major release is first and foremost focused on ironing out any wrinkles that have been introduced with .NET 9 preview releases so the final release is production ready together with the official launch from Microsoft.

### Added

- RunAsync (abstract method) to the ConsoleHostedService class in the Codebelt.Bootstrapper.Console namespace
- UseServices (abstract method) to the ConsoleHostedService class in the Codebelt.Bootstrapper.Console namespace

### Changed

- Dependencies to latest and greatest with respect to TFM
- Run (abstract method) from the ConsoleStartup class located in the Codebelt.Bootstrapper.Console namespace (breaking change)
- ConsoleHostedService class in the Codebelt.Bootstrapper.Console namespace to provide a better developer experience

### Removed

- Support for TFM .NET 6 (LTS)
- ILogger{TStartup} argument from the ConsoleHostedService class in the Codebelt.Bootstrapper.Console namespace (breaking change)

## [2.0.0] - 2024-09-08

### Changed

- Dependencies to latest and greatest with respect to TFM
- WebStartup class in the Codebelt.Bootstrapper.Web namespace to provide a new abstraction; ConfigurePipeline(IApplicationBuilder) and removed ILogger parameter from Configure method (breaking change)

### Removed

- Support for TFM .NET 7 (STS)


## [1.3.0] - 2024-02-11

### Changed

- Dependencies to latest and greatest with respect to TFM

## [1.2.0] - 2023-12-15

### Added

- Support for TFM .NET 8 (LTS)

### Changed

- Dependencies to latest and greatest with respect to TFM

## [1.1.0] - 2022-11-09

### Added

- Support for TFM .NET 7 (STS)

### Changed

- Dependencies to latest and greatest with respect to TFM

### Removed

- Support for TFM .NET 5 (STS)

## [1.0.1] - 2021-12-29

### Added

- Support for TFM .NET 6 (LTS)

### Changed

- Dependencies to latest and greatest with respect to TFM
- Namespace from Codebelt.Bootstrapper.Common to Codebelt.Bootstrapper.Console (breaking change)

### Fixed

- Non-critical bug in the ConsoleHostedService class located in the Codebelt.Bootstrapper.Console namespace

## [1.0.0] - 2021-07-05

### Added

- BootstrapperLifetime class in the Codebelt.Bootstrapper namespace that listens for Ctrl+C or SIGTERM and initiates shutdown
- HostBuilderExtensions class in the Codebelt.Bootstrapper namespace that consist of extension methods for the IHostBuilder interface: UseBootstrapperLifetime, UseBootstrapperStartup
- IStartupFactory interface in the Codebelt.Bootstrapper namespace that provides an interface for initializing services and middleware used by an application
- ProgramRoot class in the Codebelt.Bootstrapper namespace that is the base entry point of an application responsible for registering its StartupRoot partner
- StartupFactory class in the Codebelt.Bootstrapper namespace that is the default implementation of IStartupFactory
- StartupRoot class in the Codebelt.Bootstrapper namespace that provides the base class of a conventional based Startup class
- ConsoleHostedService class in the Codebelt.Bootstrapper.Common namespace that provides a console application that is managed by its host
- ConsoleProgram class in the Codebelt.Bootstrapper.Common namespace that is the base entry point of an application responsible for registering its ConsoleStartup partner
- ConsoleStartup interface in the Codebelt.Bootstrapper.Common namespace that provides the base class of a conventional based Startup class for a console application
- HostBuilderExtensions class in the Codebelt.Bootstrapper.Common namespace that consist of extension methods for the IHostBuilder interface: UseConsoleStartup
- WebProgram class in the Codebelt.Bootstrapper.Web namespace that is the entry point of an application responsible for registering its WebStartup partner
- WebStartup class in the Codebelt.Bootstrapper.Web namespace that provides the base class of a conventional based Startup class for web applications
- HostBuilderExtensions class in the Codebelt.Bootstrapper.Worker namespace that consist of extension methods for the IHostBuilder interface: UseWorkerStartup
- WorkerProgram class in the Codebelt.Bootstrapper.Worker namespace that is the base entry point of an application responsible for registering its WorkerStartup partner
- WorkerStartup interface in the Codebelt.Bootstrapper.Worker namespace that provides the base class of a conventional based Startup class for a console application
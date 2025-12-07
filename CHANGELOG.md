# Changelog

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/), and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

For more details, please refer to `PackageReleaseNotes.txt` on a per assembly basis in the `.nuget` folder.

## [5.0.1] - 2025-12-07

This is a service update that primarily focuses on fixing issues related to host lifetime status message suppression.

### Fixed

- ConsoleHostedService class in the Codebelt.Bootstrapper.Console namespace to properly handle suppression of host lifetime status messages when configured to do so
- MinimalConsoleHostedService class in the Codebelt.Bootstrapper.Console namespace to properly handle suppression of host lifetime status messages when configured to do so

## [5.0.0] - 2025-11-14

This is a major release that focuses on adapting the latest `.NET 10` release (LTS) in exchange for current `.NET 8` (LTS).

> To ensure access to current features, improvements, and security updates, and to keep the codebase clean and easy to maintain, we target only the latest long-term (LTS), short-term (STS) and (where applicable) cross-platform .NET versions.

## [4.0.6] - 2025-10-20

This is a service update that focuses on package dependencies.

## [4.0.5] - 2025-09-15

This is a service update that focuses on package dependencies.

## [4.0.4] - 2025-08-20

This is a service update that focuses on package dependencies.

## [4.0.3] - 2025-07-11

This is a service update that focuses on package dependencies.

## [4.0.2] - 2025-06-16

This is a service update that focuses on package dependencies.

## [4.0.1] - 2025-05-25

This is a service update that focuses on package dependencies.

## [4.0.0] - 2025-04-12

This major release revisits and refines some of the earlier design decisions to offer a more consistent and flexible API. It also brings forward improvements to reliability and maintainability.

### Added

- IHostLifetimeEvents interface in the Codebelt.Bootstrapper namespace that provides a convenient way to be notified of host lifetime events

### Changed

- BootstrapperLifetime class in the Codebelt.Bootstrapper namespace to implement IHostLifetimeEvents and hereby removing static equivalents (breaking change)
- UseBootstrapperStartup method on the HostApplicationBuilderExtensions class in the Codebelt.Bootstrapper namespace to extend IHostApplicationBuilder instead of HostApplicationBuilder (breaking change)
- UseBootstrapperProgram method on the HostApplicationBuilderExtensions class in the Codebelt.Bootstrapper.Console namespace to extend IHostApplicationBuilder instead of HostApplicationBuilder (breaking change)
- UseMinimalConsoleProgram method on the HostApplicationBuilderExtensions class in the Codebelt.Bootstrapper.Console namespace to extend IHostApplicationBuilder instead of HostApplicationBuilder (breaking change)

### Removed

- HostedServiceExtensions class in the Codebelt.Bootstrapper namespace (breaking change)
- WebApplicationBuilderExtensions class in the Codebelt.Bootstrapper.Web namespace (breaking change)

### Fixed

- BootstrapperLifetime class in the Codebelt.Bootstrapper namespace to disregard SuppressStatusMessages and always assign callbacks to members of IHostLifetimeEvents

## [3.0.1] - 2025-01-31

This is a service update that primarily focuses on package dependencies and minor improvements.

## [3.0.0] - 2024-11-13

This major release is first and foremost focused on ironing out any wrinkles that have been introduced with .NET 9 preview releases so the final release is production ready together with the official launch from Microsoft.

Highlighted features included in this release:

- Support for consistent use of `Minimal` equivalents to all Program/Startup pairs of bootstrapper application roles

### Added

- RunAsync (abstract method) to the ConsoleStartup class in the Codebelt.Bootstrapper.Console namespace
- ConfigureConsole (virtual method) to the ConsoleStartup class in the Codebelt.Bootstrapper.Console namespace
- HostApplicationBuilderExtensions class in the Codebelt.Bootstrapper namespace that consist of extension methods for the IHostApplicationBuilder interface: UseBootstrapperLifetime
- HostedServiceExtensions class in the Codebelt.Bootstrapper namespace that consist of extension methods for the IHostedService interface: WaitForApplicationStartedAnnouncementAsync
- HostApplicationBuilderExtensions class in the Codebelt.Bootstrapper.Console namespace that consist of extension methods for the HostApplicationBuilder class: UseBootstrapperProgram and UseMinimalConsoleProgram
- IProgramFactory interface in the Codebelt.Bootstrapper.Console namespace that provides an interface for initializing services and middleware used by an application
- MinimalConsoleHostedService class in the Codebelt.Bootstrapper.Console namespace that provides a console application that is managed by its host
- MinimalConsoleProgram class in the Codebelt.Bootstrapper.Console namespace that provides the base entry point of an application optimized for console applications
- ProgramFactory class in the Codebelt.Bootstrapper.Console namespace that is the default implementation of IProgramFactory
- MinimalWebProgram class in the Codebelt.Bootstrapper.Web namespace that is the entry point of an application optimized for web applications
- WebApplicationBuilderExtensions class in the Codebelt.Bootstrapper.Web namespace that consist of extension methods for the WebApplicationBuilder class: UseBootstrapperLifetime
- MinimalWorkerProgram class in the Codebelt.Bootstrapper.Worker namespace that is the entry point of an application optimized for worker applications

### Changed

- Dependencies to latest and greatest with respect to TFM
- ConsoleHostedService class in the Codebelt.Bootstrapper.Console namespace to provide a significantly better developer experience
- BootstrapperLifetime in the Codebelt.Bootstrapper namespace to support adhering to ConsoleLifetimeOptions
- UseBootstrapperStartup method on the HostBuilderExtensions class in the Codebelt.Bootstrapper namespace to be generic (breaking change)
- IStartupFactory interface in the Codebelt.Bootstrapper namespace to be generic and reference an instance of TStartup (breaking change)
- ProgramRoot class in the Codebelt.Bootstrapper namespace to have zero members (breaking change)
- StartupFactory class in the Codebelt.Bootstrapper namespace to use generic IStartupFactory{TStartup} interface (breaking change)

### Removed

- Support for TFM .NET 6 (LTS)
- Run (abstract method) from the ConsoleStartup class located in the Codebelt.Bootstrapper.Console namespace (breaking change)
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
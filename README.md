# .NET-Mentoring-Program-Advanced - Online Store

## Getting started

### Prerequisites
* [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
* [Docker](https://docs.docker.com/docker-for-mac/install/)
* IDE
  * [dotNet CLI](https://docs.microsoft.com/en-us/dotnet/core/tools/) and
  * [Visual Studio Code](https://code.visualstudio.com/) or
  * [Visual Studio](https://visualstudio.microsoft.com/)

### Instructions for running tests
The testing tool [xUnit.net](https://xunit.net/) is used for testing. To run integration tests, Docker Desktop must be running.
Test fixtures will automatically create the necessary containers as well as databases. The first run of the integration tests can take up to 5 minutes as the container management tool loads the necessary docker images.

The [DotNet.Testcontainers](https://github.com/testcontainers/testcontainers-dotnet) is used to manage docker containers.

### Built With
* [DotNet.Testcontainers](https://github.com/testcontainers/testcontainers-dotnet)
* [xUnit.net](https://xunit.net/)
* [fluentassertions](https://fluentassertions.com/)
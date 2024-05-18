[![.NET](https://github.com/konak/am.kon.packages.services.web-client-service/actions/workflows/dotnet.yml/badge.svg)](https://github.com/konak/am.kon.packages.services.web-client-service/actions/workflows/dotnet.yml)

# WebClientService for .NET Core

The WebClientService package provides a robust and flexible web client component designed to be used as a service within .NET Core applications. It leverages the power of '**IHttpClientFactory**' to facilitate efficient and scalable interactions with web services. This package supports dependency injection, making it easy to integrate into your existing .NET Core applications.

## Key Features:
- **Dependency Injection**: Seamlessly integrates with the .NET Core dependency injection framework.
- **HTTP Client Management**: Utilizes '**IHttpClientFactory**' for robust HTTP client instantiation and management.
- **Result Handling**: Comprehensive handling of HTTP response results with detailed status and error information.
- **Extensible Base Classes**: Abstract base classes for implementing services that interact with web endpoints, returning various data types including strings and streams.
- **Configurable Requests**: Supports various HTTP methods, custom headers, bearer token authentication, and more.

## Usage Scenarios:
- Consuming RESTful APIs
- Fetching data from web services
- Handling HTTP responses with rich error information
- Streaming data from endpoints

## Getting Started:

1. **Installation**: Install the package via NuGet Package Manager.

```shell
dotnet add package WebClientService
```

2. **Configuration**: Configure IHttpClientFactory in your Startup.cs.

```C#
public void ConfigureServices(IServiceCollection services)
{
    services.AddHttpClient();
    services.AddScoped<WebClientStringDataService>();
    services.AddScoped<WebClientStreamDataService>();
}
```

3. **Usage**: Inject the service into your controllers or services and start making web requests.

```C#
public class MyService
{
    private readonly WebClientStringDataService _webClient;

    public MyService(WebClientStringDataService webClient)
    {
        _webClient = webClient;
    }

    public async Task<string> GetDataAsync(Uri requestUri)
    {
        var result = await _webClient.InvokeRequest(requestUri);
        if (result.Result == RequestInvocationResultTypes.Ok)
        {
            return result.Data;
        }
        else
        {
            // Handle error
            throw new Exception(result.Message);
        }
    }
}
```

# Juhta.NET Namespaces

------------------------------

## Juhta.Net.Common

Juhta.Net.Common is the home for all useful common classes that have no special relationship to any separate feature or capability that Juhta.NET provides.

Juhta.Net.Common is implemented by the Juhta.Net library.

## Juhta.Net.Console

Juhta.Net.Console contains classes that help building console applications. At this moment, the namespace provides a versatile support for command line argument parsing, which is a quite typical challenge for console applications.

Juhta.Net.Console is implemented by the Juhta.Net.Console library.

## Juhta.Net.Diagnostics

There is always room for good logging and tracing capabilities, and that is exactly what the Juhta.Net.Diagnostics namespace aims to provide. For the moment, the Juhta.Net.Diagnostics namespace provides a generic diagnostic message and logging model with a built-in file logger. However, in the roadmap, there is a separate Juhta.Net.Diagnostics library, which will extend logging and tracing features to a whole new level.

Juhta.Net.Diagnostics is implemented by the Juhta.Net library.

## Juhta.Net.Extensions

Juhta.Net.Extensions is the namespace for extension classes. These extension classes contain nothing but extension methods. Furthermore, all extension methods have been divided into separate classes according to the corresponding classes they extend.

Juhta.Net.Extensions is implemented by the Juhta.Net library.

## Juhta.Net.Framework

Juhta.Net.Framework is a namespace containing classes and type definitions that relate to the core infrastructure of Juhta.NET.

Juhta.Net.Framework is implemented by the Juhta.Net library.

## Juhta.Net.Helpers

Juhta.Net.Helpers is reserved for so-called helper classes. Helper classes have no official definition, but their purpose is to facilitate certain operations in a quite restricted context. For now, in the Juhta.Net.Helpers namespace there is only one helper class defined that facilitates argument validation.

Juhta.Net.Helpers is implemented by the Juhta.Net library.

## Juhta.Net.LibraryManagement

Juhta.Net.LibraryManagement defines comprehensive library management interfaces and base classes to implement them. With these library management interfaces and base classes, you can easily implement methods for starting and closing your application libraries gracefully.

The library management interfaces support also so-called dynamic libraries, which means you can change the configuration of your libraries on the fly. Of course, to make that possible, your libraries have to implement appropriate dynamic library interfaces.

Juhta.Net.LibraryManagement is implemented by the Juhta.Net.LibraryManagement library.

## Juhta.Net.Services

Juhta.Net.Services provides dependency injection services for the application. These dependency injection services can comprise of any number of configured services. Each dependency injection service can also have any number of configured constructor parameters with values. Dependency injection services can even have other dependency injection services as constructor parameters. This makes it possible to define aggregate services.

The running application doesn’t have to know anything about how services are being constructed. An application just creates instances of services by corresponding service types or service identifiers.

Juhta.Net.Services is implemented by the Juhta.Net.Services library.

## Juhta.Net.Startup

Juhta.Net.Startup provides a startup support for any application. This means that you can configure and start a logical application, which is a singleton instance of the Application class. You can start and close your application gracefully by just implementing appropriate library management interfaces for your libraries. Your application can have any number of libraries. Furthermore, each of these libraries can be of any library type supported by the library management interfaces. Juhta.Net.Startup takes care of all startup and shutdown code for you.

An application can be of any type, from console applications to web applications and web API services.

Juhta.Net.Startup is implemented by the Juhta.Net.Startup library.

## Juhta.Net.Validation

Juhta.Net.Validation defines common data validation interfaces and provides a set of validator classes. The goal of this namespace is to introduce a uniform model for validating any kind of data from scalar values to aggregate objects. The other important goal is to provide ready validator classes for the most common business data validation needs.

Juhta.Net.Validation is implemented by the Juhta.Net.Validation library.

## Juhta.Net.WebApi.Exceptions

Juhta.Net.WebApi.Exceptions defines base classes for the Web API client error and server error exceptions and for the corresponding error response classes.

Juhta.Net.WebApi.Exceptions is implemented by the Juhta.Net.WebApi.Exceptions library.

## Juhta.Net.WebApi.Exceptions.Client

Juhta.Net.WebApi.Exceptions.Client defines exception classes for all HTTP client errors (4xx) defined in the [System.Net.HttpStatusCode](https://docs.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode?view=netstandard-2.0) enumeration.

Juhta.Net.WebApi.Exceptions.Client is implemented by the Juhta.Net.WebApi.Exceptions library.

## Juhta.Net.WebApi.Exceptions.Server

Juhta.Net.WebApi.Exceptions.Server defines exception classes for all HTTP server errors (5xx) defined in the [System.Net.HttpStatusCode](https://docs.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode?view=netstandard-2.0) enumeration.

Juhta.Net.WebApi.Exceptions.Server is implemented by the Juhta.Net.WebApi.Exceptions library.

# Juhta.NET

Juhta.NET is an open source, general purpose app framework built on the top of .NET Core.

The basic idea of Juhta.NET is to provide a modular approach for the application development. You just choose those modules suitable for your application without any unnecessary dependencies to the entire framework.

Juhta.NET is built to support modern architectures. These architectures comprise such buzzwords as microservices, RESTful Web APIs, event sourcing etc.

And of course, Juhta.NET is free, now and in the future. The only driver for its development is the pure passion for software development and code writing without any goals for economic benefits whatsoever.

You may wonder what the word ”juhta” means? It’s a Finnish word and means “beast of burden” in English, that is, an animal such as a mule or donkey that is used for carrying loads. Therefore, in the application development world it's a great metaphor for a solid and robust app framework running workloads. The word is also very close to the author’s first name, which gives it another association.

## Juhta.NET Libraries

### Juhta.Net

Juhta.Net is the core library of the framework. It contains types, interfaces, classes etc. that are shared between all libraries of Juhta.NET. However, although Juhta.Net is the core of the framework, it's also a standalone library that can be used on its own to boost up app development.

Juhta.Net contains the following namespaces:

* Juhta.Net provides classes for logging (with built-in logger) and managing the framework.
* Juhta.Net.Common provides a wide set of common classes.
* Juhta.Net.Extensions provides useful extension classes.
* Juhta.Net.LibraryManagement provides a comprehensive library management functionalities. With this library management you can start and close your application gracefully by just implementing appropriate library management interfaces. Juhta.NET takes care of all startup and shutdown code for you. The library management supports also so called dynamic libraries, which means you can change the configuration of your libraries on the fly.


## On the roadmap (subject to change)

#### Juhta.Net.Diagnostics

There is always room for a good logging ang tracing library, and that's exactly what Juhta.Net.Diagnostics aims to be.

*To be updated.*

#### Juhta.Net.WebApi

*To be updated.*

#### Juhta.Net.WebApi.Gateway

*To be updated.*

#### Juhta.Net.WebApi.Services

*To be updated.*

#### Juhta.Net.Processing

*To be updated.*

# Juhta.NET

Juhta.NET is an open source, general purpose app framework built on the top of .NET Core.

The basic idea of Juhta.NET is provide a modular approach for the application development. You just choose those modules suitable for your application without any unnecessary dependencies to the entire framework.

And of course, Juhta.NET is free, and it always will be. The only driver for its development is the pure passion for software development and code writing without any goals for economic benefits whatsoever.

You may wonder what the word ”juhta” means? It’s a Finnish word and means “beast of burden” in English, that is, an animal such as a mule or donkey that is used for carrying loads. So in the application development world it's a metaphor for a solid and robust app framework running workloads. The word is also very close to the author’s first name which gives it another association.

## Juhta.NET Libraries

### Juhta.Net

Juhta.Net is the core library of the framework. It contains types, interfaces, and classes etc. that are shared between all libraries of Juhta.NET. Furthermore, Juhta.Net is a standalone library that can be used alone to boost up app development.

The namespace of Juhta.Net are:

* Juhta.Net provides classes for logging (with built-in file logger) and managing the framework.
* Juhta.Net.Common provides a wide set of common classes.
* Juhta.Net.Extensions provides useful extension classes.
* Juhta.Net.LibraryManagement provides a comprehensive library management functionalities. With this library management you can start and close your application gracefully by just implementing appropriate library management interfaces. Juhta.NET takes care of all startup and shutdown code for you. The library management support also so called dynamic libraries which means you change the configuration of your libraries on the fly.

### Juhta.Net.Diagnostics

*to be updated*

## On the roadmap (subject to change)

### Juhta.Net.WebApi

### Juhta.Net.WebApi.Gateway

### Juhta.Net.WebApi.Services

### Juhta.Net.Processing

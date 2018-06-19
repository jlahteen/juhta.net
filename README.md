# Welcome to Juhta.NET

Juhta.NET is an open-source, general-purpose application framework built on the top of .NET Core 2 and .NET Standard 2 (or later).

The basic idea of Juhta.NET is to provide a modular approach for the application development. You just choose those modules suitable for your application without any unnecessary dependencies to the entire framework. However, please note that the first release of Juhta.NET consists of only one library, which is the core library of Juhta.NET.

The future versions of Juhta.NET will be built to support modern architectures. These architectures comprise such buzzwords as microservices, RESTful Web APIs, event sourcing etc.

## Motivation

Juhta.NET is free, now and in the future. The main driver for its development is the pure passion for software development and code writing without any goals for economic benefits whatsoever.

The other important driver is that the author just wants to make himself and the current team more productive and also share this productivity toolbox in an open way.

## License

Juhta.NET is licensed under the permissive MIT License, please see the [LICENSE.txt](LICENSE.txt) file for more details.

## Main Features

The current main features of Juhta.NET are as follows:

* Process-level configurable application model
* Library management support for starting and closing any number of libraries with the application
* Versatile and configurable dependency injection support
* Diagnostics message model and a built-in logger
* Command line argument parsing support for console applications
* Data validation model and a set of validation classes
* Set of useful common classes
* Set of useful extension classes containing extension methods
* Helper class for argument validation

More details about the main namespaces can be found [here](https://jlahteen.github.io/juhta.net/latest/docs/main-namespaces.html).

## Documentation

The documentation of the latest release can be found [here](https://jlahteen.github.io/juhta.net/latest/docs).

## API Reference

The API Reference of the latest release can be found [here](https://jlahteen.github.io/juhta.net/latest/api).

## How to Play with the Code

Juhta.NET has been developed by using [Visual Studio Community](https://www.visualstudio.com/vs/community/). However, to get started, you just need the following:

* Download and install [.NET Core 2.1 SDK](https://www.microsoft.com/net/download/windows)
* Clone or download the [source code](https://github.com/jlahteen/juhta.net)

By using [Git](https://git-scm.com/), to clone the repo, type:

```batch
git clone https://github.com/jlahteen/juhta.net.git
```

To build Juhta.NET, go to the root directory of the solution and type:

```batch
dotnet build
```

To run all the unit tests in the solution, just type:

```batch
dotnet test
```

## Releases

* Juhta.NET 1.0.0-rc, July xx, 2018
  * [Release notes](https://github.com/jlahteen/juhta.net/releases)
  * [Home page](https://jlahteen.github.io/juhta.net/latest)

## Future Roadmap

Here is the future roadmap for upcoming versions. Please note that the upcoming versions are subject to change.

| Version              | Schedule | What's New
|----------------------|----------|-----------
| Juhta.NET 1.0.0      | Q3 2018  | Unit tests will also pass on Linux and MacOS
| Juhta.NET 1.1.0      | Q4 2018  | Juhta.Net.Diagnostics library included
| Juhta.NET 1.2.0      | Q4 2018  | Juhta.Net.WebApi library included

## Author

The author of Juhta.NET is Juha Lähteenmäki. You can connect with the author on [Twitter](https://twitter.com/jlahteen) or [LinkedIn](https://fi.linkedin.com/in/juhalahteenmaki).

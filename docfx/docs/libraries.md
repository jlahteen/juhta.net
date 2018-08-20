# Juhta.NET Libraries

---------------------

Juhta.NET consists of several libraries that are shipped as NuGet packages. Each library encapsulates a modular functionality of its own. The dependencies between the libraries have been minimized for ensuring as modular implementation as possible.

Juhta.NET libraries could be called as "feature" libraries. The basic idea is that a developer just picks up those libraries that are needed by the application to boost up the implementation. There is no need to use the entire framework if not necessary.

## Current Libraries

---------------------

Here is a list of the current libraries of Juhta.NET:

* **Juhta.Net** is the core library of Juhta.NET, and it contains common classes, types and interfaces that are shared between all libraries of Juhta.NET.

* **Juhta.Net.Console** contains classes that help building console applications.

* **Juhta.Net.LibraryManagement** defines comprehensive and versatile interfaces and base classes for initializing and closing application libraries gracefully.

* **Juhta.Net.Services** provides flexible and scalable dependency injection services for an application.

* **Juhta.Net.Startup** provides methods for graceful startup and shutdown for any application with any number of libraries through the Juhta.Net.LibraryManagement interfaces.

* **Juhta.Net.Validation** defines common data validation interfaces and provides a set of validator classes.

# The Dispatcher API
### *RESTful Task Scheduling & Priority Engine*

## Overview
A modern **ASP.NET Core 9 Web API** designed for high-performance task dispatching. This project demonstrates **Clean Architecture**, **Asynchronous Data Handling**, and **Relational Persistence** using Entity Framework Core.

## Key Technical Features
- **RESTful Design:** Full CRUD implementation (GET, POST, PUT, PATCH, DELETE) using standard HTTP status codes.
- **EF Core Persistence:** Implements **SQLite** as a relational backend with **Code-First Migrations** for schema versioning.
- **Asynchronous Pipeline:** Uses `async/await` and `Task<T>` patterns to ensure non-blocking database I/O.
- **Modern API Documentation:** Integrated with **Scalar** and **OpenAPI 9** for real-time endpoint testing and documentation.
- **Dependency Injection:** Leverages the .NET built-in DI container for `AppDbContext` lifecycle management.

- ## Architectural Decisions
- **Data Transfer Objects (DTOs):** Implemented to decouple the internal database schema from the public API contract, preventing over-posting attacks and ensuring system scalability.
- **Model Validation:** Utilized Data Annotations (`[Required]`, `[RegularExpression]`) to enforce business rules at the "Gatekeeper" (API) level, reducing unnecessary database hits.
- **Async/Await Pattern:** Every endpoint is non-blocking to ensure high availability under concurrent loads.


## How to Run
1. Ensure [.NET 9 SDK](https://dotnet.microsoft.com) is installed.
2. `dotnet build`
3. `dotnet run`
4. Navigate to `http://localhost:5066/scalar/v1` to test the endpoints.

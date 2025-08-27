# ASP.NET Core 9 Clean Architecture Template

A production-ready Clean Architecture template for ASP.NET Core 9 applications with CQRS, Entity Framework Core, PostgreSQL, and comprehensive testing setup.

## 🏗️ Architecture Overview

This template follows Clean Architecture principles with clear separation of concerns:

```
┌─────────────────────────────────────────────────────────────┐
│                          WebAPI                             │
│                    (Controllers, Middleware)                │
├─────────────────────────────────────────────────────────────┤
│                      Infrastructure                         │
│              (Data Access, External Services)               │
├─────────────────────────────────────────────────────────────┤
│                       Application                           │
│                (Business Logic, Use Cases)                  │
├─────────────────────────────────────────────────────────────┤
│                        Domain                               │
│                (Entities, Value Objects)                    │
└─────────────────────────────────────────────────────────────┘
```

## 📁 Project Structure

```
CleanArchitectureTemplate/
├── src/
│   ├── Domain/                  # Core business logic and entities
│   │   ├── Common/
│   │   ├── Entities/
│   │   ├── Enums/
│   │   ├── ValueObjects/
│   │   └── Events/
│   ├── Application/             # Business use cases and application logic
│   │   ├── Common/
│   │   │   ├── Behaviours/
│   │   │   ├── Interfaces/
│   │   │   ├── Models/
│   │   │   └── Mappings/
│   │   └── Features/
│   │       └── SampleEntity/
│   │           ├── Commands/
│   │           └── Queries/
│   ├── Infrastructure/          # Data access and external services
│   │   ├── Data/
│   │   │   ├── Configurations/
│   │   │   └── Interceptors/
│   │   ├── Services/
│   │   └── Identity/
│   └── WebAPI/                  # API controllers and configuration
│       ├── Controllers/
│       ├── Middleware/
│       ├── Extensions/
│       └── Filters/
├── tests/
│   ├── UnitTests/               # Isolated unit tests
│   └── IntegrationTests/        # End-to-end integration tests
└── docs/                        # Documentation
```

## 🚀 Getting Started

### Prerequisites

- .NET 9.0 SDK
- PostgreSQL (local or Docker)
- Your favorite IDE (Visual Studio, VS Code, Rider)

### Using This Template

1. **Clone and Rename**
   ```bash
   git clone <github link>
   cd your-new-project
   ```

2. **Update Project Names** (Optional)
    - Replace "CleanArchitectureTemplate" in solution file
    - Update namespaces throughout the projects
    - Rename projects if desired

3. **Configure Database**
   - Check .env.example to configure database connection and Jwt settings


5. **Run the Application**
   ```bash
   dotnet restore
   dotnet build
   dotnet run --project src/WebAPI
   ```

## 📦 Technology Stack

### Core Framework
- **.NET 9.0** - Latest .NET framework
- **ASP.NET Core 9.0** - Web API framework
- **Entity Framework Core 9.0** - ORM for data access
- **PostgreSQL** - Primary database

### Architecture & Patterns
- **MediatR** - CQRS and Mediator pattern implementation
- **AutoMapper** - Object-to-object mapping
- **FluentValidation** - Request validation

### Authentication & Security
- **ASP.NET Identity** - User management
- **JWT Bearer** - Token-based authentication

### Logging & Documentation
- **Serilog** - Structured logging
- **Swagger/OpenAPI** - API documentation

### Testing
- **xUnit** - Testing framework
- **Moq** - Mocking framework
- **FluentAssertions** - Readable assertions
- **TestContainers** - Integration testing with real databases

## 🏛️ Architecture Principles

### Clean Architecture Layers

1. **Domain Layer** (Core)
    - Contains enterprise business rules
    - Entities, Value Objects, Domain Events
    - No dependencies on other layers

2. **Application Layer**
    - Contains application business rules
    - Use cases, Commands, Queries (CQRS)
    - Depends only on Domain layer

3. **Infrastructure Layer**
    - Contains framework and external concerns
    - Database access, external APIs, file systems
    - Implements interfaces defined in Application layer

4. **WebAPI Layer**
    - Contains controllers and API configuration
    - Depends on Application and Infrastructure layers
    - Entry point for HTTP requests

### Key Patterns

- **CQRS** (Command Query Responsibility Segregation)
- **Repository Pattern** with Unit of Work
- **Dependency Injection**
- **Domain Events**
- **Specification Pattern** (optional)

## 🧪 Testing

### Running Tests

```bash
# Run all tests
dotnet test

# Run unit tests only
dotnet test tests/UnitTests

# Run integration tests only
dotnet test tests/IntegrationTests

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"
```

### Test Structure

- **Unit Tests**: Fast, isolated tests for business logic
- **Integration Tests**: Test complete request/response cycles
- **TestContainers**: Real database instances for integration testing

## 🛠️ Development

### Adding New Features

1. **Create Entity** (Domain layer)
2. **Create Commands/Queries** (Application layer)
3. **Create Handlers** (Application layer)
4. **Create Controllers** (WebAPI layer)
5. **Add Tests** (Unit and Integration)

### Example: Adding a Product Feature

```csharp
// 1. Domain/Entities/Product.cs
public class Product : BaseEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}

// 2. Application/Features/Products/Commands/CreateProduct.cs
public record CreateProductCommand(string Name, decimal Price) : IRequest<Guid>;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, Guid>
{
    // Implementation
}

// 3. WebAPI/Controllers/ProductsController.cs
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    // Implementation
}
```

## 🚀 Deployment

### Docker Support

```dockerfile
# Add Dockerfile for containerization
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAPI.dll"]
```

### Environment Configuration

- Development: `appsettings.Development.json`
- Production: `appsettings.Production.json`
- Environment variables for sensitive data

## 📝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 📞 Support

- 📧 Create an issue for bugs or feature requests
- 💬 Discussions for questions and ideas
- ⭐ Star this repository if you find it helpful!

---

**Happy Coding!** 🎉
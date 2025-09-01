
# SmartKart-Catalog Service

An enterprise-grade **Catalog microservice** built using **Domain-Driven Design (DDD)**, **Clean Architecture**, **Onion Architecture**, and **Microservices** best practices.

---

##  What’s Inside

- **Domain Layer**  
  - Contains core business entities: `Product`, `Category`, `Brand`  
  - Holds domain contracts like `IProductRepository`, `ICategoryRepository`  
  - Defines `IAggregateRoot` and domain logic (e.g., stock adjustments, validation)

- **Application Layer**  
  - Orchestrates use cases: `ProductService`, `CategoryService`, and `UnitOfWork`  
  - Contains data transfer objects (DTOs), commands/queries, and exception handling

- **Infrastructure Layer**  
  - Implements repositories using EF Core:
    - `EfRepository<T>` (generic)
    - `ProductRepository`, `CategoryRepository` (specific)
  - Includes `CatalogDbContext`, EF configurations, and Dependency Injection setup

- **API Layer (Presentation)**  
  - ASP.NET Core Web API exposing endpoints through `ProductController`
  - Sets up DI, Swagger, and middleware for clean API handling

---

##  Architecture Overview

+–––––––––––+
|    API Layer         |  ← Controllers (e.g., ProductsController)
+–––––––––––+
↓
+–––––––––––+
| Application Layer    |  ← Use cases (ProductService), UoW
+–––––––––––+
↓
+–––––––––––+
| Domain Layer         |  ← Entities, Repository Interfaces
+–––––––––––+
↑
+–––––––––––+
| Infrastructure Layer |  ← EF Core, DbContext, Repo implementations
+–––––––––––+

This structure follows **Clean/Onion Architecture**: dependencies flow inward, preserving a pure domain model and keeping infrastructure concerns at the edges.

---

##  Why This Architecture?

| Principle              | Benefit                                                                |
|-------------------------|------------------------------------------------------------------------|
| **DDD**                | Centers the code around business concepts (Product, Category, Brand)   |
| **Clean/Onion Layers** | Enhances maintainability and testability by separating concerns        |
| **Microservices**      | Each service, including Catalog, can evolve and scale independently    |
| **Unit of Work**       | Ensures consistent save lifecycle across repositories                  |

---

##  Getting Started

#### Prerequisites
- .NET 8 SDK
- Visual Studio 2022 / VS Code

#### Run Locally
1. Clone the repository:
   ```bash
   git clone https://github.com/SiddharthBhamare/SmartKart-Catalog.git
   cd SmartKart-Catalog/src/SmartKart.CatalogService.API

	2.	Update appsettings.json with your connection string under CatalogConnection.
	3.	Apply migrations:

dotnet ef database update --project ../SmartKart.CatalogService.Infrastructure


	4.	Run the API:

dotnet run


	5.	Navigate to https://localhost:5001/swagger for API docs.

⸻

Architectural Insights (Interview Tips)
	•	Domain Layer contains only business logic—no EF Core, framework, or API references.
	•	UnitOfWork aggregates repository interfaces and commits transactions consistently.
	•	Generic IRepository<T> offers basic CRUD support; specialized interfaces like IProductRepository add business-specific queries.
	•	ProductService depends only on IProductRepository, illustrating Inversion of Control.

⸻

What’s Next?

Future enhancements could include:
	•	CQRS for segregated read/write models (especially useful for high-scale query operations)
	•	Event-Driven Integration (e.g., messaging with RabbitMQ/Kafka) to connect microservices like Orders or Inventory
	•	Caching Strategy (e.g., Redis) and Distributed Logging for production readiness

⸻

Quick Summary

SmartKart-Catalog is a solid foundation showcasing how to structure enterprise-grade microservices using DDD, Clean/Onion Architecture, and best practices in .NET. This README aims to help both newcomers and seasoned developers grasp the reasoning behind each architectural choice.

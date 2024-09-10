# SpiceRackAPI
SpiceRackAPI is an ASP.NET Core API designed for restaurant management.  It offers a robust set of features for managing ingredients, dishes, tables, and orders, while adhering to the Onion architecture for clean separation of concerns.
This ASP.NET Core API serves as the backend for a restaurant management system. It provides a robust set of endpoints for managing ingredients, dishes, tables, and orders. The API adheres to a strict Onion architecture, ensuring a clean separation of concerns and high maintainability.

# Key Features

* **Secure User Management**: JWT authentication and Identity framework ensure secure user access and authorization.

* **Role-based Access Control**: Supports multiple roles (administrators and waiters) with granular permissions.

* **Onion Architecture**: Well-defined layers for domain, application, and infrastructure concerns.

* **Entity Framework Core**: Data persistence using code-first migrations.

* **AutoMapper**: Efficient object-object mapping between view models, entities, and DTOs.

* **Generic Repositories and Services**: Promotes code reusability and maintainability.

* **Swagger Documentation**: The API is documented with Swagger for easy exploration and usage.

* **Error Handling Middleware**: A centralized middleware is implemented for handling and capturing exceptions.

* **Comprehensive Validation**: View models are used for input validation.

# Technologies Used

* ASP.NET Core
  
* Entity Framework Core

* AutoMapper

* JWT

* Identity

* Onion Architecture

* Swagger UI

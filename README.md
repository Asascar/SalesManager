# Sales API

## Overview

The **Sales API** is a project designed to manage and process sales, customers, products, and branches while adhering to Domain-Driven Design (DDD) principles. The API provides CRUD functionality for all entities and includes business rules such as quantity-based discounts and sale item limits.

The project demonstrates skills and competencies in building RESTful APIs, applying clean architecture principles, and implementing complex business rules.

---

## Features

- Manage **Sales**, **Customers**, **Products**, and **Branches**.
- Apply business rules for discounts and item limits during sales creation:
  - 10% discount for 4-9 items of the same product.
  - 20% discount for 10-20 items of the same product.
  - Restriction: Cannot sell more than 20 units of the same product.
- Implement cancellation logic for sales and items.
- Generate **Swagger** documentation for the API.

---

## Tech Stack

- **Backend:** .NET 8 (ASP.NET Core)
- **Database:** PostgreSQL
- **ORM:** Entity Framework Core
- **Testing Framework:** xUnit
- **Documentation:** Swagger (via Swashbuckle)

---

## Project Structure

The project is organized following Domain-Driven Design principles:

```
Sales.API/
├── Controllers/           # API controllers (Sales, Products, Branches, Customers, etc.)
├── Models/                # Models layer with entities.
├── Services/
├── DTOs/ 
├── Context/               # Database context (SalesDbContext)
├── Repositories/          # Repository implementations
├── Program.cs             # Application startup configuration
└── appsettings.json       # Configuration file (e.g., database connection)
```

---

## Business Rules

1. **Discount Rules:**
   - 4-9 items: 10% discount.
   - 10-20 items: 20% discount.
   - No discounts for fewer than 4 items.
2. **Item Quantity Limits:**
   - Cannot sell more than 20 units of the same product.
3. **Cancellation:**
   - Sales and individual items can be canceled if necessary.

---

## Getting Started

### Prerequisites

- .NET SDK 8.0
- PostgreSQL server
- IDE (e.g., Visual Studio, Visual Studio Code)

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/your-username/sales-api.git
   cd sales-api
   ```

2. Configure the database connection in `appsettings.json`:

   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Database=SalesDb;Username=postgres;Password=yourpassword"
   }
   ```

3. Apply database migrations:

   ```bash
   dotnet ef database update
   ```

4. Run the application:

   ```bash
   dotnet run
   ```

[http://localhost:5000/swagger](http://localhost:5000/swagger)

````

---

## API Endpoints

### Sales
- `POST /api/sales`: Create a new sale.
- `GET /api/sales`: Get a list of all sales.
- `GET /api/sales/{id}`: Get a specific sale by ID.
- `PUT /api/sales/{id}`: Update a sale.
- `DELETE /api/sales/{id}`: Cancel a sale.

### Products
- `POST /api/products`: Create a new product.
- `GET /api/products`: Get a list of all products.
- `GET /api/products/{id}`: Get a specific product by ID.
- `PUT /api/products/{id}`: Update a product.
- `DELETE /api/products/{id}`: Delete a product.

### Branches
- `POST /api/branches`: Create a new branch.
- `GET /api/branches`: Get a list of all branches.
- `GET /api/branches/{id}`: Get a specific branch by ID.
- `PUT /api/branches/{id}`: Update a branch.
- `DELETE /api/branches/{id}`: Delete a branch.

### Customers
- `POST /api/customers`: Create a new customer.
- `GET /api/customers`: Get a list of all customers.
- `GET /api/customers/{id}`: Get a specific customer by ID.
- `PUT /api/customers/{id}`: Update a customer.
- `DELETE /api/customers/{id}`: Delete a customer.

---
````

---

## Future Enhancements

- Add support for asynchronous event publishing (e.g., SaleCreated, SaleModified).
- Integrate authentication and authorization.
- Implement a frontend application to interact with the API.

---

## License

This project is licensed under the MIT License. See the LICENSE file for details.


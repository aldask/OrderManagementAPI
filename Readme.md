# OrderManagementAPI

A simple .NET 8 RESTful API for managing products, orders, and invoices in a retail scenario.

---

## Functionalities Implemented (~ hours)

- **Products**
  - Create new products with `name` and `price`. Also `discount` and `minQuantity` can be set.
  - Retrieve a list of products, with optional search by name.
  - Apply discounts to products (`percentage` and `minimum quantity`).

- **Orders**
  - Create new orders with multiple products (quantity specified per product).
  - Retrieve all orders or a specific order by ID.

- **Invoices**
  - Retrieve invoice for an order:
    - Shows product `name`, `quantity`, `discount %`, `amount`, and `total`.
  - Retrieve report for discounted products:
    - Shows discounted product name, discount %, number of orders, total amount.

- **Infrastructure & Tools**
  - PostgreSQL persistence via Entity Framework Core.
  - Services for product, order, and invoice management.
  - Dependency Injection for services.
  - AutoMapper included for DTO mapping.
  - Unit test scaffolding with NUnit.
  - Swagger/OpenAPI documentation.

---

## Missing / To Be Included

---

## Prerequisites

- .NET 8 
- PostgreSQL database
- (Optional) Docker for containerization

---

## Setup / Run

1. Clone the repository:

```bash
git clone https://github.com/aldask/OrderManagementAPI.git
cd OrderManagementAPI
```bash

2. Configure PostgreSQL connection in appsettings.json:

```bash
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=OrderManagement;Username=postgres;Password=yourpassword"
}
```bash

3. Build and run:

```bash
dotnet build
dotnet run
```bash

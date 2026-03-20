# AbySalto Junior - Restaurant Order Management System

A REST API for managing restaurant orders, built with .NET 8 and Clean Architecture.

## Tech Stack

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core 8
- SQL Server Express
- Swagger / OpenAPI

## Architecture

The solution follows Clean Architecture principles, split into 4 projects:

- **Domain** - Core business models, enums and constants
- **Application** - Business logic, interfaces, DTOs and services
- **Infrastructure** - Database context, repository implementations and EF Core migrations
- **Presentation** - API controllers, middleware and configuration

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (free)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or any IDE with .NET support

## Getting Started

### 1. Clone the repository
```bash
git clone https://github.com/YOUR_USERNAME/YOUR_REPO.git
cd YOUR_REPO
```

### 2. Configure the database connection

Open `appsettings.json` in the Presentation project and update the connection string if needed:
```json
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=RestaurantDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

If your SQL Server instance has a different name, update `localhost\\SQLEXPRESS` accordingly.

### 3. Apply migrations
```bash
dotnet ef database update --project Infrastructure --startup-project AbySalto.Junior
```

### 4. Run the application
```bash
dotnet run --project AbySalto.Junior
```

Or press **F5** in Visual Studio.

### 5. Open Swagger

Navigate to `http://localhost:{port}` in your browser. Swagger UI will load automatically.

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/Restaurant | Get all orders (optional: ?sortByAmount=true) |
| GET | /api/Restaurant/{id} | Get order by ID |
| POST | /api/Restaurant | Create a new order |
| PUT | /api/Restaurant/{id}/status | Update order status |
| GET | /api/Restaurant/{id}/total | Calculate order total |

## Order Status Values

| Value | Description |
|-------|-------------|
| Pending | Order is waiting to be processed |
| InPreparation | Order is being prepared |
| Completed | Order has been completed |

## Payment Method Values

| Value | Description |
|-------|-------------|
| Cash | Payment in cash |
| Card | Payment by card |
| Online | Online payment |

## Example Request
```json
POST /api/Restaurant
{
    "customerName": "Ivan Horvat",
    "paymentMethod": "Card",
    "deliveryAddress": "Ilica 10, Zagreb",
    "contactNumber": "0911234567",
    "note": "Bez luka molim",
    "currency": "EUR",
    "items": [
        { "name": "Margherita pizza", "quantity": 2, "price": 12.50 },
        { "name": "Cola", "quantity": 2, "price": 2.50 }
    ]
}
```

## Currency Support

The API defaults to EUR. Supported currencies for sorting by amount:
EUR, USD, GBP, HRK

> Note: Exchange rates are hardcoded for simplicity. In a production environment these would be fetched from a live exchange rate API.

## Notes

- Currency defaults to EUR if not provided
- Orders must contain at least one item
- Sorting by amount converts all currencies to EUR equivalent for accurate comparison

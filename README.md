# Product Catalog API


<p align="center">
  <h3>A robust .NET 7 Web API for managing product catalogs</h3>
</p>

<p align="center">
  <a href="#features">Features</a> •
  <a href="#getting-started">Getting Started</a> •
  <a href="#api-endpoints">API Endpoints</a> •
  <a href="#configuration">Configuration</a> •
  <a href="#deployment">Deployment</a>
</p>

---

## 🌟 Features

- **CRUD Operations**: Full support for Products and Categories
- **Advanced Querying**: Pagination, search, and category filtering
- **Robust Architecture**: Repository pattern and Service layer implementation
- **Data Mapping**: Efficient object mapping with AutoMapper
- **API Documentation**: Interactive Swagger UI
- **Error Handling**: Global error handling middleware
- **Database**: SQL Server with Entity Framework Core

## 🚀 Getting Started

1. **Clone the repository**
```
//
```
2. **Restore packages**
```
dotnet restore
```
3. **Set up the database**
- Update the connection string in `appsettings.json`
- Apply migrations:
  ```
  dotnet ef database update
  ```

4. **Run the application**
```
dotnet run
```
🌐 The API will be available at:
- HTTP: `http://localhost:5296`

## 📡 API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET    | `/api/Products?PageNumber=1&PageSize=10&search=book&categoryId=5` | Get all Products (filterd & paginated) <h3>( **filter & pagination queryStrings are optional** )</h3>|
| GET    | `/api/Products/{id}` | Get a specific product |
| POST   | `/api/Products` | Create a new product |
| PUT    | `/api/Products/{id}` | Update a product |
| DELETE | `/api/Products/{id}` | Delete a product |
| GET    | `/api/Categories` | Get all Categories (paginated) |
| GET    | `/api/Categories/{id}` | Get a specific category |
| POST   | `/api/Categories` | Create a new category |
| PUT    | `/api/Categories/{id}` | Update a category |
| DELETE | `/api/Categories/{id}` | Delete a category |

📚 For detailed API documentation, visit `/swagger` when the application is running.

## ⚙️ Configuration

Key settings in `appsettings.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433; user=sa; Password=MyStrongPass123; Database=CategoriesProducts;TrustServerCertificate=true" 
   }
}

```

📦 Deployment
//

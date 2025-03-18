# To-Do List API

## Overview
This is a simple **To-Do List API** built with **.NET 8**, implementing **Clean Architecture** and **CQRS**. Users can register, log in, and manage their tasks. Each task is associated with the user who created it, and users can only access their own tasks. Authentication is handled using **JWT Bearer Tokens**.

## Features
- **User Authentication** (Register & Login) with JWT
- **Task Management** (CRUD operations)
- **CQRS Pattern** implementation
- **Entity Framework Core** with **MSSQL Server**
- **Clean Architecture** with `Domain`, `Application`, `Infrastructure`, `Presentation`, and `IOC`
- **Swagger API Documentation**

## Technologies Used
- **.NET 8**
- **Entity Framework Core**
- **MSSQL Server**
- **JWT Authentication**
- **CQRS with MediatR**
- **Repository Pattern**

## Project Structure
```
ToDoListAPI/
│-- Domain/
│-- Application/
│-- Infrastructure/
│-- Presentation/
│-- IOC/
│-- API-Doc/ (Postman Documentation)
```

## Getting Started
### Prerequisites
- Install **.NET 8 SDK**
- Install **SQL Server**

### Configuration
Update the `appsettings.json` file with your **database connection string** and **JWT settings**:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=ToDo_Db;User Id=amirkhavari;Password=13801380;TrustServerCertificate=True;"
  },
  "JwtSettings": {
    "SecretKey": "Somesupersecretkey1234567890987654321123456789123456789",
    "Issuer": "Rira-ToDo.test",
    "Audience": "Rira-ToDo.test",
    "ExpiryMinutes": 60
  },
  "AllowedHosts": "*"
}
```

### Running the Application
1. Restore dependencies:
   ```sh
   dotnet restore
   ```
2. Run the application:
   ```sh
   dotnet run --project Presentation
   ```

> **Note:** The database and tables will be automatically created on the first run.

### Authentication & Token Usage
- After **login**, the API returns a JWT token.
- The token must be included in the **Authorization header** as a **Bearer Token**:
  ```http
  Authorization: Bearer YOUR_TOKEN_HERE
  ```

## API Documentation
The complete API documentation, including request/response details, is available in **Postman Documentation**:
[API-Doc](./API-Doc/)

## License
This project is licensed under the **MIT License**.

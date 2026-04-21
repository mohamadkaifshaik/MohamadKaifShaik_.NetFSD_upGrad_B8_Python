# Shopee Employee CRM

This is a beginner-friendly mini project built with:

- ASP.NET Core MVC for the user interface
- ASP.NET Core Web API for backend services
- Entity Framework Core with SQL Server
- LINQ for data operations

## Solution Structure

- `src/Shopee.EmployeeCRM.Mvc` - MVC frontend application
- `src/Shopee.EmployeeCRM.Api` - Web API backend application
- `src/Shopee.EmployeeCRM.Shared` - shared DTOs and enums

## Beginner-Friendly Flow

1. MVC controller calls the API service.
2. API controller calls the service layer.
3. Service layer calls the repository layer.
4. Repository layer reads and writes data using Entity Framework Core.
5. SQL Server stores the data.

## Main Modules

- Employee management
- Client management
- Task management
- Dashboard reporting

## Notes

- The API uses `EnsureCreated()` and sample seed data to keep setup simple for beginners.
- The project includes role fields like `Admin`, `Manager`, and `Employee` so you can extend it later with authentication and authorization.

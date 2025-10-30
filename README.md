# OrderManagement API

A simple **Order Management System** built with **ASP.NET Core 8**, **Entity Framework Core**, and **AutoMapper**.  
Demonstrates **Repository & Service patterns**, DTO mapping, and a RESTful API for managing customers and orders.

---

## Project Structure

- `OrderManagement.Api` - ASP.NET Core Web API project  
- `OrderManagement.Core` - Domain models, DTOs, Interfaces, Services  
- `OrderManagement.Infrastructure` - EF Core DbContext, Repositories, Database configuration  

---

## Features

- CRUD operations for **Customers** and **Orders**  
- Retrieve all orders for a customer  
- Repository & Service pattern for clean separation of concerns  
- AutoMapper mapping between Entities and DTOs  
- Swagger UI for API testing  

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  
- SQL Server LocalDB (for development)  
- Optional: Visual Studio 2022 or VS Code  

---

## Setup

1. Clone the repository:

```bash
git clone https://github.com/janamihajloska/OrderManagementAPI
cd OrderManagementSolution

2. Update appsettings.json (in OrderManagement.Api project) with your connection string:
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=OrderManagementDb;Trusted_Connection=True;"
  }

3. Restore dependencies 
dotnet restore

4. Apply migrations 
dotnet ef database update --startup-project ../OrderManagement.Api

5. Run the API
dotnet run

## Testing the Api
Open SwaggerUI in your browser.
https://localhost:7061/swagger
Swagger will show all the endpoints. 
Click try it out to see responses.




# User Group Management MVC

A simple ASP.NET Core MVC application for managing users, groups, and permissions.

## Features
- Create, edit, and delete users
- Assign users to groups
- View user counts per group
- REST API backend with Entity Framework Core

## Technologies Used
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server

## How to Run
1. Clone the repository:
   ```bash
   git clone https://github.com/YourUsername/UserGroupManagement.git
Open the solution in Visual Studio.

Update the database connection string in appsettings.json.

Run database migrations:

bash
Copy
Edit
dotnet ef database update
Press F5 to start the application.

API Endpoints
GET /api/users – Get all users

GET /api/users/{id} – Get a specific user

POST /api/users – Create a new user

PUT /api/users/{id} – Update a user

DELETE /api/users/{id} – Delete a user

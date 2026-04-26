# 📘 Student Management System API

> A **.NET 9 Web API** built using **Clean Architecture + CQRS (MediatR)** with JWT Authentication, EF Core, FluentValidation, and SQL Server.  
> This project demonstrates production-style backend development with proper layering, security, and error handling.

---

## 🚀 Features

| Feature | Details |
|---|---|
| ✅ Student CRUD | Create, Read, Update, Delete |
| 🔐 JWT Authentication | Secure token-based auth |
| ⚡ CQRS Pattern | Powered by MediatR |
| 🧠 FluentValidation | Full request validation |
| 🗄️ EF Core | SQL Server integration |
| ⚠️ Global Exception Handling | Centralized middleware |
| 📄 Swagger | Auto-generated API docs |
| 🧩 Clean Architecture | 4-layer separation |

---

## 🏗️ Architecture

```
API Layer (Controllers)
        ↓
Application Layer (CQRS, DTOs, Validators)
        ↓
Infrastructure Layer (DbContext, Services, Repositories)
        ↓
Domain Layer (Entities)
```

---

## 🧑‍🎓 Student Entity

```csharp
public class Student
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
    public string Course { get; set; }
    public DateTime CreatedDate { get; set; }
}
```

---

## ⚙️ Prerequisites

Make sure you have installed:

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- SQL Server / SSMS
- Visual Studio 2022+ or VS Code
- EF Core CLI (`dotnet tool install --global dotnet-ef`)

---

## 🗄️ Database Setup

### 1. Update Connection String

In `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=StudentDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

**Example (LocalDB):**

```json
"Server=(localdb)\\MSSQLLocalDB;Database=StudentDb;Trusted_Connection=True;TrustServerCertificate=True;"
```

### 2. Run Migrations

```bash
dotnet ef migrations add InitialCreate \
  --project StudentManagementSystem.Infrastructure \
  --startup-project StudentManagementSystem.API
```

```bash
dotnet ef database update \
  --project StudentManagementSystem.Infrastructure \
  --startup-project StudentManagementSystem.API
```

---

## ▶️ Run the Project

```bash
dotnet restore
dotnet build
dotnet run --project StudentManagementSystem.API
```

---

## 🌐 Swagger UI

Once the project runs, open your browser at:

```
https://localhost:{port}/swagger
```

---

## 🔐 JWT Authentication

### 1. Login

```http
POST /api/auth/login
```

**Request:**

```json
{
  "email": "admin@test.com",
  "password": "123456"
}
```

**Response:**

```json
{
  "statusCode": 200,
  "message": "Login Successful",
  "result": {
    "token": "eyJhbGciOiJIUzI1NiIs..."
  }
}
```

### 2. Use Token in Swagger

Click 👉 **Authorize Button (🔒)** and enter:

```
Bearer YOUR_TOKEN
```

---

## 📌 API Endpoints

### 👨‍🎓 Students

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/students` | Get all students |
| `GET` | `/api/students/{id}` | Get student by ID |
| `POST` | `/api/students` | Create student |
| `PUT` | `/api/students/{id}` | Update student |
| `DELETE` | `/api/students/{id}` | Delete student |

---

## ⚠️ Global Exception Handling

All errors are handled centrally using middleware:

```json
{
  "statusCode": 500,
  "message": "Error message",
  "result": null
}
```

---

## 🧪 Validation

Implemented using **FluentValidation**:

| Field | Rule |
|-------|------|
| `Name` | Required, Max 100 chars |
| `Email` | Valid email format |
| `Age` | Between 5 – 100 |
| `Course` | Required |

---

## 🧱 Tech Stack

| Technology | Purpose |
|---|---|
| .NET 9 | Runtime |
| ASP.NET Core Web API | HTTP layer |
| Entity Framework Core | ORM |
| SQL Server | Database |
| MediatR | CQRS implementation |
| FluentValidation | Input validation |
| JWT Authentication | Security |
| Swagger / Swashbuckle | API documentation |

---

## 📁 Project Structure

```
StudentManagementSystem
│
├── StudentManagementSystem.API
│   ├── Controllers/
│   └── Middleware/
│
├── StudentManagementSystem.Application
│   ├── Commands/
│   ├── Queries/
│   ├── DTOs/
│   └── Validators/
│
├── StudentManagementSystem.Infrastructure
│   ├── Persistence/        (DbContext)
│   ├── Services/
│   └── Repositories/
│
└── StudentManagementSystem.Domain
    └── Entities/
```

---

## 👨‍💻 Author

**Bhanwarlal Prajapat**

---

## ⭐ Notes

This project demonstrates:

- ✅ Clean Architecture principles
- ✅ Scalable backend design
- ✅ Real-world API structure
- ✅ Secure JWT authentication
- ✅ Production-ready coding standards

---

## 🚀 Future Improvements

- [ ] Role-based Authorization (Admin / User)
- [ ] Refresh Token system
- [ ] Pagination & Filtering
- [ ] Unit Testing (xUnit)
- [ ] Docker support
- [ ] Frontend integration (React / Angular)

# 👥 MemberService API

A lightweight, cloud-ready RESTful service to manage **Member records**. This project is built using **.NET 6 Web API** with **SQLite** as the database and follows professional architectural practices including layered services, repository pattern, error handling middleware, and Swagger documentation.

---

## 🚀 Features

- Retrieve all members
- Update member details
- Delete a member
- RESTful architecture
- Cloud deployable (Render/Railway)
- Well-structured and extensible codebase

---

## 🧱 Tech Stack

- .NET 6 Web API
- Entity Framework Core
- SQLite
- Swagger (OpenAPI)
- Optional: Render or Railway for deployment

---

## 📦 Project Structure

```
MemberService.Api/
├── Controllers/
├── Models/
├── Data/
├── Services/
├── Repositories/
├── Middleware/
└── Program.cs
```

---

## 🎯 API Endpoints

| Method | Route               | Description            |
| ------ | ------------------- | ---------------------- |
| GET    | `/api/members`      | Get all member records |
| PUT    | `/api/members/{id}` | Update member by ID    |
| DELETE | `/api/members/{id}` | Delete member by ID    |

---

## 🔧 Running the App Locally

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download)
- [SQLite](https://www.sqlite.org/download.html)
- [EF Core CLI](https://docs.microsoft.com/en-us/ef/core/cli/dotnet)

### Steps

1. Clone the repository:

   ```bash
   git clone https://github.com/your-username/MemberService.Api.git
   cd MemberService.Api
   ```

2. Restore packages:

   ```bash
   dotnet restore
   ```

3. Run EF migrations (after you create them):

   ```bash
   dotnet ef database update
   ```

4. Run the application:

   ```bash
   dotnet run
   ```

5. Navigate to:
   ```
   https://localhost:5001/swagger
   ```

---

## 🌍 Deployment

This project can be deployed to:

- [Render](https://render.com/)
- [Railway](https://railway.app/)
- Azure App Service

> For deployment, ensure you configure the `members.db` file as a persistent volume or use a production-grade DB.

---

## 💼 Explaining to Customers

This API acts as the digital backbone of a membership system — ideal for employee directories, club management systems, or CRM integrations. It offers a clean, extensible foundation for managing member data with full CRUD capabilities and clean separation of concerns, ensuring maintainability and scalability.

---

## ✅ Future Improvements

- Add authentication and authorization
- Pagination for GET endpoints
- Create and Read single member endpoints
- Add search/filter capabilities
- Unit and integration testing

---

## 🪪 License

This project is licensed under the MIT License.

---

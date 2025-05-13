# 👥 MemberService API

A lightweight, cloud-ready RESTful service to manage **Member records**. This project is built using **.NET 8 Web API** with **SQLite** as the database and follows professional architectural practices including layered services, repository pattern, error handling middleware, and Swagger documentation.

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

- .NET 8 Web API
- Entity Framework Core
- SQLite
- Swagger (OpenAPI)
- Render or Railway for deployment

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

| Method | Route               | Description              |
| ------ | ------------------- | ------------------------ |
| GET    | `/api/members`      | Get all member records   |
| GET    | `/api/members/{id}` | Get member records by ID |
| PUT    | `/api/members/{id}` | Update member by ID      |
| POST   | `/api/members/{id}` | Delete member by ID      |
| PATCH  | `/api/members/{id}` | Delete member by ID      |
| DELETE | `/api/members/{id}` | Delete member by ID      |

---

## 🔧 Running the App Locally

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
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

This project is hosted live on Render:

🔗 **Live API**: [https://autodesk-test.onrender.com/swagger/index.html](https://autodesk-test.onrender.com/swagger/index.html)

Render auto-builds the project using Docker. The SQLite database is recreated on each deployment (non-persistent) for demonstration purposes.

### 🐳 Docker Configuration

To run the API locally using Docker:

1. Build the Docker image:

   ```bash
   docker build -t member-service-api .
   ```

2. Run the container:

   ```bash
   docker run -d -p 8080:8080 member-service-api
   ```

3. Open in browser:
   ```
   http://localhost:8080/swagger
   ```

> SQLite is configured to use `/tmp/member.db` as the database path for compatibility with Docker and Render's ephemeral file system.

---

## 📘 For Developers Consuming This API

This API acts as the digital backbone of a membership system — ideal for employee directories, club management systems, or CRM integrations. It offers a clean, extensible foundation for managing member data with full CRUD capabilities and clean separation of concerns, ensuring maintainability and scalability.

If you're building a frontend or integrating with this API, here are some helpful notes:

- All endpoints are RESTful and return standard HTTP status codes (`200`, `201`, `204`, `404`).
- Requests and responses use `application/json`.
- Use `/api/members` to retrieve or manage members.
- To update a member:
  - Use `PUT` to fully replace the record.
  - Use `PATCH` to modify only selected fields.
- Swagger UI is enabled at `/swagger` with full schema documentation.
- Authentication is not required (yet), making it easy to test.

Let us know if your application needs additional endpoints or features.

---

## ✅ Future Improvements

- Add authentication and authorization
- Pagination for GET endpoints
- Add search/filter capabilities

---

## 🪪 License

This project is licensed under the MIT License.

---

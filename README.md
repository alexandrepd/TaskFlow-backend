# TaskFlow API

A RESTful task management API built with **.NET 8** and **Clean Architecture**, featuring JWT authentication, role-based access control, and full Docker support.

---

## Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Tech Stack](#tech-stack)
- [API Endpoints](#api-endpoints)
- [Environment Variables](#environment-variables)
- [Running with Docker](#running-with-docker)
- [Local Development](#local-development)
- [Database Migrations](#database-migrations)
- [Project Structure](#project-structure)
- [Branch History](#branch-history)

---

## Overview

TaskFlow is a task management system where authenticated users can create, update, and track their personal tasks. An admin role allows managing all registered users. The API follows Clean Architecture principles with CQRS via MediatR.

---

## Architecture

```
TaskFlow.API            → Controllers, Middleware, DI configuration
TaskFlow.Application    → CQRS Handlers, Validators, DTOs
TaskFlow.Domain         → Entities, Interfaces (no external dependencies)
TaskFlow.Infrastructure → EF Core, Repositories, Services, Migrations
```

Each layer depends only on inner layers. The Domain has zero framework dependencies.

---

## Tech Stack

| Technology | Version | Purpose |
|---|---|---|
| .NET | 8.0 | Runtime |
| ASP.NET Core | 8.0 | Web API framework |
| Entity Framework Core | 8.x | ORM |
| SQL Server | 2022 | Database |
| MediatR | 12.x | CQRS pipeline |
| FluentValidation | 11.x | Request validation |
| BCrypt.Net-Next | 4.0.3 | Password hashing |
| Serilog | 3.x | Structured logging |
| Swagger / Swashbuckle | — | API documentation (dev only) |
| Docker | — | Containerization |

---

## API Endpoints

### Authentication

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| `POST` | `/api/auth/login` | Public | Authenticate and receive a JWT |
| `POST` | `/api/auth/register` | Public | Register a new user account |
| `GET` | `/api/auth/users` | Admin only | List all registered users |

**Login / Register request body:**
```json
{
  "username": "john_doe",
  "password": "Secret123"
}
```

**Login / Register response:**
```json
{
  "token": "<jwt>"
}
```

### Tasks

All task endpoints require a valid `Authorization: Bearer <token>` header.

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/tasks` | List all tasks for the authenticated user |
| `POST` | `/api/tasks` | Create a new task |
| `GET` | `/api/tasks/{id}` | Get a specific task |
| `PUT` | `/api/tasks/{id}` | Update a task |
| `DELETE` | `/api/tasks/{id}` | Delete a task |

**Task fields:** `title`, `description`, `status` (`Pending` / `InProgress` / `Completed`), `priority` (`Low` / `Medium` / `High`), `dueDate`.

---

## Environment Variables

The following variables must be set at runtime. In production, provide them via the host environment or a secrets manager. **Never commit real values to source control.**

| Variable | Description | Example |
|---|---|---|
| `JWT_KEY` | Secret key for signing JWTs (≥ 32 chars) | `your-very-long-secret-key` |
| `JWT_ISSUER` | JWT issuer claim | `TaskFlow` |
| `JWT_EXPIRE_DAYS` | Token validity in days | `1` |
| `TASKFLOW_ADMIN_PASSWORD` | Password for the seeded `admin` account | `AdminPass!2024` |
| `ConnectionStrings__DefaultConnection` | SQL Server connection string | `Server=db,1433;...` |

For **local development only**, values can be placed in `TaskFlow.API/appsettings.Development.json` (already git-ignored from secrets).

---

## Running with Docker

> Prerequisites: Docker and Docker Compose installed.

```bash
# Clone the repository
git clone <repo-url>
cd TaskFlow

# Set the required environment variables in your shell (or a .env file)
export JWT_KEY="your-very-long-secret-key-here"
export JWT_ISSUER="TaskFlow"
export JWT_EXPIRE_DAYS="1"
export TASKFLOW_ADMIN_PASSWORD="AdminPass!2024"
export SA_PASSWORD="YourStrong@Password"

# Start all services (API + SQL Server)
docker compose up --build
```

The API will be available at **http://localhost:4000**.  
Swagger UI (development profile): **http://localhost:4000/swagger**

To stop and remove all containers and volumes:

```bash
docker compose down -v
```

---

## Local Development

> Prerequisites: .NET 8 SDK, SQL Server (local or Docker).

```bash
# Restore dependencies
dotnet restore

# Run the API
dotnet run --project TaskFlow.API
```

The API starts on `http://localhost:5105` by default (see `launchSettings.json`).

---

## Database Migrations

```bash
# Apply all pending migrations
dotnet ef database update --project TaskFlow.Infrastructure --startup-project TaskFlow.API

# Create a new migration
dotnet ef migrations add <MigrationName> --project TaskFlow.Infrastructure --startup-project TaskFlow.API

# Rollback to a specific migration
dotnet ef database update <MigrationName> --project TaskFlow.Infrastructure --startup-project TaskFlow.API
```

---

## Project Structure

```
TaskFlow/
├── TaskFlow.API/
│   ├── Controllers/        # AuthController, TasksController
│   ├── Extensions/         # JWT, Logging service extensions
│   ├── Middleware/         # Global exception handler
│   └── Program.cs
├── TaskFlow.Application/
│   ├── Common/
│   │   ├── Behaviors/      # MediatR validation pipeline
│   │   └── Exceptions/     # NotFoundException
│   ├── Features/
│   │   ├── Auth/           # Login, Register, GetUsers handlers + validators
│   │   └── Tasks/          # CRUD handlers + validators
│   └── Extensions/         # MediatR DI registration
├── TaskFlow.Domain/
│   ├── Entities/           # User, TaskItem
│   └── Interfaces/         # IUserRepository, ITaskRepository, IPasswordHasher, IJwtTokenService
└── TaskFlow.Infrastructure/
    ├── Data/               # TaskDbContext, DatabaseInitializer
    ├── Migrations/
    ├── Repositories/       # UserRepository, TaskRepository
    └── Services/           # JwtTokenService, BcryptPasswordHasher
```

---

## Branch History

| Branch | Description |
|---|---|
| `chore/taskflow-architecture-hardening` | Clean Architecture refactor, CQRS, validation pipeline, exception middleware |
| `feature/user-registration` | `POST /api/auth/register` endpoint with FluentValidation |
| `feature/admin-users-panel` | `GET /api/auth/users` endpoint restricted to Admin role |


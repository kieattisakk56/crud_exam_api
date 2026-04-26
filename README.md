# 🚀 Project 63 - Full-Stack Recruitment Exam

[![.NET Core](https://img.shields.io/badge/.NET%20Core-9.0-512BD4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![Angular](https://img.shields.io/badge/Angular-21-DD0031?style=for-the-badge&logo=angular)](https://angular.io/)
[![Architecture](https://img.shields.io/badge/Architecture-Clean%20%26%20CQRS-blue?style=for-the-badge)](https://github.com/dotnet-architecture/eShopOnContainers)

A comprehensive recruitment test project showcasing modern full-stack development practices, featuring a high-performance **.NET Core Web API** and a dynamic **Angular** frontend.

---

## 🏗️ Project Architecture

The system is built using **Clean Architecture** principles to ensure maintainability, scalability, and testability.

### 🔹 Backend (.NET Core 9)
- **Domain**: Pure business logic, entities, and repository interfaces.
- **Application**: CQRS implementation with **MediatR**, validation logic, and DTOs.
- **Infrastructure**: Data access layer with **Entity Framework Core**, Unit of Work, and external services.
- **WebApi**: RESTful API controllers with standard response wrapping.

### 🔹 Frontend (Angular 21)
- **Modern UI**: Styled with **Tailwind CSS** for a premium, responsive look.
- **State Management**: Reactive programming with RxJS.
- **Components**: Reusable UI components and specific test pages (Test 01 - 10).

---

## 🛠️ Tech Stack

| Category | Technology |
| :--- | :--- |
| **Backend** | .NET 9.0, EF Core, MediatR, SQLite / SQL Server |
| **Frontend** | Angular 21, Tailwind CSS, TypeScript |
| **Patterns** | CQRS, Repository Pattern, Unit of Work, Dependency Injection |
| **Tools** | Swagger/OpenAPI, Git, Postman |

---

## 🌟 Key Features (Test Modules)

This project contains 10 specific test scenarios, each focusing on different technical aspects:

- **01 - Data Entry**: Basic CRUD with validation.
- **02 - Authentication**: JWT-based login system.
- **03 - Document Approval**: Workflow-based status management.
- **04 - Profile Management**: Image handling (Base64) and complex forms.
- **05 - Ticketing System**: Real-time queue management logic.
- **06 - Barcode Integration**: Unique constraint handling and generation.
- **07 - QR Code Tracking**: Secure tracking with unique identifiers.
- **08 - Examination Engine**: Dynamic question/answer handling.
- **09 - Social Interaction**: Commenting system with nested relationships.
- **10 - Result Analytics**: Data aggregation and reporting.

---

## 🚀 Getting Started

### Prerequisites
- .NET 9.0 SDK
- Node.js (Latest LTS)
- Angular CLI

### 1. Setup Backend
```bash
cd apis
dotnet restore
dotnet run --project ProjectApi.WebApi
```

### 2. Setup Frontend
```bash
cd web
npm install
ng serve
```

---

## 📂 Project Structure

```text
63-Test/
├── apis/                  # .NET Core Solution
│   ├── ProjectApi.Domain/          # Core Domain Layer
│   ├── ProjectApi.Application/     # Application Logic (CQRS)
│   ├── ProjectApi.Infrastructure/  # Data & Services
│   └── ProjectApi.WebApi/          # API Controllers
└── web/                   # Angular Application
    ├── src/app/pages/              # Test pages (01-10)
    └── src/app/services/           # API integration services
```

---

## 👤 Author
**Kieattisakk**
*Recruitment Test Project*
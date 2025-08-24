
# 🏗️ Modular Monolith (Layered) — Project Template

[![Template](https://img.shields.io/badge/template-modular--monolith-blue.svg)](https://github.com)
[![Architecture](https://img.shields.io/badge/architecture-layered-yellowgreen.svg)](https://github.com)
[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=.net)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE)

> 🚀 **A lightweight, reusable template demonstrating a modular-monolith (feature modules) with a layered architecture. It groups features into reusable projects while keeping a single solution for easy local development and gradual extraction into microservices.**

---

## 📋 Table of Contents

- [🎯 What this repo is](#-what-this-repo-is)
- [🏛️ Architecture overview](#️-architecture-overview)
- [📋 Design contract](#-design-contract-mini-contract)
- [🗂️ Project layout](#️-project-layout-map-to-files)
- [🚀 How to build and run](#-how-to-build-and-run-local-developer-flow)
- [⚙️ Configuration and cross-cutting concerns](#️-configuration-and-cross-cutting-concerns)
- [🧪 Testing strategy](#-testing-strategy)
- [🚢 Deployment notes](#-deployment-notes)
- [📊 ASCII diagram](#-ascii-diagram-high-level)
- [🤝 Contributing and extending](#-contributing-and-extending)
- [🔧 Troubleshooting tips](#-troubleshooting-tips)
- [📝 Next steps / recommended TODOs](#-next-steps--recommended-todos)
- [📄 Files of interest](#-files-of-interest)

---

## 🎯 What this repo is

This repository is a **reusable template** for building modular-monolithic applications using a layered architecture. It groups related features into self-contained projects/modules while keeping them inside a single solution for simpler local development and deployment.

### 🔑 Key Components

| Component                    | Description                                                   | Purpose                          |
| ---------------------------- | ------------------------------------------------------------- | -------------------------------- |
| 🔁 **Gateway**                | API gateway / edge project (Ocelot configuration)             | Routes and request edge concerns |
| 🏛️ **Nucleus.Api**            | Main API surface: controllers, request handling and DI wiring | Central API orchestration        |
| 🗄️ **Nucleus.Databases**      | Database context and EF Core related types                    | Data persistence layer           |
| 🧩 **Nucleus.Infrastructure** | Workflow and repository implementations                       | Business logic foundation        |
| 📚 **Nucleus.Models/Dtos**    | Domain models and DTO projects                                | Data contracts                   |
| 🛠️ **Nucleus.Utilities**      | Helpers: encryption, extensions, and utilities                | Cross-cutting concerns           |
| 🚀 **Services/Auth**          | Example microservice module                                   | Feature-oriented service         |

### 🧱 Infrastructure Components

#### 📌 **Repository Pattern**
| Component                | Purpose                    |
| ------------------------ | -------------------------- |
| 🔸 **NucleusIRepository** | Repository interfaces      |
| 🔸 **NucleusRepository**  | Repository implementations |
| 🔸 **Nucleus.Repository** | Concrete persistence logic |

#### 🔄 **Workflow Pattern**
| Component              | Purpose                  |
| ---------------------- | ------------------------ |
| 🔸 **NucleusIWorkflow** | Workflow contracts       |
| 🔸 **INucleusWorkflow** | Workflow interfaces      |
| 🔸 **Nucleus.Workflow** | Workflow implementations |

> 💡 **Note:** The `Nucleus.*` projects are common/shared libraries for reuse across multiple services. The `Services/` folder contains independently runnable microservices.

---

## 🏛️ Architecture overview

This codebase follows **two key architectural principles**:

### 1️⃣ Modular Monolith
```
📦 Feature-oriented modules as separate projects
├── 🎯 Clear boundaries without operational overhead
├── 🔧 Easy local development
└── 🚀 Gradual extraction to microservices
```

### 2️⃣ Layered Architecture
```
🏗️ Vertical layers inside modules
├── 🌐 API Layer           → Controllers, request/response handling
├── ⚙️ Application Layer   → Business use cases orchestration
├── 🧠 Domain Layer        → Domain logic and workflows
├── 💾 Data Layer          → Persistence, EF Core contexts
└── 🔧 Utilities Layer     → Cross-cutting concerns
```

### 🔄 Benefits

| ✅ Benefit        | 📝 Description                                    |
| ---------------- | ------------------------------------------------ |
| **Testable**     | Clear separation enables unit testing            |
| **Portable**     | Easy module extraction to separate services      |
| **Maintainable** | Organized code with clear responsibilities       |
| **Scalable**     | Gradual evolution from monolith to microservices |

---

## 📋 Design contract (mini-contract)

### 📥 Inputs
- 🌐 HTTP requests via `Gateway` or `Nucleus.Api` controllers

### 📤 Outputs
- 📊 HTTP JSON responses (standardized `ApiResponse` type)
- 💾 Database side-effects

### ⚠️ Error modes
| Status        | Description                  | Handler Location      |
| ------------- | ---------------------------- | --------------------- |
| 🔴 **400**     | Validation errors            | `Nucleus.Api/Filters` |
| 🔐 **401/403** | Authentication/Authorization | `Nucleus.Api/Filters` |
| 🔍 **404**     | Not Found                    | `Nucleus.Api/Filters` |
| 💥 **5xx**     | Server errors                | `Nucleus.Api/Filters` |

### ✅ Success criteria
- ✔️ Request handled within API layer
- ✔️ Delegated to service/workflow layer
- ✔️ Persistence saved by repository layer
- ✔️ Idempotent behavior
- ✔️ Consistent error handling

---

## 🗂️ Project layout (map to files)

### 🏗️ Core Projects

#### 🔁 **Gateway**
```
📁 Gateway/
├── 📄 Gateway/Gateway.csproj
├── ⚙️ ocelot.json
└── 📁 Ocelot/
```

#### 🏛️ **Nucleus API**
```
📁 Nucleus.Api/
├── 📄 Nucleus.Api.csproj
├── 🎛️ Filters/
├── 🔧 Dependency/
└── 📊 Controllers/
```

#### 🗄️ **Database Layer**
```
📁 Nucleus.Databases/
├── 📄 Nucleus.Databases.csproj
├── 🗃️ MainDatabaseContext.cs
└── 🔑 Configurations/
```

#### 🧩 **Infrastructure Layer**
```
📁 Nucleus.Infrastructure/
├── 📌 NucleusIRepository/
│   └── 📄 Nucleus.IRepository.csproj
├── 🏪 NucleusRepository/
│   └── 📄 Nucleus.Repository.csproj
├── 🔄 NucleusIWorkflow/
│   └── 📄 Nucleus.IWorkflow.csproj
└── ⚡ INucleusWorkflow/
    └── 📄 Nucleus.Workflow.csproj
```

#### 📚 **Models & DTOs**
```
📁 Nucleus.Models/
├── 📄 Nucleus.Models.csproj
└── 📊 Domain entities

📁 Nucleus.Dtos/
├── 📄 Nucleus.Dtos.csproj
└── 📋 Data transfer objects
```

#### 🛠️ **Utilities**
```
📁 Nucleus.Utilities/
├── 📄 Nucleus.Utilities.csproj
├── 🔐 AesEncryption.cs
└── 🔧 Extensions/
```

#### 🚀 **Example Service Module**
```
📁 Services/Auth/src/
├── 🎯 Service/
├── 🏪 Repository/
├── 🔄 Workflow/
├── 📋 Dtos/
└── 📚 Models/
```

---

## 🚀 How to build and run (local developer flow)

### 🛠️ Prerequisites

| Requirement    | Version               | Purpose                 |
| -------------- | --------------------- | ----------------------- |
| 🔷 **.NET SDK** | 8.x                   | Runtime and compilation |
| 🐳 **Docker**   | Latest (Optional)     | Containerized runs      |
| 🗄️ **Database** | PostgreSQL/SQL Server | Data persistence        |

### 📋 Basic steps (CLI)

#### 1️⃣ **Restore and Build**
```bash
# 📁 From repository root (where Balerionv2.sln is located)
dotnet restore Balerionv2.sln
dotnet build Balerionv2.sln -c Debug
```

#### 2️⃣ **Run API Project**
```bash
# 🚀 Run Nucleus.Api for quick dev iteration
cd Nucleus.Api
dotnet run --project Nucleus.Api.csproj
```

#### 3️⃣ **Run Gateway**
```bash
# 🔁 Run Gateway to route external traffic
cd Gateway/Gateway
dotnet run --project Gateway.csproj
```

### 🗄️ Database and migrations

#### 🔧 **Configuration**
- 📝 Check `Nucleus.Databases/MainDatabaseContext.cs` for DB provider settings
- ⚙️ Configure connection strings in `appsettings.json` or user secrets

#### 📊 **EF Core Commands**
```bash
# ➕ Add migration
dotnet ef migrations add InitialCreate \
  --project Nucleus.Databases \
  --startup-project Nucleus.Api

# 🔄 Apply migrations
dotnet ef database update \
  --project Nucleus.Databases \
  --startup-project Nucleus.Api
```

> 💡 **Tip:** Adjust `--startup-project` to the project that configures DbContext at runtime.

---

## ⚙️ Configuration and cross-cutting concerns

### 🔐 **Authentication & JWT**
- 📄 `Nucleus.Api/Dependency/JwtDependency.cs`
- 🎫 `Nucleus.Models/JwtSettings.cs`

### 📝 **Logging**
- 📊 Serilog: `Nucleus.Api/Dependency/SerilogDependency.cs`
- 🖥️ UI Support: `SerilogUiDependency.*`

### 🎛️ **Filters**
- 🔐 Authentication: `Nucleus.Api/Filters/AuthenticationAttribute.cs`
- ✅ Validation: `Nucleus.Api/Filters/ModelValidatorAttribute.cs`

### 📊 **Request/Response Shape**
- 🏷️ Standard wrapper: `Nucleus.Models/ApiResponse.cs`

### 🔒 **Encryption**
- 🛡️ AES: `Nucleus.Utilities/AesEncryption.cs`
- 🏷️ Attribute: `AesDecryptAttribute.cs`

---

## 🧪 Testing strategy

### 🏗️ **Recommended Test Structure**
```
🧪 Testing Pyramid
├── 🔬 Unit Tests         → Service layer business rules
├── 🔗 Integration Tests  → Repository against test DB
└── 🌐 End-to-End Tests   → HTTP tests via Nucleus.Api
```

### 📍 **Suggested Test Placement**
- 📁 `Services/<Module>/tests/` — Module-specific tests
- 📁 `tests/` — Top-level shared test folder

### 🎯 **Minimal Tests to Add**
- ✅ **Service layer unit tests** — Business rules validation
- 🗄️ **Repository integration tests** — Database operations
- 🌐 **End-to-end tests** — Full HTTP request/response cycle

---

## 🚢 Deployment notes

### 🏠 **Small-scale Deployment**
```
🔧 Simple Setup
├── 📦 Publish Nucleus.Api
├── 🔁 Publish Gateway
└── 🌐 Deploy behind reverse proxy/load balancer
```

### 🐳 **Containerization**
```dockerfile
📋 Add per project:
├── 📄 Dockerfile (per project)
└── 📄 docker-compose.yml (at repo root)
```

### ☁️ **Cloud Migration**
```
🚀 Microservices Evolution
├── 📦 Extract modules to separate solutions
├── 🔍 Add service discovery
└── ⚙️ Add distributed configuration
```

---

## 📊 ASCII diagram (high level)

```
                    🌐 TRAFFIC FLOW
    
┌─────────────┐       ┌───────────┐      ┌────────────────┐
│   👥 Clients │ ◄──── │ 🔁 Gateway │ ──── │ 🏛️ Nucleus.Api │
└─────────────┘       └───────────┘      └────────────────┘
                                               │     │     │
                                               ▼     ▼     ▼
                    ┌─────────────┐    ┌─────────────┐    ┌──────────────┐
                    │ 🚀 Services │    │ 🧩 Workflows │    │ 🗄️ Nucleus.DB │
                    │   Auth,     │    │  Business   │    │  (EF Core)   │
                    │   ModuleA,  │    │   Logic     │    │              │
                    │   ModuleB   │    │             │    │              │
                    └─────────────┘    └─────────────┘    └──────────────┘
                           │                   │                   ▲
                           └───────────────────┼───────────────────┘
                                              ▼
                                    ┌─────────────────┐
                                    │ 🏪 Repository   │
                                    │   Pattern       │
                                    └─────────────────┘

🏗️ LAYER LEGEND:
├── 🔁 Gateway     → Edge routing
├── 🏛️ API         → Controllers
├── 🚀 Services    → Business logic
└── 🏪 Repository  → Data persistence
```

---

## 🤝 Contributing and extending

### ➕ **Adding New Feature Module**

#### 📁 **Recommended Structure**
```
📁 Services/YourNewModule/
├── 🎯 Service/           → Controllers, DI wiring
├── 🏪 Repository/        → Persistence contracts
├── 📋 IRepository/       → Repository interfaces
├── 🔄 Workflow/          → Domain workflows
├── ⚡ IWorkflow/         → Workflow interfaces
├── 📋 Dtos/             → Data transfer objects
└── 📚 Models/           → Domain models
```

#### 🔧 **Integration Steps**
1. 📦 Create folder under `Services/`
2. 🏗️ Follow suggested project structure
3. 🔗 Keep dependencies inverted (use interfaces)
4. ⚙️ Add DI configuration in `Nucleus.Api/Dependency/ServiceDependency.cs`

### 💡 **Best Practices**
- ✅ Use dependency inversion (depend on `IRepository`, not concrete implementations)
- 📝 Follow consistent naming conventions
- 🧪 Add tests for new modules
- 📚 Update documentation

---

## 🔧 Troubleshooting tips

### 🚨 **Common Issues & Solutions**

| 🔴 Issue                    | 🔍 Root Cause             | 💡 Solution                                      |
| -------------------------- | ------------------------ | ----------------------------------------------- |
| **500/Unhandled Errors**   | Server exceptions        | Check `SerilogAuthenticationFilter.cs` and logs |
| **DB Connection Failures** | Configuration mismatch   | Verify connection strings in `appsettings.json` |
| **Routing Errors**         | Gateway misconfiguration | Check `Gateway/ocelot.json` host/port mapping   |
| **Authentication Issues**  | JWT configuration        | Review `JwtDependency.cs` settings              |
| **Migration Failures**     | EF Core context issues   | Verify `MainDatabaseContext` configuration      |

### 📊 **Debug Resources**
- 📝 **Logs**: Serilog output and filters
- 🔧 **Configuration**: `appsettings.json` files
- 🌐 **Network**: Gateway routing configuration
- 🗄️ **Database**: EF Core connection strings

---

## 📝 Next steps / recommended TODOs

### 🎯 **High Priority**
- [ ] 🧪 **Add unit and integration test projects** for `Auth` module
- [ ] 🐳 **Add docker-compose.yml** to orchestrate DB + API + Gateway
- [ ] 🔄 **Add CI pipeline** for automated build and test execution

### 🚀 **Medium Priority**
- [ ] 💚 **Add health endpoints** and readiness/liveness probes
- [ ] 📊 **Add monitoring and metrics** collection
- [ ] 🔐 **Enhance security** with rate limiting and input validation

### 🌟 **Future Enhancements**
- [ ] 📈 **Performance optimization** and caching strategies
- [ ] 🌍 **Multi-environment** configuration management
- [ ] 📚 **API documentation** with Swagger/OpenAPI

---

## 📄 Files of interest

### 🗂️ **Essential Files**

| 📂 Category        | 📄 File                                     | 🎯 Purpose                          |
| ----------------- | ------------------------------------------ | ---------------------------------- |
| **Solution**      | `Balerionv2.sln`                           | Main solution file                 |
| **Gateway**       | `Gateway/ocelot.json`                      | API gateway routes                 |
| **Configuration** | `Nucleus.Api/Dependency/*`                 | DI wiring (JWT, Serilog, services) |
| **Database**      | `Nucleus.Databases/MainDatabaseContext.cs` | EF Core DB context                 |
| **API Response**  | `Nucleus.Models/ApiResponse.cs`            | Standard response wrapper          |
| **Security**      | `Nucleus.Utilities/AesEncryption.cs`       | Encryption utilities               |

---

<div align="center">





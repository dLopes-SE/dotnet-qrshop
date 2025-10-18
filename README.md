# 🛒 QRShop (.NET Portfolio Project)

## 📘 Index
1. [Overview](#overview)  
2. [Tech Stack](#tech-stack)  
3. [Architecture](#architecture)  
4. [CI/CD Workflow](#cicd-workflow)  
5. [Running the Demo](#running-the-demo)  
6. [Roadmap](#roadmap)  
7. [License](#license)

---

## 🧩 Overview

**QRShop** is an ongoing **.NET portfolio project** designed to explore clean, maintainable, and scalable application design.  
It serves as a sandbox for implementing production-grade concepts such as **Domain Events**, **Vertical Slice Architecture**, and **asynchronous messaging** with **RabbitMQ** (future implementation).

To visualize and interact with this backend, a corresponding frontend has been implemented using Next.js: [nextjs-qrshop](https://github.com/dLopes-SE/nextjs-qrshop).  
Additionally, a **demo version** of the project is available, as described in the **Demo** section below.

The project is still **in active development**, with multiple open issues reflecting ongoing improvements and planned features.

---

## ⚙️ Tech Stack

- **.NET 8 / C#**
- **Entity Framework Core** — ORM and migrations  
- **PostgreSQL** — primary database  
- **Docker & Docker Compose** — containerization and local orchestration  
- **xUnit** — testing framework  
- **GitHub Actions** — CI/CD pipeline  
- **(Planned)** RabbitMQ — message-based communication  
- **(Planned)** Redis — distributed caching  

---

## 🏗️ Architecture

QRShop follows a **Vertical Slice Architecture** (feature-based design), focusing on organizing code by behavior rather than technical layers.  

### Folder Overview:
- **`Features/`** – Core business use cases, each encapsulating commands, queries, and handlers  
- **`Domains/`** – Domain models and business logic  
- **`Infrastructure/`** – Database, external services, and persistence setup  
- **`Common/`** and **`Abstractions/`** – Shared utilities, base classes, and abstractions  
- **`Services/`** – Supporting application services and cross-cutting concerns  

### In Progress:
- **Domain Events** – to decouple domain logic and react to business changes  
- **Output Pattern** – standardizing responses and error handling  

This approach enhances modularity and testability while keeping the system extensible.

---

## 🚀 CI/CD Workflow

A **GitHub Actions** workflow automatically builds, tests, and publishes a **public Docker image** for QRShop.  
This enables fast demo setup and continuous delivery of new versions.

---

## 💻 Running the Demo

You can quickly spin up a demo instance of QRShop using the provided scripts — no manual setup required.

**Windows:**
```powershell
powershell -ExecutionPolicy Bypass -Command "iwr -useb https://raw.githubusercontent.com/dlopes-se/dotnet-qrshop/main/scripts/run-qrshop.ps1 | iex"
```

**macOS / Linux:**
```
bash <(curl -fsSL https://raw.githubusercontent.com/dlopes-se/dotnet-qrshop/main/scripts/run-qrshop.sh)
```

These scripts automatically download and run the latest Docker image published by the CI/CD pipeline.

---

## 🗺️ Roadmap

- [ ] Integrate RabbitMQ for event-driven workflows  
- [ ] Add Redis caching layer  
- [ ] Implement Domain Events pattern  
- [ ] Finalize Output Pattern  
- [ ] Expand test coverage (unit + integration)  
- [ ] Extend CI/CD workflow (linting, static analysis)

---

## 🛒 Future Implementations Ideas

This part of the document describes some ideas for features and business rules for the e-commerce system (built with .NET 9 and (e.g.) Rust as feature flag provider).

---

## 📌 Core Areas

### 1. Product Retirement (Scalable)
- **Goal:** Safely remove products from catalog while handling large numbers of users and orders.
- **Challenges:**
  - Products may exist in **millions of carts or checkouts**.
  - Need to maintain **integrity of historical orders**.
- **Approach:**
  - Introduce a **retirement status** (`Active`, `Retired`, `Archived`) instead of hard deletes.
  - Allow **retired products** to remain in historical orders but prevent new carts/checkouts from adding them.
  - Add **background job** to purge carts that contain retired products (optional cleanup).
- **Business Rule:**  
  - Retired products → visible in history, hidden from shop, not addable to cart.

---

### 2. Feature Flags (Real-time)
- **Goal:** Control system features dynamically without redeployments.
- **Provider:** Rust microservice exposing `/flags` endpoint (database-backed).
- **.NET Consumer:**
  - Implement `RustFeatureProvider` that calls Rust service.
  - Cache results locally with refresh interval (30–60 seconds).
- **Examples:**
  - `product_removal` → enable/disable product retirement.
  - `discount_engine` → toggle new pricing logic.
- **Future Enhancements:**
  - Role-based or user-segment targeting.
  - Percentage rollouts (canary releases).
  - Audit log of feature flag changes.


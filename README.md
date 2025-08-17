# 🛒 E-Commerce Future Implementations Roadmap

This part of the document describes planned features and business rules for the e-commerce system (built with .NET 9 and Rust as feature flag provider).

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

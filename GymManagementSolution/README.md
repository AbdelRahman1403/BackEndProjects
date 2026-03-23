# 💪 Gym Management Solution

[![.NET Version](https://img.shields.io/badge/.NET-9.0-blue.svg)](https://dotnet.microsoft.com/download/dotnet/9.0)
[![Architecture](https://img.shields.io/badge/Architecture-N--Tier-green.svg)](#architecture)

A comprehensive, enterprise-grade Gym Management System built with **.NET 9**. This solution is designed to streamline gym operations, ranging from member registration and subscription tracking to trainer scheduling and business analytics.

---

## 🚀 Project Overview

The **Gym Management Solution** provides a robust platform for gym owners to manage their facility efficiently. It features a modern web interface for administrators to track memberships, manage staff, and monitor real-time gym analytics. The system ensures data integrity through a strictly layered architecture and secure authentication protocols.

## 🛠️ Tech Stack

* **Framework:** [.NET 9.0 (ASP.NET Core MVC)](https://dotnet.microsoft.com/en-us/apps/aspnet/mvc)
* **Database:** SQL Server with Entity Framework Core
* **Authentication:** ASP.NET Core Identity (Role-based Authorization)
* **Object Mapping:** AutoMapper
* **Frontend:** Razor Views, Bootstrap 5, FontAwesome/Bootstrap Icons
* **Patterns:** Unit of Work, Generic Repository, Dependency Injection

## 🏗️ Architecture

The project follows an **N-Tier (Layered) Architecture** to ensure separation of concerns and maintainability:

1.  **GymManagementPL (Presentation Layer):** An ASP.NET Core MVC project containing controllers, views, and client-side assets.
2.  **BLL (Business Logic Layer):** Contains the core business rules, services (e.g., Member, Trainer, Analytics), and ViewModels.
3.  **DAL (Data Access Layer):** Manages database interactions using EF Core, including Migrations, Repositories, and the Unit of Work pattern.



## ✨ Features

* **👥 Member Management:** Full CRUD operations for gym members, including profile photo uploads and detailed health records.
* **💳 Membership Tracking:** Manage subscriptions with automated status tracking (Active/Expired) and plan assignments.
* **🏋️ Trainer Portal:** Track trainer specialties, contact information, and assigned training sessions.
* **📊 Business Analytics:** Real-time dashboard showing total active members, upcoming sessions, and trainer distribution.
* **📅 Session Scheduling:** Create and manage gym sessions with capacity limits and category assignments.
* **🛡️ Secure File Handling:** Custom `AttachmentServices` for secure image uploads with extension validation and 5MB size limits.

## 🧪 Testing & Validation

* **Server-side Validation:** Implemented using Data Annotations in ViewModels and `ModelState` checks in Controllers.
* **Database Constraints:** SQL-level constraints (e.g., `CHECK` constraints) for data integrity on fields like Phone (010xxxxxxxx), Name length, and Session capacity (1-25).
* **Logic Verification:** Services handle edge cases, such as preventing deletion of members with active memberships.

## 📂 Folder Structure

```text
GymManagementSolution/
├── BLL/                     # Business Logic Layer
│   ├── AttachmentServices/  # File upload logic
│   ├── Interfaces/          # Service Abstractions
│   ├── Profiles/            # AutoMapper Mappings
│   ├── Services/            # Service Implementations
│   └── ViewModels/          # Data Transfer Objects
├── DAL/                     # Data Access Layer
│   ├── Context/             # EF Core DbContext
│   ├── Migrations/          # Database Schema History
│   ├── Models/              # Database Entities (Member, Trainer, Plan, etc.)
│   └── Repositories/        # Unit of Work & Repos
├── GymManagementPL/         # Presentation Layer (MVC)
│   ├── Controllers/         # MVC Controllers
│   ├── Views/               # Razor Templates
│   └── wwwroot/             # Static Assets (CSS, JS, Images)
└── GymManagementSolution.sln

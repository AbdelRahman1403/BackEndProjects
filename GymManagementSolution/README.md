# 💪 Gym Management Solution

[![.NET Version](https://img.shields.io/badge/.NET-9.0-blue.svg)](https://dotnet.microsoft.com/download/dotnet/9.0)
[![Architecture](https://img.shields.io/badge/Architecture-N--Tier-green.svg)](#architecture)

A comprehensive, enterprise-grade Gym Management System built with **.NET 9**. [cite_start]This solution is designed to streamline gym operations, from member registration and subscription tracking to trainer scheduling and business analytics [cite: 30, 681-683].

---

## 🚀 Project Overview

[cite_start]The **Gym Management Solution** provides a robust platform for gym owners to manage their facility efficiently [cite: 681-683]. It features a modern web interface for administrators to track memberships, manage staff, and monitor real-time gym analytics. [cite_start]The system ensures data integrity through a strictly layered architecture and secure authentication protocols[cite: 677].

## 🛠️ Tech Stack

- [cite_start]**Framework:** [.NET 9.0 (ASP.NET Core MVC)](https://dotnet.microsoft.com/en-us/apps/aspnet/mvc) [cite: 30]
- [cite_start]**Database:** SQL Server with Entity Framework Core [cite: 296, 304]
- [cite_start]**Authentication:** ASP.NET Core Identity (Role-based Authorization) [cite: 299, 672-677]
- [cite_start]**Object Mapping:** AutoMapper [cite: 644-662]
- [cite_start]**Frontend:** Razor Views, Bootstrap 5, FontAwesome/Bootstrap Icons [cite: 541-544]
- [cite_start]**Patterns:** Unit of Work, Generic Repository, Dependency Injection [cite: 679-680, 686-687]

## 🏗️ Architecture

The project follows an **N-Tier (Layered) Architecture** to ensure separation of concerns and maintainability:

1.  [cite_start]**GymManagementPL (Presentation Layer):** An ASP.NET Core MVC project containing controllers, views, and client-side assets[cite: 486].
2.  [cite_start]**BLL (Business Logic Layer):** Contains the core business rules, services (e.g., Member, Trainer, Analytics), and ViewModels [cite: 602, 621-638].
3.  [cite_start]**DAL (Data Access Layer):** Manages database interactions using EF Core, including Migrations, Repositories, and the Unit of Work pattern [cite: 260-263, 296].

## ✨ Features

- [cite_start]**👥 Member Management:** Full CRUD operations for gym members, including profile photo uploads and detailed health records [cite: 625-627, 703-705].
- [cite_start]**💳 Membership Tracking:** Manage subscriptions with automated status tracking (Active/Expired) and plan assignments [cite: 628-630, 727-733].
- [cite_start]**🏋️ Trainer Portal:** Track trainer specialties, contact information, and assigned training sessions [cite: 637-639, 664-670].
- [cite_start]**📊 Business Analytics:** Real-time dashboard showing total active members, upcoming sessions, and trainer distribution [cite: 623, 681-683].
- [cite_start]**📅 Session Scheduling:** Create and manage gym sessions with capacity limits and category assignments [cite: 634-635, 780-782].
- [cite_start]**🛡️ Secure File Handling:** Custom `AttachmentServices` for secure image uploads with extension validation (.jpg, .jpeg, .png) and 5MB size limits [cite: 602-604, 608-609].

## 🧪 Testing & Validation

- [cite_start]**Server-side Validation:** Implemented using Data Annotations in ViewModels and `ModelState` checks in Controllers [cite: 222-223, 488].
- [cite_start]**Database Constraints:** SQL-level constraints for data integrity, such as email format checks, name length, and phone number validation (010xxxxxxxx)[cite: 261, 301].
- [cite_start]**Business Logic:** Services handle edge cases, such as preventing deletion of members with active memberships [cite: 716-717].

## 📂 Folder Structure

```text
GymManagementSolution/
├── BLL/                     # Business Logic Layer
│   ├── AttachmentServices/  # File upload logic [cite: 602]
│   ├── Interfaces/          # Service Abstractions [cite: 621]
│   ├── Profiles/            # AutoMapper Mappings [cite: 644]
│   ├── Services/            # Service Implementations [cite: 671]
│   └── ViewModels/          # Data Transfer Objects
├── DAL/                     # Data Access Layer
│   ├── Context/             # EF Core DbContext [cite: 296]
│   ├── Migrations/          # Database Schema History [cite: 260]
│   ├── Models/              # Database Entities [cite: 662]
│   └── Repositories/        # Unit of Work & Repos
├── GymManagementPL/         # Presentation Layer (MVC) [cite: 486]
│   ├── Controllers/         # MVC Controllers
│   ├── Views/               # Razor Templates [cite: 540]
│   └── wwwroot/             # Static Assets (CSS, JS, Images)
└── GymManagementSolution.sln
```

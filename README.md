# Product Management System

Manage products efficiently with the Product Management System. Admins can perform CRUD operations on products, while regular users can only view them.

## Overview

The Product Management System is an ASP.NET MVC web application designed to streamline the management of products. It incorporates user roles to differentiate between administrators and regular users.

## Prerequisites

Ensure you have the following installed before running the application:

- [.NET Core SDK](https://dotnet.microsoft.com/download)

## Getting Started

Follow these steps to set up and run the Product Management System on your local machine.

### 1. Clone the Repository

```bash
git clone https://github.com/your-username/product-management-system.git
cd product-management-system
```

### 2. Configure the Database

```bash
dotnet ef database update
```

### 3. Install Dependencies

```bash
dotnet restore
```

### 4. Run the Application

```bash
dotnet run
```

Visit http://localhost:1000 in your browser.

## User Roles

Admins:
Can log in to the admin panel.
Perform CRUD operations on products.

Users:
Can log in.
View the list of available products.

## Usage

Log in using your credentials.
Admins can access the admin panel to manage products.
Users can view the list of available products.

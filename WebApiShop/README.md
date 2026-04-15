# WebApiShop

## 📋 Overview
Project is a Web API project written with .NET 9 in C#.
The project implements the principles of REST.

## 🏗️ Architecture
The project is built from 3 layers:
1. **APPLICATION** layer 🖥️
2. **SERVICES** layer ⚙️
3. **REPOSITORY** layer 🗄️

The layers communicate with each other using Dependency Injection to create Decoupling.

## 🛢️ Database
The connection to the database is done using Entity Framework ORM with SQL Server. The work was done using a database-first approach.
Access to the database was done asynchronously in order to free up threads and improve scalability ⚡.

## 📦 DTOs
There is a DTO layer to remove circular dependencies and to disconnect the DATA layer from the other layers.
We used Records in the DTO layer since it is more suitable for the data transfer layer, while the conversion between Entities and DTOs was done using AutoMapper 🔄.

## ⚙️ Configuration
The configurations are saved separately from the code in the appsettings files.

## 📝 Logging & Error Handling
The project makes extensive use of the logger in the NLog library, with errors being handled by error handling middleware.
NLog is configured to write structured JSON logs to file and send email notifications 📧 via SMTP for errors.
For tracking purposes, all incoming HTTP requests are logged in the Rating table.

## 🚀 API Features
- 📖 **Swagger / OpenAPI** documentation is available in development mode.
- 📄 **Pagination, Filtering & Sorting** on the Products endpoint (by name, description, category, price range, with sorting options).
- 🔐 **Password Strength Evaluation** using the Zxcvbn library — weak passwords (score ≤ 2) are rejected.
- 🔑 **User Login** endpoint with email and password validation.
- 🌐 **Static Files** served from the wwwroot folder for front-end pages.
- 🔒 **HTTPS Redirection** is enforced.

## ✅ Testing
We ensured test coverage with unit tests and integration tests using the xUnit library.
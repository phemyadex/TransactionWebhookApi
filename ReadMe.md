# Transaction Webhook API

## Overview

This project implements a webhook endpoint that receives transaction notifications from external providers, validates incoming requests, prevents duplicate transactions, stores transaction data in PostgreSQL.

The solution follows Clean Architecture principles with separate Domain, Application, Infrastructure, and API layers.

---

## Features

* Receive transaction webhooks
* Persist transactions to PostgreSQL
* Duplicate transaction prevention
* Input validation
* Entity Framework Core migrations
* Swagger documentation
* Repository Pattern
* Dependency Injection
* Clean Architecture

---

## Technology Stack

* .NET 10
* ASP.NET Core Web API
* Entity Framework Core
* PostgreSQL
* xUnit
* Moq
* Swagger

---

## Project Structure

src/

* TransactionWebhook.Api
* TransactionWebhook.Application
* TransactionWebhook.Domain
* TransactionWebhook.Infrastructure

tests/

* TransactionWebhook.Tests

---

## Setup

### 1. Clone Repository

```bash
git clone <repository-url>
cd TransactionWebhookApi
```

### 2. Configure PostgreSQL

Create a database:

```sql
CREATE DATABASE transactionwebhookdb;
```

Update appsettings.json:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=transactionwebhookdb;Username=postgres;Password=********"
  }
}
```

### 3. Apply Migrations

```powershell
Add-Migration InitialCreate
Update-Database 
```

### 4. Run Application

```bash
dotnet build
dotnet run --project src/TransactionWebhook.Api
```

### 5. Open Swagger

```
https://localhost:7060/swagger
```

---

## API Endpoints

### Create Transaction

POST /api/transactions

Request:

```json
{
  "transactionId": "TXN001",
  "amount": 500.00,
  "currency": "USD"
}
```

Response:

```json
{
  "success": true,
  "message": "Transaction processed successfully"
}
```

---


## Running Tests

```bash
dotnet test
```

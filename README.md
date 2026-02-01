# MadameCoco Backend Challenge

## Introduction
I designed and implemented this Microservices Backend Solution for the MadameCoco challenge. My primary goal was to create a scalable, maintainable system using strict clean architecture principles. I structured the solution to ensure loosely coupled services that can evolve independently, utilizing industry-standard patterns like CQRS and Event-Driven Architecture.

## Architecture and Design Decisions

### Command Query Responsibility Segregation (CQRS)
I implemented the CQRS pattern in the Order Service to distinctly separate write and read operations, optimizing each for its specific workload.
*   **Write Side (Commands):** I used **Entity Framework Core** to handle state changes (e.g., `CreateOrderCommand`), leveraging its robust change tracking and domain modeling capabilities.
*   **Read Side (Queries):** I utilized **Dapper** for the read side (e.g., `GetOrderByIdQueryHandler`). This was a deliberate choice to ensure high-performance data retrieval by executing raw, optimized SQL queries without the overhead of full entity tracking.

### Data Integrity
I integrated **FluentValidation** throughout the application to enforce strict data integrity rules. For example, the `CustomerValidator` ensures that all customer data meets domain requirements before it reaches the core logic.

### API Gateway
I deployed an **Ocelot API Gateway** to function as the single entry point for all client requests, abstracting the underlying microservices infrastructure and simplifing routing configuration.

### Event-Driven Communication
To maintain decoupling between services, I implemented an Event-Driven Architecture using **RabbitMQ** (via MassTransit). When an order is placed, an event is published asynchronously. This design ensures that the Order Service remains highly responsive and is not blocked by downstream processes like auditing.

### Order Snapshot Strategy
When an order is created, I capture a snapshot of the product details (Name, Price, ImageUrl) at that specific moment. This ensures that historical order data remains accurate even if the product catalog changes in the future.

### Audit Service
I built the Audit Service as a background worker that consumes integration events and persists audit logs to **MongoDB**. I selected MongoDB for its flexibility in handling unstructured data, which is ideal for evolving log schemas.

## Technologies Used
*   **Framework:** .NET 8.0
*   **Message Broker:** RabbitMQ (MassTransit)
*   **Databases:** PostgreSQL (Relational), MongoDB (NoSQL)
*   **API Gateway:** Ocelot
*   **ORM:** Entity Framework Core (Write), Dapper (Read)
*   **Validation:** FluentValidation
*   **Containerization:** Docker & Docker Compose

## Prerequisites
*   **Docker Desktop**: Required to run the containerized environment.

## Execution Instructions

### Running the Environment
I have automated the entire setup using Docker Compose. Run the following command to verify the environment, initializing all databases and services:

```bash
docker-compose down -v && docker-compose up --build
```

**Note:** This command automatically handles the initialization of the PostgreSQL databases (`MadameCocoCustomerDb`, `MadameCocoOrderDb`) and applies all necessary logical migrations on startup.

### Verification (Unit Tests)
To verify the core business logic of the Order Service, run the unit test suite:

```bash
dotnet test
```

## API Usage

The following CURL commands can be used to test the endpoints via the API Gateway (Port 5050).

### 1. Create a Customer
**Endpoint:** `POST /api/customers`

```bash
curl -X POST http://localhost:5050/api/customers \
   -H "Content-Type: application/json" \
   -d '{
         "name": "John Doe",
         "email": "john.doe@example.com",
         "address": {
           "addressLine": "123 Main St",
           "city": "Istanbul",
           "country": "Turkey",
           "cityCode": 34
         }
       }'
```

### 2. Create an Order
**Endpoint:** `POST /api/orders`

```bash
curl -X POST http://localhost:5050/api/orders \
   -H "Content-Type: application/json" \
   -d '{
         "customerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
         "address": {
           "addressLine": "123 Main St",
           "city": "Istanbul",
           "country": "Turkey",
           "cityCode": 34
         },
         "items": [
           {
             "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
             "productName": "Coffee Cup",
             "imageUrl": "http://example.com/coffee.jpg",
             "status": "Active",
             "quantity": 1,
             "unitPrice": 150.00
           }
         ]
       }'
```

### 3. Change Order Status
**Endpoint:** `PATCH /api/orders/{id}/status`

```bash
curl -X PATCH http://localhost:5050/api/orders/3fa85f64-5717-4562-b3fc-2c963f66afa6/status \
   -H "Content-Type: application/json" \
   -d '"Shipped"'
```
Brief Explanation 

The Transaction Webhook API receives transaction notifications from external systems and stores them in PostgreSQL. The application follows Clean Architecture to separate business rules from infrastructure concerns.

Incoming requests are validated to ensure that transaction identifiers, amounts, and currencies are provided. Before persisting a transaction, the system checks whether the supplied external transaction identifier already exists. If a duplicate is detected, the request is rejected with a conflict response.

Entity Framework Core is used for persistence, while PostgreSQL serves as the relational database. The repository pattern abstracts data access, making business logic independent of storage implementation and easier to test.

The API exposes endpoints for transaction creation. Swagger is enabled for API documentation and testing. Automated unit tests verify duplicate detection, validations and successful transaction processing.

This design promotes maintainability, testability, and scalability while keeping the implementation simple and aligned with webhook processing requirements.

Assumptions:
i. External transaction IDs are globally unique.
ii. Currency values are supplied using ISO currency codes (e.g., USD, EUR, NGN).
iii. Transactions are processed synchronously upon webhook receipt.

Decision Justification:
1. Clean Architecture - Chosen to separate business logic from infrastructure and API concerns, improving maintainability and testability.

2. PostgreSQL - Selected because it is open-source, reliable, ACID-compliant, and well-supported by Entity Framework Core.

Rejected Alternative:
In-Memory Storage - Rejected because transaction data must persist across application restarts and support reliable duplicate detection.

Failure Scenario:
Duplicate Webhook Delivery - External providers may resend the same webhook multiple times. The application mitigates this by enforcing a unique constraint on external_transaction_id and validating duplicates before insertion. This prevents duplicate transaction records from being created.
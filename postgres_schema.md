CREATE TABLE transactions
(
    id UUID PRIMARY KEY,
    external_transaction_id VARCHAR(100) UNIQUE NOT NULL,
    amount NUMERIC(18,2) NOT NULL,
    currency VARCHAR(10) NOT NULL,
    created_at TIMESTAMP NOT NULL
);

CREATE TABLE derived_records
(
    id UUID PRIMARY KEY,
    transaction_id UUID NOT NULL,
    fee NUMERIC(18,2) NOT NULL,
    net_amount NUMERIC(18,2) NOT NULL,

    CONSTRAINT fk_transaction
        FOREIGN KEY(transaction_id)
        REFERENCES transactions(id)
);
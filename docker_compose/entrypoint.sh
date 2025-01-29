#!/bin/bash
# Start SQL Server in the background
/opt/mssql/bin/sqlservr &

# Wait for SQL Server to start
echo "Waiting for SQL Server to start..."
sleep 15

# Run SQL commands to create a database
echo "Creating database..."
/opt/mssql-tools/bin/sqlcmd -S localhost,1433 -U sa -P "$SA_PASSWORD" -Q "CREATE DATABASE FinancialTransactionsProcessing"

# Bring SQL Server to the foreground
wait
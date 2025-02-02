# FinancialTransactionsProcessing
Evaluator that consumes CSV files to produce reports of customer expenses and carry out products popularity estimation

# Run project (Windows)

`docker build . -f  docker_compose/Dockerfile.SQLDatabase -t sql` - build an image of a database

`docker run --rm -e "SA_PASSWORD=3nbr3u2@!fwo" -e "MSSQL_TCP_PORT=1433" -e "ACCEPT_EULA=Y" -p 1433:1433 --name sql_server_container --env-file docker_compose/.env sql` - run the container for mssql

`dotnet run --project ./FinancialTransactionsProcessing/FinancialTransactionsProcessing/FinancialTransactionsProcessing.csproj` - Run Console project by executing from the repo root directory

# Mac OS

`docker build . -f  docker_compose/Dockerfile.SQLDatabase -t macsql`

`docker run --rm -e "SA_PASSWORD=3nbr3u2@!fwo" -e "MSSQL_TCP_PORT=1433" -e "ACCEPT_EULA=Y" -p 1433:1433 --name sql_server_container --env-file docker_compose/.env macsql` 

and run project locally `dotnet run --project ./FinancialTransactionsProcessing/FinancialTransactionsProcessing/FinancialTransactionsProcessing.csproj`

OR

`docker-compose up -d` - this will roll out two containers for database and web application

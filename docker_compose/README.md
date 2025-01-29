# Docker

Enter a root level of the project (project, docker_compose etc.)

To run containers altogether perform `docker-compose up --build` or `docker-compose up -d` command at docker_compose folder 

## SQL

### Create a private network for database

`docker network create db_network`

### Build docker file (should be executed at a root level of the project)

`docker build . -f  docker_compose/Dockerfile.SQLDatabase -t sql`

### Run docker image

`docker run --rm -e "SA_PASSWORD=3nbr3u2@!fwo" -e "MSSQL_TCP_PORT=1433" -e "ACCEPT_EULA=Y" -p 1433:1433 --name sql_server_container --network db_network --env-file docker_compose/.env sql` 

## .NET 8 (avoid this step if running locally to debug)

### Build docker file (should be executed at a root level of the project)
 
`docker build -f  docker_compose/Dockerfile.WebApp -t webapp .` 

### Run docker image

`docker run -d -p 5000:5000 --name web_app_container --network db_network webapp`
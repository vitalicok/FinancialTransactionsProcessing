#!/bin/sh
rm -rf ../docker_compose/result && mkdir ../docker_compose/result
docker-compose -f ../docker_compose/docker-compose.mac.yml up --build

until docker exec app test -f /app/result/report.json; do
  echo "Waiting..."
  sleep 1
done

docker cp app:/app/result/report.json ./result/report.json
exit
docker-compose down
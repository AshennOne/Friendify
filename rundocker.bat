@echo off
echo Pulling Docker images...
docker-compose pull
echo Starting Docker containers...
docker-compose up
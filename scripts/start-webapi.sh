#!/bin/bash
./scripts/compose-database:up.sh -d 
ASPNETCORE_ENVIRONMENT=local-local-db 
dotnet run --project ./source/Espresso.WebApi/Espresso.WebApi.csproj --urls "http://localhost:8000" 
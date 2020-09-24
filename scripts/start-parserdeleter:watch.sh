#!/bin/bash
./scripts/compose-database:up.sh -d 
ASPNETCORE_ENVIRONMENT=local-local-db 
dotnet watch --project ./source/Espresso.ParserDeleter/Espresso.ParserDeleter.csproj run --urls "http://localhost:8000" 
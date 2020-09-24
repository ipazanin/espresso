#!/bin/bash
./scripts/compose-database:up.sh -d 
ASPNETCORE_ENVIRONMENT=local-local-db 
dotnet run --project ./source/Espresso.ParserDeleter/Espresso.ParserDeleter.csproj 
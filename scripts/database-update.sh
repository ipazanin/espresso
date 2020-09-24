#!/bin/bash
ASPNETCORE_ENVIRONMENT=local-production-db dotnet ef database update -p ./source/Espresso.Persistence/Espresso.Persistence.csproj -v
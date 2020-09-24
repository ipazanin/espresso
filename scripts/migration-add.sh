#!/bin/bash
ASPNETCORE_ENVIRONMENT=local-local-db dotnet ef migrations add -p ./source/Espresso.Persistence/Espresso.Persistence.csproj -v $1
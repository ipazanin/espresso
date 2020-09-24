#!/bin/bash
ASPNETCORE_ENVIRONMENT=local-local-db dotnet ef migrations remove -p ./source/Espresso.Persistence/Espresso.Persistence.csproj -v
#!/bin/bash
./scripts/compose-database:up.sh -d 
ASPNETCORE_ENVIRONMENT=local-local-db 
#!/bin/bash
ASPNETCORE_ENVIRONMENT=local-local-db 
if [ -z $1 ]
then
    dotnet run --project ./source/Espresso.WebApi/Espresso.WebApi.csproj --urls "http://localhost:8000" 
elif [ $1 == "watch" ]
then
    dotnet watch --project ./source/Espresso.WebApi/Espresso.WebApi.csproj run --urls "http://localhost:8000" 
else
    echo "Invalid Argument. Accepted arguments: {empty}, watch}"
fi
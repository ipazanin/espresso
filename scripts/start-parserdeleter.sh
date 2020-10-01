#!/bin/bash
ASPNETCORE_ENVIRONMENT=local-local-db 
if [ -z $1 ]
then
    dotnet run --project ./source/Espresso.ParserDeleter/Espresso.ParserDeleter.csproj --urls "http://localhost:9000"
elif [ $1 == "watch" ]
then
    dotnet watch --project ./source/Espresso.ParserDeleter/Espresso.ParserDeleter.csproj run --urls "http://localhost:9000" 
else
    echo "Invalid Argument. Accepted arguments: {empty}, watch}"
fi
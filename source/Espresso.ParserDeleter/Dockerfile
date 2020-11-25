FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS dotnetruntime

# .NET App
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS dotnetbuilder
WORKDIR /source

# Restore .NET App
COPY ["Espresso.Application/Espresso.Application.csproj", "Espresso.Application/"]
COPY ["Espresso.Common/Espresso.Common.csproj", "Espresso.Common/"]
COPY ["Espresso.Domain/Espresso.Domain.csproj", "Espresso.Domain/"]
COPY ["Espresso.ParserDeleter.Application/Espresso.ParserDeleter.Application.csproj", "Espresso.ParserDeleter.Application/"]
COPY ["Espresso.ParserDeleter/Espresso.ParserDeleter.csproj", "Espresso.ParserDeleter/"]
COPY ["Espresso.Persistence/Espresso.Persistence.csproj", "Espresso.Persistence/"]
RUN dotnet restore "Espresso.ParserDeleter/Espresso.ParserDeleter.csproj"

# Build .NET App
COPY . .
WORKDIR "/source/Espresso.ParserDeleter"
RUN dotnet build "Espresso.ParserDeleter.csproj" -c Release -o /app/build

# Publish .NET App
FROM dotnetbuilder AS dotnetpublisher
RUN dotnet publish "Espresso.ParserDeleter.csproj" -c Release -o /app/publish

FROM dotnetruntime AS final
WORKDIR /app

# Copy .NET Application From clientappbuilder Image Into final Image
COPY --from=dotnetpublisher /app/publish .
ENTRYPOINT ["dotnet", "Espresso.ParserDeleter.dll"]

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS dotnet-runtime

# .NET App
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS dotnet-builder
WORKDIR /server/Espresso

# Restore .NET App
COPY ["server/Espresso/Espresso.Application/Espresso.Application.csproj", "Espresso.Application/"]
COPY ["server/Espresso/Espresso.Common/Espresso.Common.csproj", "Espresso.Common/"]
COPY ["server/Espresso/Espresso.Domain/Espresso.Domain.csproj", "Espresso.Domain/"]
COPY ["server/Espresso/Espresso.ParserDeleter.Application/Espresso.ParserDeleter.Application.csproj", "Espresso.ParserDeleter.Application/"]
COPY ["server/Espresso/Espresso.ParserDeleter/Espresso.ParserDeleter.csproj", "Espresso.ParserDeleter/"]
COPY ["server/Espresso/Espresso.Persistence/Espresso.Persistence.csproj", "Espresso.Persistence/"]
RUN dotnet restore "Espresso.ParserDeleter/Espresso.ParserDeleter.csproj"

# Build .NET App
COPY server/Espresso .
WORKDIR /server/Espresso/Espresso.ParserDeleter
RUN dotnet build "Espresso.ParserDeleter.csproj" -c Release -o /app/server/build

# Publish .NET App
FROM dotnet-builder AS dotnet-publisher
RUN dotnet publish "Espresso.ParserDeleter.csproj" -c Release -o /app/server/publish

FROM dotnet-runtime AS final

# Sets working directory for dotnet entry point command
WORKDIR /app/server

# Copy .NET Application From dotnet-publisher Image Into final Image
COPY --from=dotnet-publisher /app/server/publish /app/server
ENTRYPOINT ["dotnet", "Espresso.ParserDeleter.dll"]

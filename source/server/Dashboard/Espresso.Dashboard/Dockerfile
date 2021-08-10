FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS dotnet-runtime

# .NET App
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS dotnet-builder
WORKDIR /server/Espresso

# Restore .NET App
COPY ["server/Shared/Espresso.Application/Espresso.Application.csproj", "Shared/Espresso.Application/"]
COPY ["server/Shared/Espresso.Common/Espresso.Common.csproj", "Shared/Espresso.Common/"]
COPY ["server/Shared/Espresso.Domain/Espresso.Domain.csproj", "Shared/Espresso.Domain/"]
COPY ["server/Shared/Espresso.Persistence/Espresso.Persistence.csproj", "Shared/Espresso.Persistence/"]
COPY ["server/Dashboard/Espresso.Dashboard.Application/Espresso.Dashboard.Application.csproj", "Dashboard/Espresso.Dashboard.Application/"]
COPY ["server/Dashboard/Espresso.Dashboard/Espresso.Dashboard.csproj", "Dashboard/Espresso.Dashboard/"]
RUN dotnet restore "Dashboard/Espresso.Dashboard/Espresso.Dashboard.csproj"

# Build .NET App
COPY server/ .
RUN dotnet build "Dashboard/Espresso.Dashboard/Espresso.Dashboard.csproj" -c Release -o /app/server/build

# Publish .NET App
FROM dotnet-builder AS dotnet-publisher
RUN dotnet publish "Dashboard/Espresso.Dashboard/Espresso.Dashboard.csproj" -c Release -o /app/server/publish

FROM dotnet-runtime AS final

# Sets working directory for dotnet entry point command
WORKDIR /app/server

# Copy .NET Application From dotnet-publisher Image Into final Image
COPY --from=dotnet-publisher /app/server/publish /app/server
ENTRYPOINT ["dotnet", "Espresso.Dashboard.dll"]

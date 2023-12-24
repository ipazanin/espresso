FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS dotnet-runtime

# .NET App
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dotnet-builder
WORKDIR /source/server/Espresso

# Restore .NET App
COPY ["source/server/Shared/Espresso.Application/Espresso.Application.csproj", "Shared/Espresso.Application/"]
COPY ["source/server/Shared/Espresso.Common/Espresso.Common.csproj", "Shared/Espresso.Common/"]
COPY ["source/server/Shared/Espresso.Domain/Espresso.Domain.csproj", "Shared/Espresso.Domain/"]
COPY ["source/server/Shared/Espresso.Persistence/Espresso.Persistence.csproj", "Shared/Espresso.Persistence/"]
COPY ["source/server/Dashboard/Espresso.Dashboard.Application/Espresso.Dashboard.Application.csproj", "Dashboard/Espresso.Dashboard.Application/"]
COPY ["source/server/Dashboard/Espresso.Dashboard/Espresso.Dashboard.csproj", "Dashboard/Espresso.Dashboard/"]
RUN dotnet restore "Dashboard/Espresso.Dashboard/Espresso.Dashboard.csproj"

# Build .NET App
COPY /source/server/ .
COPY /.editorconfig .
RUN dotnet build "Dashboard/Espresso.Dashboard/Espresso.Dashboard.csproj" -c Release -o /app/server/build --no-restore

# Publish .NET App
FROM dotnet-builder AS dotnet-publisher
RUN dotnet publish "Dashboard/Espresso.Dashboard/Espresso.Dashboard.csproj" -c Release -o /app/server/publish

FROM dotnet-runtime AS final

# Sets working directory for dotnet entry point command
WORKDIR /app/server

# Copy .NET Application From dotnet-publisher Image Into final Image
COPY --from=dotnet-publisher /app/server/publish /app/server
ENTRYPOINT ["dotnet", "Espresso.Dashboard.dll"]

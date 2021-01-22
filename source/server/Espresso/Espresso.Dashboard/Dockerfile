FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS dotnet-runtime

# .NET App
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS dotnet-builder
WORKDIR /server/Espresso

# Restore .NET App
COPY ["server/Espresso/Espresso.Application/Espresso.Application.csproj", "Espresso.Application/"]
COPY ["server/Espresso/Espresso.Common/Espresso.Common.csproj", "Espresso.Common/"]
COPY ["server/Espresso/Espresso.Domain/Espresso.Domain.csproj", "Espresso.Domain/"]
COPY ["server/Espresso/Espresso.Dashboard.Application/Espresso.Dashboard.Application.csproj", "Espresso.Dashboard.Application/"]
COPY ["server/Espresso/Espresso.Dashboard/Espresso.Dashboard.csproj", "Espresso.Dashboard/"]
COPY ["server/Espresso/Espresso.Persistence/Espresso.Persistence.csproj", "Espresso.Persistence/"]
RUN dotnet restore "Espresso.Dashboard/Espresso.Dashboard.csproj"

# Build .NET App
COPY server/Espresso .
WORKDIR /server/Espresso/Espresso.Dashboard
RUN dotnet build "Espresso.Dashboard.csproj" -c Release -o /app/server/build

# Publish .NET App
FROM dotnet-builder AS dotnet-publisher
RUN dotnet publish "Espresso.Dashboard.csproj" -c Release -o /app/server/publish

FROM dotnet-runtime AS final

# Sets working directory for dotnet entry point command
WORKDIR /app/server

# Copy .NET Application From dotnet-publisher Image Into final Image
COPY --from=dotnet-publisher /app/server/publish /app/server
ENTRYPOINT ["dotnet", "Espresso.Dashboard.dll"]

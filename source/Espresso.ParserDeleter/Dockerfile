FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /source
COPY ["Espresso.ParserDeleter/Espresso.ParserDeleter.csproj", "Espresso.ParserDeleter/"]
COPY ["Espresso.Common/Espresso.Common.csproj", "Espresso.Common/"]
COPY ["Espresso.ParserDeleter.Application/Espresso.ParserDeleter.Application.csproj", "Espresso.ParserDeleter.Application/"]
COPY ["Espresso.Domain/Espresso.Domain.csproj", "Espresso.Domain/"]
COPY ["Espresso.Persistence/Espresso.Persistence.csproj", "Espresso.Persistence/"]
RUN dotnet restore "Espresso.ParserDeleter/Espresso.ParserDeleter.csproj"
COPY . .
WORKDIR "/source/Espresso.ParserDeleter"
RUN dotnet build "Espresso.ParserDeleter.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Espresso.ParserDeleter.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Espresso.ParserDeleter.dll"]

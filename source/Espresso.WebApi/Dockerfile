FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Espresso.WebApi/Espresso.WebApi.csproj", "Espresso.WebApi/"]
COPY ["Espresso.DataAccessLayer/Espresso.DataAccessLayer.csproj", "Espresso.DataAccessLayer/"]
COPY ["Espresso.Domain/Espresso.Domain.csproj", "Espresso.Domain/"]
COPY ["Espresso.Common/Espresso.Common.csproj", "Espresso.Common/"]
COPY ["Espresso.Persistence/Espresso.Persistence.csproj", "Espresso.Persistence/"]
COPY ["Espresso.Application/Espresso.Application.csproj", "Espresso.Application/"]
RUN dotnet restore "Espresso.WebApi/Espresso.WebApi.csproj"
COPY . .
WORKDIR "/src/Espresso.WebApi"
RUN dotnet build "Espresso.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Espresso.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Espresso.WebApi.dll"]
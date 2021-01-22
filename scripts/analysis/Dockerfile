FROM mcr.microsoft.com/infersharp:latest AS infer-sharp

RUN ls

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS dotnet-builder
WORKDIR /server/Espresso

# Restore .NET App
COPY nuget.config nuget.config
COPY ["server/Espresso/Espresso.Application/Espresso.Application.csproj", "Espresso.Application/"]
COPY ["server/Espresso/Espresso.Common/Espresso.Common.csproj", "Espresso.Common/"]
COPY ["server/Espresso/Espresso.Domain/Espresso.Domain.csproj", "Espresso.Domain/"]
COPY ["server/Espresso/Espresso.Dashboard.Application/Espresso.Dashboard.Application.csproj", "Espresso.Dashboard.Application/"]
COPY ["server/Espresso/Espresso.Dashboard/Espresso.Dashboard.csproj", "Espresso.Dashboard/"]
COPY ["server/Espresso/Espresso.Persistence/Espresso.Persistence.csproj", "Espresso.Persistence/"]
RUN dotnet restore "Espresso.Dashboard/Espresso.Dashboard.csproj"

COPY server/Espresso .

WORKDIR /server/Espresso/Espresso.Dashboard
RUN dotnet build "Espresso.Dashboard.csproj" -c Release -o /app/server/build

FROM dotnet-builder AS dotnet-publisher
RUN dotnet publish "Espresso.Dashboard.csproj" -c Release -o /app/server/publish

FROM infer-sharp

RUN mkdir Espresso

WORKDIR /Espresso

COPY --from=dotnet-builder /app/server/build .
COPY --from=dotnet-publisher /app/server/publish .
RUN rm -rf AppSettings

FROM infer-sharp

RUN ls Examples

ENTRYPOINT ["sh", "run_infersharp.sh", "Espresso", "output"]

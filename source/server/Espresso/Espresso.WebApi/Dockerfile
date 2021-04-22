FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS dotnet-runtime

# .NET App
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS dotnet-builder
WORKDIR /server/Espresso

# Restore .NET App
COPY ["server/Espresso/Espresso.Application/Espresso.Application.csproj", "Espresso.Application/"]
COPY ["server/Espresso/Espresso.Common/Espresso.Common.csproj", "Espresso.Common/"]
COPY ["server/Espresso/Espresso.Domain/Espresso.Domain.csproj", "Espresso.Domain/"]
COPY ["server/Espresso/Espresso.Persistence/Espresso.Persistence.csproj", "Espresso.Persistence/"]
COPY ["server/Espresso/Espresso.WebApi.Application/Espresso.WebApi.Application.csproj", "Espresso.WebApi.Application/"]
COPY ["server/Espresso/Espresso.WebApi/Espresso.WebApi.csproj", "Espresso.WebApi/"]
RUN dotnet restore "Espresso.WebApi/Espresso.WebApi.csproj"

# Build .NET App
COPY server/Espresso .
WORKDIR /server/Espresso/Espresso.WebApi
RUN dotnet build "Espresso.WebApi.csproj" -c Release -o /app/server/build

# Publish .NET App
FROM dotnet-builder AS dotnet-publisher
RUN dotnet publish "Espresso.WebApi.csproj" -c Release -o /app/server/publish

# Client App
FROM node AS client-builder
ARG REACT_APP_ENVIRONMENT

# set working directory
RUN mkdir /usr/src/app
WORKDIR /usr/src/app

# add `/usr/src/app/node_modules/.bin` to $PATH
ENV PATH /usr/src/app/node_modules/.bin:$PATH

# install and cache app dependencies
COPY client/package.json /usr/src/app/package.json
RUN npm install --force

# Copy rest of Client Application files
COPY client/. /usr/src/app

# Build Client Application
ENV REACT_APP_ENVIRONMENT=$REACT_APP_ENVIRONMENT
RUN npm run build-frontend

# Copy .NET Application From client-builder Image Into final Image
FROM dotnet-runtime AS final
WORKDIR /app/server/espresso/webapi
COPY --from=dotnet-publisher /app/server/publish /app/server/espresso/webapi

# Copy Client Application From client-builder Image Into final Image
RUN mkdir -p /app/client/build
COPY --from=client-builder /usr/src/app/build/. /app/client/build

ENTRYPOINT ["dotnet", "Espresso.WebApi.dll"]

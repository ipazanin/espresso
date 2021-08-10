FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS dotnet-runtime

# .NET App
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS dotnet-builder
WORKDIR /server/Espresso

# Restore .NET App
COPY ["server/Shared/Espresso.Application/Espresso.Application.csproj", "Shared/Espresso.Application/"]
COPY ["server/Shared/Espresso.Common/Espresso.Common.csproj", "Shared/Espresso.Common/"]
COPY ["server/Shared/Espresso.Domain/Espresso.Domain.csproj", "Shared/Espresso.Domain/"]
COPY ["server/Shared/Espresso.Persistence/Espresso.Persistence.csproj", "Shared/Espresso.Persistence/"]
COPY ["server/WebApi/Espresso.WebApi.Application/Espresso.WebApi.Application.csproj", "WebApi/Espresso.WebApi.Application/"]
COPY ["server/WebApi/Espresso.WebApi/Espresso.WebApi.csproj", "WebApi/Espresso.WebApi/"]
RUN dotnet restore "WebApi/Espresso.WebApi/Espresso.WebApi.csproj"

# Build .NET App
COPY server/ .
RUN dotnet build "WebApi/Espresso.WebApi/Espresso.WebApi.csproj" -c Release -o /app/server/build

# Publish .NET App
FROM dotnet-builder AS dotnet-publisher
RUN dotnet publish "WebApi/Espresso.WebApi/Espresso.WebApi.csproj" -c Release -o /app/server/publish

# Client App
FROM node:14 AS client-builder
ARG REACT_APP_ENVIRONMENT

# set working directory
RUN mkdir /usr/src/app
WORKDIR /usr/src/app

# add `/usr/src/app/node_modules/.bin` to $PATH
ENV PATH /usr/src/app/node_modules/.bin:$PATH

# install and cache app dependencies
COPY client/package.json /usr/src/app/package.json
RUN npm install

# Copy rest of Client Application files
COPY client/. /usr/src/app

# Build Client Application
ENV REACT_APP_ENVIRONMENT=$REACT_APP_ENVIRONMENT
RUN npm run build-frontend

# Copy .NET Application From dotnet-publisher Image Into final Image
FROM dotnet-runtime AS final
WORKDIR /app/server/espresso/webapi
COPY --from=dotnet-publisher /app/server/publish /app/server/espresso/webapi

# Copy Client Application From client-builder Image Into final Image
RUN mkdir -p /app/client/build
COPY --from=client-builder /usr/src/app/build/. /app/client/build

ENTRYPOINT ["dotnet", "Espresso.WebApi.dll"]

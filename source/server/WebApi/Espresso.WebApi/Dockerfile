FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS dotnet-runtime

# .NET App
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS dotnet-builder

# Restore .NET App
COPY ["source/server/Shared/Espresso.Application/Espresso.Application.csproj", "Shared/Espresso.Application/"]
COPY ["source/server/Shared/Espresso.Common/Espresso.Common.csproj", "Shared/Espresso.Common/"]
COPY ["source/server/Shared/Espresso.Domain/Espresso.Domain.csproj", "Shared/Espresso.Domain/"]
COPY ["source/server/Shared/Espresso.Persistence/Espresso.Persistence.csproj", "Shared/Espresso.Persistence/"]
COPY ["source/server/WebApi/Espresso.WebApi.Application/Espresso.WebApi.Application.csproj", "WebApi/Espresso.WebApi.Application/"]
COPY ["source/server/WebApi/Espresso.WebApi/Espresso.WebApi.csproj", "WebApi/Espresso.WebApi/"]
RUN dotnet restore "WebApi/Espresso.WebApi/Espresso.WebApi.csproj"

# Build .NET App
COPY /source/server/ .
COPY /.editorconfig .
RUN dotnet build "WebApi/Espresso.WebApi/Espresso.WebApi.csproj" -c Release -o /app/server/build --no-restore

# Publish .NET App
FROM dotnet-builder AS dotnet-publisher
RUN dotnet publish "WebApi/Espresso.WebApi/Espresso.WebApi.csproj" -c Release -o /app/server/publish

# Client App
FROM node:18 AS client-builder
ARG REACT_APP_ENVIRONMENT

# set working directory
RUN mkdir /usr/src/app
WORKDIR /usr/src/app

# add `/usr/src/app/node_modules/.bin` to $PATH
ENV PATH /usr/src/app/node_modules/.bin:$PATH

# install and cache app dependencies
COPY /source/client/package.json /usr/src/app/package.json
RUN npm install

# Copy rest of Client Application files
COPY /source/client/. /usr/src/app

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

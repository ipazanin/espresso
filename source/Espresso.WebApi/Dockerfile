FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS dotnetruntime

# .NET App
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS dotnetbuilder
WORKDIR /source

# Restore .NET App
COPY ["Espresso.Application/Espresso.Application.csproj", "Espresso.Application/"]
COPY ["Espresso.Common/Espresso.Common.csproj", "Espresso.Common/"]
COPY ["Espresso.Domain/Espresso.Domain.csproj", "Espresso.Domain/"]
COPY ["Espresso.Persistence/Espresso.Persistence.csproj", "Espresso.Persistence/"]
COPY ["Espresso.WebApi.Application/Espresso.WebApi.Application.csproj", "Espresso.WebApi.Application/"]
COPY ["Espresso.WebApi/Espresso.WebApi.csproj", "Espresso.WebApi/"]
RUN dotnet restore "Espresso.WebApi/Espresso.WebApi.csproj"

# Build .NET App
COPY . .
WORKDIR "/source/Espresso.WebApi"
RUN dotnet build "Espresso.WebApi.csproj" -c Release -o /app/build

# Publish .NET App
FROM dotnetbuilder AS dotnetpublisher
RUN dotnet publish "Espresso.WebApi.csproj" -c Release -o /app/publish

# Client App
FROM node AS clientappbuilder
ARG REACT_APP_ENVIRONMENT

# set working directory
RUN mkdir /usr/src/app
WORKDIR /usr/src/app

# add `/usr/src/app/node_modules/.bin` to $PATH
ENV PATH /usr/src/app/node_modules/.bin:$PATH

# install and cache app dependencies
COPY Espresso.ClientApp/package.json /usr/src/app/package.json
RUN npm install

# Copy rest of Client Application files
COPY Espresso.ClientApp/. /usr/src/app

# Build Client Application
ENV REACT_APP_ENVIRONMENT=$REACT_APP_ENVIRONMENT
RUN npm run build-frontend

# Copy .NET Application From clientappbuilder Image Into final Image
FROM dotnetruntime AS final
WORKDIR /app/dotnet
COPY --from=dotnetpublisher /app/publish .

# Copy Client Application From clientappbuilder Image Into final Image
RUN mkdir -p /app/Espresso.ClientApp/build
COPY --from=clientappbuilder /usr/src/app/build/. /app/Espresso.ClientApp/build

ENTRYPOINT ["dotnet", "Espresso.WebApi.dll"]

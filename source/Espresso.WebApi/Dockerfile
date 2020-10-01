FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source
COPY ["Espresso.Application/Espresso.Application.csproj", "Espresso.Application/"]
COPY ["Espresso.Common/Espresso.Common.csproj", "Espresso.Common/"]
COPY ["Espresso.Domain/Espresso.Domain.csproj", "Espresso.Domain/"]
COPY ["Espresso.Persistence/Espresso.Persistence.csproj", "Espresso.Persistence/"]
COPY ["Espresso.WebApi.Application/Espresso.WebApi.Application.csproj", "Espresso.WebApi.Application/"]
COPY ["Espresso.WebApi/Espresso.WebApi.csproj", "Espresso.WebApi/"]
RUN dotnet restore "Espresso.WebApi/Espresso.WebApi.csproj"
COPY . .
WORKDIR "/source/Espresso.WebApi"
RUN dotnet build "Espresso.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Espresso.WebApi.csproj" -c Release -o /app/publish

#React build
FROM node AS nodebuilder
ARG REACT_APP_ENVIRONMENT

# set working directory
RUN mkdir /usr/src/app
WORKDIR /usr/src/app

# add `/usr/src/app/node_modules/.bin` to $PATH
ENV PATH /usr/src/app/node_modules/.bin:$PATH

# install and cache app dependencies
COPY Espresso.WebApi/ClientApp/package.json /usr/src/app/package.json
COPY Espresso.WebApi/ClientApp/.npmrc /usr/src/app/.npmrc
RUN npm install

# add app
COPY Espresso.WebApi/ClientApp/. /usr/src/app
ENV REACT_APP_ENVIRONMENT=$REACT_APP_ENVIRONMENT
RUN npm run build-frontend
#End React build

FROM base AS final
# ASP.NET Core  
WORKDIR /app
COPY --from=publish /app/publish .
# React
RUN mkdir -p /app/ClientApp/build
COPY --from=nodebuilder /usr/src/app/build/. /app/ClientApp/build

ENTRYPOINT ["dotnet", "Espresso.WebApi.dll"]

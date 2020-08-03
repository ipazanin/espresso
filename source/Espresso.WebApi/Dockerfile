FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /source
COPY ["Espresso.WebApi/Espresso.WebApi.csproj", "Espresso.WebApi/"]
COPY ["Espresso.Domain/Espresso.Domain.csproj", "Espresso.Domain/"]
COPY ["Espresso.Common/Espresso.Common.csproj", "Espresso.Common/"]
COPY ["Espresso.Persistence/Espresso.Persistence.csproj", "Espresso.Persistence/"]
COPY ["Espresso.Application/Espresso.Application.csproj", "Espresso.Application/"]
RUN dotnet restore "Espresso.WebApi/Espresso.WebApi.csproj"
COPY . .
WORKDIR "/source/Espresso.WebApi"
RUN dotnet build "Espresso.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Espresso.WebApi.csproj" -c Release -o /app/publish

#Angular build
FROM node AS nodebuilder
ARG REACT_APP_ENVIRONMENT

# set working directory
RUN mkdir /usr/src/app
WORKDIR /usr/src/app

# add `/usr/src/app/node_modules/.bin` to $PATH
ENV PATH /usr/src/app/node_modules/.bin:$PATH

# install and cache app dependencies
COPY Espresso.WebApi/ClientApp/package.json /usr/src/app/package.json
RUN npm install
# RUN npm install -g @angular/cli@1.7.0 --unsafe

# add app

COPY Espresso.WebApi/ClientApp/. /usr/src/app
RUN echo IPAZANIN $REACT_APP_ENVIRONMENT
ENV REACT_APP_ENVIRONMENT=$REACT_APP_ENVIRONMENT
RUN npm run build

#End Angular build


FROM base AS final
# ASP.NET Core  
WORKDIR /app
COPY --from=publish /app/publish .
# React
RUN mkdir -p /app/ClientApp/build
COPY --from=nodebuilder /usr/src/app/build/. /app/ClientApp/build

ENTRYPOINT ["dotnet", "Espresso.WebApi.dll"]

# Common
all : 
	list pre-commit \
	build compose-database compose-development \
	compose-local database-update docker-build-webapi \
	docker-build-parserdeleter docker-build migration-add \
	migration-remove update restore start-p start-w \
	test-coverage test \
	lint test-frontend

.PHONY : all
list:
	@$(MAKE) -pRrq -f $(lastword $(MAKEFILE_LIST)) : 2>/dev/null | \
	awk -v RS= -F: '/^# File/,/^# Finished Make data base/ \
	{if ($$1 !~ "^[#.]") {print $$1}}' | sort | egrep -v -e '^[^[:alnum:]]' -e '^$@$$'

pre-commit:
	make build
	make test
	make lint
	make test-frontend

# Backend
build:
	dotnet build --configuration Release --no-restore source/Espresso.sln

compose-database:
ifeq ($(arg1), up)
	docker-compose -f ./compose/docker-compose-database.yml up \
	--build --remove-orphans $(arg2)
else ifeq ($(arg1), down)
	docker-compose -f ./compose/docker-compose-database.yml down
else
	echo "Invalid Argument. Accepted arguments: up, down"
endif

compose-development:
ifeq ($(arg), up)
	sudo docker-compose -f ./compose/docker-compose-development.yml up \
	--build --remove-orphans 
else ifeq ($(arg), down)
	sudo docker-compose -f ./compose/docker-compose-development.yml down
else
	echo "Invalid Argument. Accepted arguments: up, down"
endif

compose-local:
ifeq ($(arg), up)
	docker-compose -f ./compose/docker-compose-local.yml up --build --remove-orphans
else ifeq ($(arg), down)
	docker-compose -f ./compose/docker-compose-local.yml down
else
	echo "Invalid Argument. Accepted arguments: up, down"
endif

database-update:
	ASPNETCORE_ENVIRONMENT=local-local-db dotnet ef database update -p \
	./source/Espresso.Persistence/Espresso.Persistence.csproj -v

docker-build-webapi:
	docker build --force-rm -f ./source/Espresso.WebApi/Dockerfile -t \
	ipazanin/espresso-webapi:$(v) --build-arg REACT_APP_ENVIRONMENT=production ./source \
	&& docker push ipazanin/espresso-webapi:$(v)

docker-build-parserdeleter:
	docker build --force-rm -f ./source/Espresso.ParserDeleter/Dockerfile -t \
	ipazanin/espresso-parserdeleter:$(v) ./source \
	&& docker push ipazanin/espresso-parserdeleter:$(v)

docker-build:
	make docker-build-webapi v=$(v)
	make docker-build-parserdeleter v=$(v)

migration-add:
	ASPNETCORE_ENVIRONMENT=local-local-db dotnet ef migrations \
	add -p ./source/Espresso.Persistence/Espresso.Persistence.csproj -v $(name)

migration-remove:
	ASPNETCORE_ENVIRONMENT=local-local-db dotnet ef migrations \
	remove -p ./source/Espresso.Persistence/Espresso.Persistence.csproj -v

update:
	./scripts/update.sh

restore:
	dotnet restore ./source/Espresso.sln

start-p:
	make compose-database arg1=up arg2="-d"
ifeq ($(strip $(arg)),)
	ASPNETCORE_ENVIRONMENT=local-local-db dotnet run --project \
	./source/Espresso.ParserDeleter/Espresso.ParserDeleter.csproj --urls "http://localhost:9000"
else ifeq ($(arg), watch)
	ASPNETCORE_ENVIRONMENT=local-local-db dotnet watch --project \
	./source/Espresso.ParserDeleter/Espresso.ParserDeleter.csproj run --urls "http://localhost:9000" 
else
	echo "Invalid Argument. Accepted arguments: {empty}, watch}"
endif

start-w:
	make compose-database arg1=up arg2="-d"
ifeq ($(strip $(arg)),)
	ASPNETCORE_ENVIRONMENT=local-local-db dotnet run --project \
	./source/Espresso.WebApi/Espresso.WebApi.csproj --urls "http://localhost:8000"
else ifeq ($(arg), watch)
	ASPNETCORE_ENVIRONMENT=local-local-db dotnet watch --project \
	./source/Espresso.WebApi/Espresso.WebApi.csproj run --urls "http://localhost:8000" 
else
	echo "Invalid Argument. Accepted arguments: {empty}, watch}"
endif

test-coverage:
	sudo dotnet test --logger 'trx;LogFileName=TestResults.trx' \
	--logger 'xunit;LogFileName=TestResults.xml' \
	--results-directory ./tests/UnitTests/TestReports/UnitTests \
	/p:CollectCoverage=true /p:CoverletOutput=TestReports/Coverage/ \
	/p:CoverletOutputFormat=cobertura ./source/Espresso.sln

test:
	dotnet test --verbosity normal source/Espresso.sln

# Frontend Scripts
FRONTEND_DIRECTORY=./source/Espresso.WebApi/ClientApp

.PHONY: lint
lint:
	cd $(FRONTEND_DIRECTORY); \
	./node_modules/.bin/eslint --ext .ts,.tsx src/ \
	--fix --cache --cache-location=./node_modules/.cache/

test-frontend:
	cd $(FRONTEND_DIRECTORY); \
	CI=true REACT_APP_ENVIRONMENT=test \
	./node_modules/.bin/react-scripts test


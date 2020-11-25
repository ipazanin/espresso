# Constants
CLIENTAPP_DIRECTORY=./source/Espresso.ClientApp

# Common
list::
	@$(MAKE) -pRrq -f $(lastword $(MAKEFILE_LIST)) : 2>/dev/null | \
	awk -v RS= -F: '/^# File/,/^# Finished Make data base/ \
	{if ($$1 !~ "^[#.]") {print $$1}}' | sort | egrep -v -e '^[^[:alnum:]]' -e '^$@$$'

health-check::
	make health-check-backend
	make health-check-frontend

rebuild::
	make rebuild-backend
	make rebuild-frontend

# Backend
health-check-backend::
	make restore
	make build
	make test

clean::
	dotnet clean --configuration Release --verbosity minimal source/Espresso.sln && \
	find . -iname "bin" -o -iname "obj" | xargs rm -rf

rebuild-backend::
	make clean
	make build

build::
	dotnet build --configuration Release source/Espresso.sln

compose-database::
ifeq ($(arg1), up)
	docker-compose \
	-f ./compose/database.yml \
	-f ./compose/database-environment.yml  \
	up --build $(arg2)
else ifeq ($(strip $(arg1)),)
	docker-compose \
	-f ./compose/database.yml \
	-f ./compose/database-environment.yml \
	up --build $(arg2)
else ifeq ($(arg1), down)
	docker-compose -f ./compose/database.yml -f ./compose/database-environment.yml down
else
	echo "Invalid Argument. Accepted arguments: up, down"
endif

compose-rabbitmq::
ifeq ($(arg), up)
	docker-compose \
	-f ./compose/rabbitmq.yml \
	-f ./compose/rabbitmq-environment.yml \
	up --build
else ifeq ($(strip $(arg)),)
	docker-compose \
	-f ./compose/rabbitmq.yml \
	-f ./compose/rabbitmq-environment.yml \
	up --build
else ifeq ($(arg), down)
	docker-compose \
	-f ./compose/rabbitmq.yml \
	-f ./compose/rabbitmq-environment.yml \
	down
else
	echo "Invalid Argument. Accepted arguments: up, down"
endif

compose-parser::
ifeq ($(arg), up)
	docker-compose \
	-f ./compose/database.yml \
	-f ./compose/database-environment.yml \	
	-f ./compose/parser.yml \
	-f ./compose/parser-environment.yml \
	up --build
else ifeq ($(strip $(arg)),)
	docker-compose \
	-f ./compose/database.yml \
	-f ./compose/database-environment.yml \	
	-f ./compose/parser.yml \
	-f ./compose/parser-environment.yml \
	up --build
else ifeq ($(arg), down)
	docker-compose \
	-f ./compose/database.yml \
	-f ./compose/database-environment.yml \	
	-f ./compose/parser.yml \
	-f ./compose/parser-environment.yml \
	down
else
	echo "Invalid Argument. Accepted arguments: up, down"
endif

compose-webapi::
ifeq ($(arg), up)
	docker-compose \
	-f ./compose/database.yml \
	-f ./compose/database-environment.yml \
	-f ./compose/webapi.yml \
	-f ./compose/webapi-environment.yml \
	up --build
else ifeq ($(strip $(arg)),)
	docker-compose \
	-f ./compose/database.yml \
	-f ./compose/database-environment.yml \
	-f ./compose/webapi.yml \
	-f ./compose/webapi-environment.yml \
	up --build
else ifeq ($(arg), down)
	docker-compose \
	-f ./compose/database.yml \
	-f ./compose/database-environment.yml \
	-f ./compose/webapi.yml \
	-f ./compose/webapi-environment.yml \
	down
else
	echo "Invalid Argument. Accepted arguments: up, down"
endif

compose-local::
ifeq ($(arg), up)
	docker-compose \
	-f ./compose/database.yml \
	-f ./compose/database-environment.yml \
	-f ./compose/webapi.yml \
	-f ./compose/webapi-environment.yml \
	-f ./compose/parser.yml \
	-f ./compose/parser-environment.yml \
	up --build
else ifeq ($(strip $(arg)),)
	docker-compose \
	-f ./compose/database.yml \
	-f ./compose/database-environment.yml \
	-f ./compose/webapi.yml \
	-f ./compose/webapi-environment.yml \
	-f ./compose/parser.yml \
	-f ./compose/parser-environment.yml \
	up --build
else ifeq ($(arg), down)
	docker-compose \
	-f ./compose/database.yml \
	-f ./compose/database-environment.yml \
	-f ./compose/webapi.yml \
	-f ./compose/webapi-environment.yml \
	-f ./compose/parser.yml \
	-f ./compose/parser-environment.yml \
	down
else
	echo "Invalid Argument. Accepted arguments: up, down"
endif

compose-local-rabbitmq::
ifeq ($(arg), up)
	docker-compose \
	-f ./compose/database.yml \
	-f ./compose/database-environment.yml \
	-f ./compose/webapi.yml \
	-f ./compose/webapi-environment.yml \
	-f ./compose/parser.yml \
	-f ./compose/parser-environment.yml \
	-f ./compose/rabbitmq.yml \
	-f ./compose/rabbitmq-environment.yml \
	up --build
else ifeq ($(strip $(arg)),)
	docker-compose \
	-f ./compose/database.yml \
	-f ./compose/database-environment.yml \
	-f ./compose/webapi.yml \
	-f ./compose/webapi-environment.yml \
	-f ./compose/parser.yml \
	-f ./compose/parser-environment.yml \
	-f ./compose/rabbitmq.yml \
	-f ./compose/rabbitmq-environment.yml \
	up --build
else ifeq ($(arg), down)
	docker-compose \
	-f ./compose/database.yml \
	-f ./compose/database-environment.yml \
	-f ./compose/webapi.yml \
	-f ./compose/webapi-environment.yml \
	-f ./compose/parser.yml \
	-f ./compose/parser-environment.yml \
	-f ./compose/rabbitmq.yml \
	-f ./compose/rabbitmq-environment.yml \
	down
else
	echo "Invalid Argument. Accepted arguments: up, down"
endif

database-update::
	ASPNETCORE_ENVIRONMENT=local-production-db dotnet ef database update -p \
	./source/Espresso.Persistence/Espresso.Persistence.csproj -v

docker-build-webapi::
ifeq ($(strip $(v)),)
	docker build --force-rm -f ./source/Espresso.WebApi/Dockerfile -t \
	docker.pkg.github.com/espresso-news/espresso-backend/espresso-webapi:latest --build-arg REACT_APP_ENVIRONMENT=production ./source
	docker push docker.pkg.github.com/espresso-news/espresso-backend/espresso-webapi:latest
else
	docker build --force-rm -f ./source/Espresso.WebApi/Dockerfile -t \
	docker.pkg.github.com/espresso-news/espresso-backend/espresso-webapi:$(v) --build-arg REACT_APP_ENVIRONMENT=production ./source
	docker push docker.pkg.github.com/espresso-news/espresso-backend/espresso-webapi:$(v)
endif

docker-build-parserdeleter::
ifeq ($(strip $(v)),)
	docker build --force-rm -f ./source/Espresso.ParserDeleter/Dockerfile -t \
	docker.pkg.github.com/espresso-news/espresso-backend/espresso-parserdeleter:latest ./source
	docker push docker.pkg.github.com/espresso-news/espresso-backend/espresso-parserdeleter:latest
else
	docker build --force-rm -f ./source/Espresso.ParserDeleter/Dockerfile -t \
	docker.pkg.github.com/espresso-news/espresso-backend/espresso-parserdeleter:$(v) ./source
	docker push docker.pkg.github.com/espresso-news/espresso-backend/espresso-parserdeleter:$(v)
endif

docker-build::
	make docker-build-webapi v=$(v)
	make docker-build-parserdeleter v=$(v)

migration-add::
	ASPNETCORE_ENVIRONMENT=local-local-db dotnet ef migrations \
	add -p ./source/Espresso.Persistence/Espresso.Persistence.csproj -v $(name)

migration-remove::
	ASPNETCORE_ENVIRONMENT=local-local-db dotnet ef migrations \
	remove -p ./source/Espresso.Persistence/Espresso.Persistence.csproj -v

update::
	./scripts/update.sh

restore::
	dotnet restore ./source/Espresso.sln

test::
ifeq ($(strip $(verbosity)),)
	dotnet test --verbosity minimal source/Espresso.sln
else
	dotnet test --verbosity $(verbosity) source/Espresso.sln
endif

test-coverage::
	dotnet test \
	--verbosity minimal \
	--logger 'xunit;LogFileName=TestResults.xml' \
	--results-directory ./tests/UnitTests/TestReports \
	/p:CollectCoverage=true \
	/p:CoverletOutput=TestReports/ \
	/p:CoverletOutputFormat=cobertura \
	./source/Espresso.sln
	
create-coverage-report::
	sudo reportgenerator \
	"-reports:tests/UnitTests/*/*/*.xml" \
	"-targetdir:tests/UnitTests/TestReports" \
	-reporttypes:Html

coverage:
	make test-coverage
	make create-coverage-report

# Frontend Scripts
health-check-frontend::
	make install
	make build-frontend
	make lint
	make test-frontend

rebuild-frontend::
	cd $(CLIENTAPP_DIRECTORY); \
	rm -rf node_modules \
	rm -rf build
	make install
	make build-frontend

install::
	cd $(CLIENTAPP_DIRECTORY); \
	npm install

build-frontend::
	cd $(CLIENTAPP_DIRECTORY); \
	REACT_APP_ENVIRONMENT=production \
	./node_modules/.bin/react-scripts build

lint::
	cd $(CLIENTAPP_DIRECTORY); \
	./node_modules/.bin/eslint --ext .ts,.tsx src/ \
	--fix --cache --cache-location=./node_modules/.cache/

test-frontend::
	cd $(CLIENTAPP_DIRECTORY); \
	CI=true REACT_APP_ENVIRONMENT=test \
	./node_modules/.bin/react-scripts test

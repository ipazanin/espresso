#######################################################
#                      common
#######################################################
list::
	@$(MAKE) -pRrq -f $(lastword $(MAKEFILE_LIST)) : 2>/dev/null | \
	awk -v RS= -F: '/^# File/,/^# Finished Make data base/ \
	{if ($$1 !~ "^[#.]") {print $$1}}' | sort | egrep -v -e '^[^[:alnum:]]' -e '^$@$$'

health-check::
	make health-check-backend

rebuild::
	make rebuild-backend
	make rebuild-frontend

#######################################################
#                      docker
#######################################################
DefaultDockerImageTag=latest

EspressoWebApiDockerfilePath="source/server/WebApi/Espresso.WebApi/Dockerfile"
EspressoParserDockerfilePath="source/server/Dashboard/Espresso.Dashboard/Dockerfile"

DockerBuildContextPath="."

WebApiDockerImage="ghcr.io/espresso-news/espresso-backend/espresso-webapi"
DashboardDockerImage="ghcr.io/espresso-news/espresso-backend/espresso-dashboard"

WebApiDockerImageGoogleContainerRegistry="gcr.io/espresso-8c4ac/espresso-webapi"
DashboardDockerImageGoogleContainerRegistry="gcr.io/espresso-8c4ac/espresso-dashboard"

DefaultReactEnvironment="production"

DatabaseComposeFile="scripts/compose/database.yml"
DatabaseEnvironmentComposeFile="scripts/compose/database-environment-postgres.yml"
DashboardComposeFile="scripts/compose/dashboard.yml"
DashboardEnvironmentComposeFile="scripts/compose/dashboard-environment-postgres.yml"
WebApiComposeFile="scripts/compose/webapi.yml"
WebApiEnvironmentComposeFile="scripts/compose/webapi-environment-postgres.yml"
RabbitMqComposeFile="scripts/compose/rabbitmq.yml"
RabbitMqEnvironmentComposeFile="scripts/compose/rabbitmq-environment.yml"


compose-database::
ifeq ($(arg1), up)
	docker-compose \
	-f $(DatabaseComposeFile) \
	-f $(DatabaseEnvironmentComposeFile)  \
	up --build $(arg2)
else ifeq ($(strip $(arg1)),)
	docker-compose \
	-f $(DatabaseComposeFile) \
	-f $(DatabaseEnvironmentComposeFile) \
	up --build $(arg2)
else ifeq ($(arg1), down)
	docker-compose -f $(DatabaseComposeFile) -f $(DatabaseEnvironmentComposeFile) down
else
	echo "Invalid Argument. Accepted arguments: up, down"
endif

compose-rabbitmq::
ifeq ($(arg), up)
	docker-compose \
	-f $(RabbitMqComposeFile) \
	-f $(RabbitMqEnvironmentComposeFile) \
	up --build
else ifeq ($(strip $(arg)),)
	docker-compose \
	-f $(RabbitMqComposeFile) \
	-f $(RabbitMqEnvironmentComposeFile) \
	up --build
else ifeq ($(arg), down)
	docker-compose \
	-f $(RabbitMqComposeFile) \
	-f $(RabbitMqEnvironmentComposeFile) \
	down
else
	echo "Invalid Argument. Accepted arguments: up, down"
endif

compose-dashboard::
ifeq ($(arg), up)
	docker-compose \
	-f $(DatabaseComposeFile) \
	-f $(DatabaseEnvironmentComposeFile) \
	-f $(DashboardComposeFile) \
	-f $(DashboardEnvironmentComposeFile) \
	up --build
else ifeq ($(strip $(arg)),)
	docker-compose \
	-f $(DatabaseComposeFile) \
	-f $(DatabaseEnvironmentComposeFile) \
	-f $(DashboardComposeFile) \
	-f $(DashboardEnvironmentComposeFile) \
	up --build
else ifeq ($(arg), down)
	docker-compose \
	-f $(DatabaseComposeFile) \
	-f $(DatabaseEnvironmentComposeFile) \
	-f $(DashboardComposeFile) \
	-f $(DashboardEnvironmentComposeFile) \
	down
else
	echo "Invalid Argument. Accepted arguments: up, down"
endif

compose-webapi::
ifeq ($(arg), up)
	docker-compose \
	-f $(DatabaseComposeFile) \
	-f $(DatabaseEnvironmentComposeFile) \
	-f $(WebApiComposeFile) \
	-f $(WebApiEnvironmentComposeFile) \
	up --build
else ifeq ($(strip $(arg)),)
	docker-compose \
	-f $(DatabaseComposeFile) \
	-f $(DatabaseEnvironmentComposeFile) \
	-f $(WebApiComposeFile) \
	-f $(WebApiEnvironmentComposeFile) \
	up --build
else ifeq ($(arg), down)
	docker-compose \
	-f $(DatabaseComposeFile) \
	-f $(DatabaseEnvironmentComposeFile) \
	-f $(WebApiComposeFile) \
	-f $(WebApiEnvironmentComposeFile) \
	down
else
	echo "Invalid Argument. Accepted arguments: up, down"
endif

compose-local::
ifeq ($(arg), up)
	docker-compose \
	-f $(DatabaseComposeFile) \
	-f $(DatabaseEnvironmentComposeFile) \
	-f $(WebApiComposeFile) \
	-f $(WebApiEnvironmentComposeFile) \
	-f $(DashboardComposeFile) \
	-f $(DashboardEnvironmentComposeFile) \
	up --build
else ifeq ($(strip $(arg)),)
	docker-compose \
	-f $(DatabaseComposeFile) \
	-f $(DatabaseEnvironmentComposeFile) \
	-f $(WebApiComposeFile) \
	-f $(WebApiEnvironmentComposeFile) \
	-f $(DashboardComposeFile) \
	-f $(DashboardEnvironmentComposeFile) \
	up --build
else ifeq ($(arg), down)
	docker-compose \
	-f $(DatabaseComposeFile) \
	-f $(DatabaseEnvironmentComposeFile) \
	-f $(WebApiComposeFile) \
	-f $(WebApiEnvironmentComposeFile) \
	-f $(DashboardComposeFile) \
	-f $(DashboardEnvironmentComposeFile) \
	down
else
	echo "Invalid Argument. Accepted arguments: up, down"
endif

compose-local-rabbitmq::
ifeq ($(arg), up)
	docker-compose \
	-f $(DatabaseComposeFile) \
	-f $(DatabaseEnvironmentComposeFile) \
	-f $(WebApiComposeFile) \
	-f $(WebApiEnvironmentComposeFile) \
	-f $(DashboardComposeFile) \
	-f $(DashboardEnvironmentComposeFile) \
	-f $(RabbitMqComposeFile) \
	-f $(RabbitMqEnvironmentComposeFile) \
	up --build
else ifeq ($(strip $(arg)),)
	docker-compose \
	-f $(DatabaseComposeFile) \
	-f $(DatabaseEnvironmentComposeFile) \
	-f $(WebApiComposeFile) \
	-f $(WebApiEnvironmentComposeFile) \
	-f $(DashboardComposeFile) \
	-f $(DashboardEnvironmentComposeFile) \
	-f $(RabbitMqComposeFile) \
	-f $(RabbitMqEnvironmentComposeFile) \
	up --build
else ifeq ($(arg), down)
	docker-compose \
	-f $(DatabaseComposeFile) \
	-f $(DatabaseEnvironmentComposeFile) \
	-f $(WebApiComposeFile) \
	-f $(WebApiEnvironmentComposeFile) \
	-f $(DashboardComposeFile) \
	-f $(DashboardEnvironmentComposeFile) \
	-f $(RabbitMqComposeFile) \
	-f $(RabbitMqEnvironmentComposeFile) \
	down
else
	echo "Invalid Argument. Accepted arguments: up, down"
endif

docker-build-webapi::
ifeq ($(strip $(v)),)
	docker build \
	-f $(EspressoWebApiDockerfilePath) \
	-t $(WebApiDockerImage):$(DefaultDockerImageTag) \
	--platform linux/amd64 \
	--build-arg REACT_APP_ENVIRONMENT=$(DefaultReactEnvironment) \
	$(DockerBuildContextPath)
	docker push $(WebApiDockerImage):$(DefaultDockerImageTag)
	docker build \
	-f $(EspressoWebApiDockerfilePath) \
	-t $(WebApiDockerImageGoogleContainerRegistry):$(DefaultDockerImageTag) \
	--platform linux/amd64 \
	--build-arg REACT_APP_ENVIRONMENT=$(DefaultReactEnvironment) \
	$(DockerBuildContextPath)
	docker push $(WebApiDockerImageGoogleContainerRegistry):$(DefaultDockerImageTag)	
else
	docker build \
	-f $(EspressoWebApiDockerfilePath) \
	-t $(WebApiDockerImage):$(v) \
	--platform linux/amd64 \
	--build-arg REACT_APP_ENVIRONMENT=$(DefaultReactEnvironment) \
	$(DockerBuildContextPath)
	docker push $(WebApiDockerImage):$(v)
	docker build \
	-f $(EspressoWebApiDockerfilePath) \
	-t $(WebApiDockerImageGoogleContainerRegistry):$(v) \
	--platform linux/amd64 \
	--build-arg REACT_APP_ENVIRONMENT=$(DefaultReactEnvironment) \
	$(DockerBuildContextPath)
	docker push $(WebApiDockerImageGoogleContainerRegistry):$(v)	
endif

docker-build-dashboard::
ifeq ($(strip $(v)),)
	docker build \
	-f $(EspressoParserDockerfilePath) \
	-t $(DashboardDockerImage):$(DefaultDockerImageTag) \
	--platform linux/amd64 \
	$(DockerBuildContextPath)
	docker push $(DashboardDockerImage):$(DefaultDockerImageTag)
	docker build \
	-f $(EspressoParserDockerfilePath) \
	-t $(DashboardDockerImageGoogleContainerRegistry):$(DefaultDockerImageTag) \
	--platform linux/amd64 \
	$(DockerBuildContextPath)
	docker push $(DashboardDockerImageGoogleContainerRegistry):$(DefaultDockerImageTag)	
else
	docker build \
	-f $(EspressoParserDockerfilePath) \
	-t $(DashboardDockerImage):$(v) \
	--platform linux/amd64 \
	$(DockerBuildContextPath)
	docker push $(DashboardDockerImage):$(v)
	docker build \
	-f $(EspressoParserDockerfilePath) \
	-t $(DashboardDockerImageGoogleContainerRegistry):$(v) \
	--platform linux/amd64 \
	$(DockerBuildContextPath)
	docker push $(DashboardDockerImageGoogleContainerRegistry):$(v)		
endif

docker-build::
	make docker-build-webapi v=$(v)
	make docker-build-dashboard v=$(v)

#######################################################
#                      backend
#######################################################
SolutionPath="Espresso.sln"
NugetConfigPath="nuget.config"
PersistenceProjectPath="source/server/Shared/Espresso.Persistence/Espresso.Persistence.csproj"
DefaultVerbosity="minimal" # verbosity levels: quiet, minimal, normal, detailed, diagnostic
LocalEnvironmentName="local"
DebugConfiguration="Debug"
DefaultConfiguration=$(DebugConfiguration) # Configurations: Release, Debug

WebApiProjectPath="source/server/WebApi/Espresso.WebApi/Espresso.WebApi.csproj"
DashboardProjectPath="source/server/Dashboard/Espresso.Dashboard/Espresso.Dashboard.csproj"
WebApiUrls="http://localhost:8000"
DashboardUrls="http://localhost:9000"
ReleaseConfiguration="Release"
DebugConfiguration="Debug"

AdminUserPassword="Opatija123"
SlackWebHook="https://hooks.slack.com/services/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"
EspressoDatabaseConnectionString="User ID=sa;Password=Opatija1;Host=localhost;Port=1433;Database=EspressoDb;" #"Server=localhost,1433;Database=EspressoDb;Application Name=Espresso;User=sa;Password=Opatija1;"
EspressoIdentityDatabaseConnectionString="User ID=sa;Password=Opatija1;Host=localhost;Port=1433;Database=EspressoIdentityDb;" #"Server=localhost,1433;Database=EspressoIdentityDb;Application Name=EspressoIdentity;User=sa;Password=Opatija1;"

UnitTestResultsPath="server/UnitTests/TestReports"
UnitTestRelativeResultsPath="TestReports"
UnitTestsDirectory="server/UnitTests"
UnitTestsLogFileName="TestResults.xml"

EspressoDatabaseContextName="EspressoDatabaseContext"
EspressoIdentityDatabaseContextName="EspressoIdentityDatabaseContext"
EspressoDatabaseMigrationsFolder="EspressoDatabaseMigrations"
EspressoIdentityDatabaseMigrationsFolder="IdentityDatabaseMigrations"

DashboardLocalLaunchProfile='dashboard-local'
WebApiLocalLaunchProfile='web-api-local'

health-check-backend::
	make restore
	make build
	make test
#	make consolidate # requires dotnet 3.1 SKD
	make format

rebuild-backend::
	make clean
	make build

clean::
	dotnet clean --configuration $(DefaultConfiguration) --verbosity $(DefaultVerbosity) $(SolutionPath)
	dotnet clean --configuration $(DebugConfiguration) --verbosity $(DefaultVerbosity) $(SolutionPath)

build::
ifeq ($(strip $(verbosity)),)
	dotnet build \
	--configuration $(DefaultConfiguration) \
	--verbosity $(DefaultVerbosity) \
	--configfile $(NugetConfigPath) \
	$(SolutionPath)
else
	dotnet build \
	--configuration $(DefaultConfiguration) \
	--verbosity $(verbosity) \
	$(SolutionPath)
endif

publish::
ifeq ($(strip $(verbosity)),)
	dotnet publish \
	--configuration $(DefaultConfiguration) \
	--verbosity $(DefaultVerbosity) \
	$(SolutionPath)
else
	dotnet publish \
	--configuration $(DefaultConfiguration) \
	--verbosity $(verbosity) \
	$(SolutionPath)
endif

database-update-espresso-database::
	dotnet ef database update \
    --project $(PersistenceProjectPath) \
    --context $(EspressoDatabaseContextName) \
    --configuration $(DefaultConfiguration) \
    --verbose \
    -- $(EspressoDatabaseConnectionString)

migration-add-espresso-database::
	dotnet ef migrations add \
    --project $(PersistenceProjectPath) \
    --context $(EspressoDatabaseContextName) \
    --output-dir $(EspressoDatabaseMigrationsFolder) \
    --configuration $(DefaultConfiguration) \
    --verbose \
    $(name) \
    -- $(EspressoDatabaseConnectionString)

migration-remove-espresso-database::
	dotnet ef migrations remove \
    --project $(PersistenceProjectPath) \
    --context $(EspressoDatabaseContextName) \
    --configuration $(DefaultConfiguration) \
	--verbose \
    -- $(EspressoDatabaseConnectionString)

database-update-identity-database::
	dotnet ef database update \
    --project $(PersistenceProjectPath) \
    --context $(EspressoIdentityDatabaseContextName) \
    --configuration $(DefaultConfiguration) \
    --verbose \
    -- $(EspressoIdentityDatabaseConnectionString)

migration-add-identity-database::
	dotnet ef migrations add \
    --project $(PersistenceProjectPath) \
    --context $(EspressoIdentityDatabaseContextName) \
    --output-dir $(EspressoIdentityDatabaseMigrationsFolder) \
    --configuration $(DefaultConfiguration) \
    --verbose \
    $(name) \
    -- $(EspressoIdentityDatabaseConnectionString)

migration-remove-identity-database::
	dotnet ef migrations remove \
    --project $(PersistenceProjectPath) \
    --context $(EspressoIdentityDatabaseContextName) \
    --output-dir $(EspressoIdentityDatabaseMigrationsFolder) \
    --configuration $(DefaultConfiguration) \
	--verbose \
    -- $(EspressoIdentityDatabaseConnectionString)

update::
	./scripts/update.sh

restore::
	dotnet restore --configfile $(NugetConfigPath) $(SolutionPath)

consolidate::
#	dotnet consolidate --solutions $(SolutionPath)

format::
#	dotnet format --verify-no-changes --no-restore $(SolutionPath)

format-fix::
#	dotnet format --no-restore $(SolutionPath)

test::
ifeq ($(strip $(verbosity)),)
	dotnet test --verbosity $(DefaultVerbosity) $(SolutionPath)
else
	dotnet test --verbosity $(verbosity) $(SolutionPath)
endif

test-coverage::
ifeq ($(strip $(verbosity)),)
	dotnet test \
	--verbosity $(DefaultVerbosity) \
	--logger 'xunit;LogFileName=$(UnitTestsLogFileName)' \
	--results-directory $(UnitTestResultsPath) \
	/p:CollectCoverage=true \
	/p:CoverletOutput=$(UnitTestRelativeResultsPath)/ \
	/p:CoverletOutputFormat=cobertura \
	$(SolutionPath)
else
	dotnet test \
	--verbosity $(verbosity) \
	--logger 'xunit;LogFileName=$(UnitTestsLogFileName)' \
	--results-directory $(UnitTestResultsPath) \
	/p:CollectCoverage=true \
	/p:CoverletOutput=$(UnitTestRelativeResultsPath)/ \
	/p:CoverletOutputFormat=cobertura \
	$(SolutionPath)
endif

create-coverage-report::
	sudo reportgenerator \
	"-reports:$(UnitTestsDirectory)/*/*/*.xml" \
	"-targetdir:$(UnitTestResultsPath)" \
	-reporttypes:Html

coverage:
	make test-coverage verbosity=quiet
	make create-coverage-report verbosity=quiet

infer-csharp::
	docker-compose -f ./scripts/analysis/infer-csharp.yml up --build

start-dashboard::
	dotnet run --project $(DashboardProjectPath) --configuration $(ReleaseConfiguration) --launch-profile $(DashboardLocalLaunchProfile)

# watch is missing because of: https://github.com/dotnet/sdk/issues/24115
watch-dashboard::
	dotnet watch run --project $(DashboardProjectPath)

start-webapi::
	dotnet run --project $(WebApiProjectPath) --configuration $(ReleaseConfiguration) --launch-profile $(WebApiLocalLaunchProfile)

# watch is missing because of: https://github.com/dotnet/sdk/issues/24115
watch-webapi::
	dotnet watch run --project $(WebApiProjectPath) --configuration $(DebugConfiguration)


#######################################################
#                      frontend
#######################################################
ClientAppDirectory="source/client"
TestReactAppEnvironment="test"

health-check-frontend::
	make install
	make build-frontend
	make lint
	make test-frontend

rebuild-frontend::
	cd $(ClientAppDirectory)
	rm -rf node_modules
	rm -rf build
	make install
	make build-frontend

install::
	cd $(ClientAppDirectory); \
	npm install

build-frontend::
	cd $(ClientAppDirectory); \
	REACT_APP_ENVIRONMENT=production \
	./node_modules/.bin/react-scripts build

lint::
	cd $(ClientAppDirectory); \
	./node_modules/.bin/eslint --ext .ts,.tsx src/ \
	--fix --cache --cache-location=./node_modules/.cache/

test-frontend::
	cd $(ClientAppDirectory); \
	CI=true REACT_APP_ENVIRONMENT=test \
	./node_modules/.bin/react-scripts test


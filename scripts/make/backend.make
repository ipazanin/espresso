SolutionPath="source/server/Espresso/Espresso.sln"
NugetConfigPath="source/server/Espresso/nuget.config"
PersistenceProjectPath="source/server/Espresso/Espresso.Persistence/Espresso.Persistence.csproj"
DefaultVerbosity="minimal" # verbosity levels: quiet, minimal, normal, detailed, diagnostic
LocalEnvironmentName="local"
DefaultConfiguration="Release" # Configurations: Release, Debug
DebugConfiguration="Debug"

WebApiProjectPath="source/server/Espresso/Espresso.WebApi/Espresso.WebApi.csproj"
ParserProjectPath="source/server/Espresso/Espresso.Dashboard/Espresso.Dashboard.csproj"
WebApiUrls="http://localhost:8000"
DashboardUrls="http://localhost:9000"
ReleaseConfiguration="Release"
DebugConfiguration="Debug"

AdminUserPassword="Opatija123"
SlackWebHook="https://hooks.slack.com/services/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"
EspressoDatabaseConnectionString="Server=localhost,1433;Database=EspressoDb;Application Name=Espresso;User=sa;Password=Opatija1;"
EspressoIdentityDatabaseConnectionString="Server=localhost,1433;Database=EspressoIdentityDb;Application Name=EspressoIdentity;User=sa;Password=Opatija1;"

UnitTestResultsPath="server/UnitTests/TestReports"
UnitTestRelativeResultsPath="TestReports"
UnitTestsDirectory="server/UnitTests"
UnitTestsLogFileName="TestResults.xml"

EspressoDatabaseContextName="EspressoDatabaseContext"
EspressoIdentityDatabaseContextName="IdentityDatabaseContext"
EspressoDatabaseMigrationsFolder="EspressoDatabaseMigrations"
EspressoIdentityDatabaseMigrationsFolder="IdentityDatabaseMigrations"

LocalLaunchProfile='local'

list::
	@$(MAKE) -pRrq -f $(lastword $(MAKEFILE_LIST)) : 2>/dev/null | \
	awk -v RS= -F: '/^# File/,/^# Finished Make data base/ \
	{if ($$1 !~ "^[#.]") {print $$1}}' | sort | egrep -v -e '^[^[:alnum:]]' -e '^$@$$'

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
    --output-dir $(EspressoDatabaseMigrationsFolder) \
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

infer-csharp::
	docker-compose -f ./scripts/analysis/infer-csharp.yml up --build

start-dashboard::
ifeq ($(strip $(arg)),)
	dotnet run --project $(ParserProjectPath) --configuration $(ReleaseConfiguration) --launch-profile $(LocalLaunchProfile)
else
	dotnet watch --project $(ParserProjectPath) run --configuration $(DebugConfiguration)
endif

start-webapi::
	make compose-database arg1=up arg2="-d"
ifeq ($(strip $(arg)),)
	ASPNETCORE_ENVIRONMENT=local \
    DATABASE_NAME=local \
    APIKEYSCONFIGURATION__ANDROID='cfb490b1-b392-49a8-aa07-4c0d0409312f' \
    APIKEYSCONFIGURATION__IOS='b8b9cc0a-90f6-4aa3-96b6-c1d9bc7b15dd' \
    APIKEYSCONFIGURATION__WEB='09ed7f5c-00bb-4c2d-9051-1c12de62abf9' \
    APIKEYSCONFIGURATION__PARSER='0161565a-bc3d-4595-b694-d9a200f28d63' \
    APIKEYSCONFIGURATION__DEVIOS='c90dea5c-c284-400e-b364-b3b0e080c3a8' \
    APIKEYSCONFIGURATION__DEVANDROID='67cce045-b19a-47a6-b367-18b1f7bfe910' \
    DATABASECONFIGURATION__ESPRESSODATABASECONNECTIONSTRING=$(EspressoDatabaseConnectionString) \
    DATABASECONFIGURATION__ESPRESSOIDENTITYDATABASECONNECTIONSTRING=$(EspressoIdentityDatabaseConnectionString) \
    APPCONFIGURATION__SLACKWEBHOOK=$(SlackWebHook) \
    RABBITMQCONFIGURATION__USERNAME="ipazanin" \
    RABBITMQCONFIGURATION__PASSWORD="Opatija1" \
    RABBITMQCONFIGURATION__HOSTNAME="localhost" \
	dotnet run --project $(WebApiProjectPath) --urls $(WebApiUrls) --configuration $(ReleaseConfiguration)
else ifeq ($(arg), watch)
	ASPNETCORE_ENVIRONMENT=local \
    DATABASE_NAME=local \
    APIKEYSCONFIGURATION__ANDROID='cfb490b1-b392-49a8-aa07-4c0d0409312f' \
    APIKEYSCONFIGURATION__IOS='b8b9cc0a-90f6-4aa3-96b6-c1d9bc7b15dd' \
    APIKEYSCONFIGURATION__WEB='09ed7f5c-00bb-4c2d-9051-1c12de62abf9' \
    APIKEYSCONFIGURATION__PARSER='0161565a-bc3d-4595-b694-d9a200f28d63' \
    APIKEYSCONFIGURATION__DEVIOS='c90dea5c-c284-400e-b364-b3b0e080c3a8' \
    APIKEYSCONFIGURATION__DEVANDROID='67cce045-b19a-47a6-b367-18b1f7bfe910' \
    DATABASECONFIGURATION__ESPRESSODATABASECONNECTIONSTRING=$(EspressoDatabaseConnectionString) \
    DATABASECONFIGURATION__ESPRESSOIDENTITYDATABASECONNECTIONSTRING=$(EspressoIdentityDatabaseConnectionString) \
    APPCONFIGURATION__SLACKWEBHOOK=$(SlackWebHook) \
    RABBITMQCONFIGURATION__USERNAME="ipazanin" \
    RABBITMQCONFIGURATION__PASSWORD="Opatija1" \
    RABBITMQCONFIGURATION__HOSTNAME="localhost" \
	dotnet watch --project $(WebApiProjectPath) run --urls $(WebApiUrls) --configuration $(DebugConfiguration)
else
	echo "Invalid Argument. Accepted arguments: {empty}, watch"
endif

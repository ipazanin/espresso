SolutionPath="server/Espresso/Espresso.sln"
PersistenceProjectPath="source/Espresso.Persistence/Espresso.Persistence.csproj"
DefaultVerbosity="minimal" # verbosity levels: quiet, minimal, normal, detailed, diagnostic
LocalEnvironmentName="local"
Configuration="Release" # Configurations: Release, Debug

UnitTestResultsPath="server/UnitTests/TestReports"
UnitTestRelativeResultsPath="TestReports"
UnitTestsDirectory="server/UnitTests"
UnitTestsLogFileName="TestResults.xml"

list::
	@$(MAKE) -pRrq -f $(lastword $(MAKEFILE_LIST)) : 2>/dev/null | \
	awk -v RS= -F: '/^# File/,/^# Finished Make data base/ \
	{if ($$1 !~ "^[#.]") {print $$1}}' | sort | egrep -v -e '^[^[:alnum:]]' -e '^$@$$'

clean::
	dotnet clean --configuration Release --verbosity minimal $(SolutionPath) && \
	find . -iname "bin" -o -iname "obj" | xargs rm -rf

build::
ifeq ($(strip $(verbosity)),)
	dotnet build \
	--configuration $(Configuration) \
	--verbosity $(DefaultVerbosity) \
	$(SolutionPath)
else
	dotnet build \
	--configuration $(Configuration) \
	--verbosity $(verbosity) \
	$(SolutionPath)
endif

database-update::
	ASPNETCORE_ENVIRONMENT=$(LocalEnvironmentName) \
	dotnet ef database update -p $(PersistenceProjectPath) -v

migration-add::
	ASPNETCORE_ENVIRONMENT=$(LocalEnvironmentName) \
	dotnet ef migrations add -p $(PersistenceProjectPath) -v $(name)

migration-remove::
	ASPNETCORE_ENVIRONMENT=$(LocalEnvironmentName) \
	dotnet ef migrations remove -p $(PersistenceProjectPath) -v

update::
	./scripts/update.sh

restore::
	dotnet restore $(SolutionPath)

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

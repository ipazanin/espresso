# Common
list::
	@$(MAKE) -pRrq -f $(lastword $(MAKEFILE_LIST)) : 2>/dev/null | \
	awk -v RS= -F: '/^# File/,/^# Finished Make data base/ \
	{if ($$1 !~ "^[#.]") {print $$1}}' | sort | egrep -v -e '^[^[:alnum:]]' -e '^$@$$'

# Backend
clean::
	dotnet clean --configuration Release --verbosity minimal source/Espresso.sln && \
	find . -iname "bin" -o -iname "obj" | xargs rm -rf

build::
	dotnet build --configuration Release source/Espresso.sln

database-update::
	ASPNETCORE_ENVIRONMENT=local-production-db dotnet ef database update -p \
	./source/Espresso.Persistence/Espresso.Persistence.csproj -v

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
	make -f scripts/make/backend/Makefile test-coverage
	make -f scripts/make/backend/Makefile create-coverage-report
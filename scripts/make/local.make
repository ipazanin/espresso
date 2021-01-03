WebApiProjectPath="source/server/Espresso/Espresso.WebApi/Espresso.WebApi.csproj"
ParserProjectPath="source/server/Espresso/Espresso.ParserDeleter/Espresso.ParserDeleter.csproj"
WebApiUrls="http://localhost:8000"
ParserUrls="http://localhost:9000"
ReleaseConfiguration="Release"
DebugConfiguration="Debug"

list::
	@$(MAKE) -pRrq -f $(lastword $(MAKEFILE_LIST)) : 2>/dev/null | \
	awk -v RS= -F: '/^# File/,/^# Finished Make data base/ \
	{if ($$1 !~ "^[#.]") {print $$1}}' | sort | egrep -v -e '^[^[:alnum:]]' -e '^$@$$'

start-parser::
	make compose-database arg1=up arg2="-d"
ifeq ($(strip $(arg)),)
	ASPNETCORE_ENVIRONMENT=local \
    DATABASE_NAME=local \
    APIKEYSCONFIGURATION__PARSER='0161565a-bc3d-4595-b694-d9a200f28d63' \
    DATABASECONFIGURATION__DEFAULTCONNECTIONSTRING='Server=localhost,1433;Database=EspressoDb;Application Name=Espresso;User=sa;Password=Opatija1;' \
    APPCONFIGURATION__SLACKWEBHOOK='https://hooks.slack.com/services/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' \
    APPCONFIGURATION__SERVERURL='http://localhost:8000' \
    RABBITMQCONFIGURATION__USERNAME='ipazanin' \
    RABBITMQCONFIGURATION__PASSWORD='Opatija1' \
    RABBITMQCONFIGURATION__HOSTNAME='localhost' \
	dotnet run --project $(ParserProjectPath) --urls $(ParserUrls) --configuration $(ReleaseConfiguration)
else ifeq ($(arg), watch)
    ASPNETCORE_ENVIRONMENT=local \
    DATABASE_NAME=local \
    APIKEYSCONFIGURATION__PARSER='0161565a-bc3d-4595-b694-d9a200f28d63' \
    DATABASECONFIGURATION__DEFAULTCONNECTIONSTRING='Server=localhost,1433;Database=EspressoDb;Application Name=Espresso;User=sa;Password=Opatija1;' \
    APPCONFIGURATION__SLACKWEBHOOK='https://hooks.slack.com/services/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' \
    APPCONFIGURATION__SERVERURL='http://localhost:8000' \
    RABBITMQCONFIGURATION__USERNAME="ipazanin" \
    RABBITMQCONFIGURATION__PASSWORD="Opatija1" \
    RABBITMQCONFIGURATION__HOSTNAME="localhost" \
	dotnet watch --project $(ParserProjectPath) run --urls $(ParserUrls)  --configuration $(DebugConfiguration)
else
	echo "Invalid Argument. Accepted arguments: {empty}, watch"
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
    DATABASECONFIGURATION__DEFAULTCONNECTIONSTRING='Server=localhost,1433;Database=EspressoDb;Application Name=Espresso;User=sa;Password=Opatija1;' \
    APPCONFIGURATION__SLACKWEBHOOK='https://hooks.slack.com/services/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' \
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
    DATABASECONFIGURATION__DEFAULTCONNECTIONSTRING='Server=localhost,1433;Database=EspressoDb;Application Name=Espresso;User=sa;Password=Opatija1;' \
    APPCONFIGURATION__SLACKWEBHOOK='https://hooks.slack.com/services/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' \
    RABBITMQCONFIGURATION__USERNAME="ipazanin" \
    RABBITMQCONFIGURATION__PASSWORD="Opatija1" \
    RABBITMQCONFIGURATION__HOSTNAME="localhost" \
	dotnet watch --project $(WebApiProjectPath) run --urls $(WebApiUrls) --configuration $(DebugConfiguration)
else
	echo "Invalid Argument. Accepted arguments: {empty}, watch"
endif

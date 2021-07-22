DefaultDockerImageTag=latest

EspressoWebApiDockerfilePath="source/server/Espresso/Espresso.WebApi/Dockerfile"
EspressoParserDockerfilePath="source/server/Espresso/Espresso.Dashboard/Dockerfile"

DockerBuildContextPath="source"

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

compose-parser::
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
	--build-arg REACT_APP_ENVIRONMENT=$(DefaultReactEnvironment) \
	$(DockerBuildContextPath)
	docker push $(WebApiDockerImage):$(DefaultDockerImageTag)
	docker build \
	-f $(EspressoWebApiDockerfilePath) \
	-t $(WebApiDockerImageGoogleContainerRegistry):$(DefaultDockerImageTag) \
	--build-arg REACT_APP_ENVIRONMENT=$(DefaultReactEnvironment) \
	$(DockerBuildContextPath)
	docker push $(WebApiDockerImageGoogleContainerRegistry):$(DefaultDockerImageTag)	
else
	docker build \
	-f $(EspressoWebApiDockerfilePath) \
	-t $(WebApiDockerImage):$(v) \
	--build-arg REACT_APP_ENVIRONMENT=$(DefaultReactEnvironment) \
	$(DockerBuildContextPath)
	docker push $(WebApiDockerImage):$(v)
	docker build \
	-f $(EspressoWebApiDockerfilePath) \
	-t $(WebApiDockerImageGoogleContainerRegistry):$(v) \
	--build-arg REACT_APP_ENVIRONMENT=$(DefaultReactEnvironment) \
	$(DockerBuildContextPath)
	docker push $(WebApiDockerImageGoogleContainerRegistry):$(v)	
endif

docker-build-dashboard::
ifeq ($(strip $(v)),)
	docker build \
	-f $(EspressoParserDockerfilePath) \
	-t $(DashboardDockerImage):$(DefaultDockerImageTag) \
	$(DockerBuildContextPath)
	docker push $(DashboardDockerImage):$(DefaultDockerImageTag)
	docker build \
	-f $(EspressoParserDockerfilePath) \
	-t $(DashboardDockerImageGoogleContainerRegistry):$(DefaultDockerImageTag) \
	$(DockerBuildContextPath)
	docker push $(DashboardDockerImageGoogleContainerRegistry):$(DefaultDockerImageTag)	
else
	docker build \
	-f $(EspressoParserDockerfilePath) \
	-t $(DashboardDockerImage):$(v) \
	$(DockerBuildContextPath)
	docker push $(DashboardDockerImage):$(v)
	docker build \
	-f $(EspressoParserDockerfilePath) \
	-t $(DashboardDockerImageGoogleContainerRegistry):$(v) \
	$(DockerBuildContextPath)
	docker push $(DashboardDockerImageGoogleContainerRegistry):$(v)		
endif

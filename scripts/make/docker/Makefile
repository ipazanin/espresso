WebApiDockerImage="docker.pkg.github.com/espresso-news/espresso-backend/espresso-webapi"
ParserDockerImage="docker.pkg.github.com/espresso-news/espresso-backend/espresso-parserdeleter"
DefaultReactEnvironment="production"

compose-database::
ifeq ($(arg1), up)
	docker-compose \
	-f ./scripts/compose/database.yml \
	-f ./scripts/compose/database-environment.yml  \
	up --build $(arg2)
else ifeq ($(strip $(arg1)),)
	docker-compose \
	-f ./scripts/compose/database.yml \
	-f ./scripts/compose/database-environment.yml \
	up --build $(arg2)
else ifeq ($(arg1), down)
	docker-compose -f ./scripts/compose/database.yml -f ./scripts/compose/database-environment.yml down
else
	echo "Invalid Argument. Accepted arguments: up, down"
endif

compose-rabbitmq::
ifeq ($(arg), up)
	docker-compose \
	-f ./scripts/compose/rabbitmq.yml \
	-f ./scripts/compose/rabbitmq-environment.yml \
	up --build
else ifeq ($(strip $(arg)),)
	docker-compose \
	-f ./scripts/compose/rabbitmq.yml \
	-f ./scripts/compose/rabbitmq-environment.yml \
	up --build
else ifeq ($(arg), down)
	docker-compose \
	-f ./scripts/compose/rabbitmq.yml \
	-f ./scripts/compose/rabbitmq-environment.yml \
	down
else
	echo "Invalid Argument. Accepted arguments: up, down"
endif

compose-parser::
ifeq ($(arg), up)
	docker-compose \
	-f ./scripts/compose/database.yml \
	-f ./scripts/compose/database-environment.yml \	
	-f ./scripts/compose/parser.yml \
	-f ./scripts/compose/parser-environment.yml \
	up --build
else ifeq ($(strip $(arg)),)
	docker-compose \
	-f ./scripts/compose/database.yml \
	-f ./scripts/compose/database-environment.yml \	
	-f ./scripts/compose/parser.yml \
	-f ./scripts/compose/parser-environment.yml \
	up --build
else ifeq ($(arg), down)
	docker-compose \
	-f ./scripts/compose/database.yml \
	-f ./scripts/compose/database-environment.yml \	
	-f ./scripts/compose/parser.yml \
	-f ./scripts/compose/parser-environment.yml \
	down
else
	echo "Invalid Argument. Accepted arguments: up, down"
endif

compose-webapi::
ifeq ($(arg), up)
	docker-compose \
	-f ./scripts/compose/database.yml \
	-f ./scripts/compose/database-environment.yml \
	-f ./scripts/compose/webapi.yml \
	-f ./scripts/compose/webapi-environment.yml \
	up --build
else ifeq ($(strip $(arg)),)
	docker-compose \
	-f ./scripts/compose/database.yml \
	-f ./scripts/compose/database-environment.yml \
	-f ./scripts/compose/webapi.yml \
	-f ./scripts/compose/webapi-environment.yml \
	up --build
else ifeq ($(arg), down)
	docker-compose \
	-f ./scripts/compose/database.yml \
	-f ./scripts/compose/database-environment.yml \
	-f ./scripts/compose/webapi.yml \
	-f ./scripts/compose/webapi-environment.yml \
	down
else
	echo "Invalid Argument. Accepted arguments: up, down"
endif

compose-local::
ifeq ($(arg), up)
	docker-compose \
	-f ./scripts/compose/database.yml \
	-f ./scripts/compose/database-environment.yml \
	-f ./scripts/compose/webapi.yml \
	-f ./scripts/compose/webapi-environment.yml \
	-f ./scripts/compose/parser.yml \
	-f ./scripts/compose/parser-environment.yml \
	up --build
else ifeq ($(strip $(arg)),)
	docker-compose \
	-f ./scripts/compose/database.yml \
	-f ./scripts/compose/database-environment.yml \
	-f ./scripts/compose/webapi.yml \
	-f ./scripts/compose/webapi-environment.yml \
	-f ./scripts/compose/parser.yml \
	-f ./scripts/compose/parser-environment.yml \
	up --build
else ifeq ($(arg), down)
	docker-compose \
	-f ./scripts/compose/database.yml \
	-f ./scripts/compose/database-environment.yml \
	-f ./scripts/compose/webapi.yml \
	-f ./scripts/compose/webapi-environment.yml \
	-f ./scripts/compose/parser.yml \
	-f ./scripts/compose/parser-environment.yml \
	down
else
	echo "Invalid Argument. Accepted arguments: up, down"
endif

compose-local-rabbitmq::
ifeq ($(arg), up)
	docker-compose \
	-f ./scripts/compose/database.yml \
	-f ./scripts/compose/database-environment.yml \
	-f ./scripts/compose/webapi.yml \
	-f ./scripts/compose/webapi-environment.yml \
	-f ./scripts/compose/parser.yml \
	-f ./scripts/compose/parser-environment.yml \
	-f ./scripts/compose/rabbitmq.yml \
	-f ./scripts/compose/rabbitmq-environment.yml \
	up --build
else ifeq ($(strip $(arg)),)
	docker-compose \
	-f ./scripts/compose/database.yml \
	-f ./scripts/compose/database-environment.yml \
	-f ./scripts/compose/webapi.yml \
	-f ./scripts/compose/webapi-environment.yml \
	-f ./scripts/compose/parser.yml \
	-f ./scripts/compose/parser-environment.yml \
	-f ./scripts/compose/rabbitmq.yml \
	-f ./scripts/compose/rabbitmq-environment.yml \
	up --build
else ifeq ($(arg), down)
	docker-compose \
	-f ./scripts/compose/database.yml \
	-f ./scripts/compose/database-environment.yml \
	-f ./scripts/compose/webapi.yml \
	-f ./scripts/compose/webapi-environment.yml \
	-f ./scripts/compose/parser.yml \
	-f ./scripts/compose/parser-environment.yml \
	-f ./scripts/compose/rabbitmq.yml \
	-f ./scripts/compose/rabbitmq-environment.yml \
	down
else
	echo "Invalid Argument. Accepted arguments: up, down"
endif

docker-build-webapi::
ifeq ($(strip $(v)),)
	docker build \
	--force-rm \
	-f ./source/Espresso.WebApi/Dockerfile \
	-t $(WebApiDockerImage):latest \
	--build-arg REACT_APP_ENVIRONMENT=$(DefaultReactEnvironment) \
	./source
	docker push $(WebApiDockerImage):latest
else
	docker build \
	--force-rm \
	-f ./source/Espresso.WebApi/Dockerfile \
	-t $(WebApiDockerImage):$(v) \
	--build-arg REACT_APP_ENVIRONMENT=$(DefaultReactEnvironment) \
	./source
	docker push $(WebApiDockerImage):$(v)
endif

docker-build-parserdeleter::
ifeq ($(strip $(v)),)
	docker build \
	--force-rm \
	-f ./source/Espresso.ParserDeleter/Dockerfile \
	-t $(ParserDockerImage):latest \
	./source
	docker push $(ParserDockerImage):latest
else
	docker build \
	--force-rm \
	-f ./source/Espresso.ParserDeleter/Dockerfile \
	-t $(ParserDockerImage):$(v) \
	./source
	docker push $(ParserDockerImage):$(v)
endif
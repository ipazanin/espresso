# Common
list::
	@$(MAKE) -pRrq -f $(lastword $(MAKEFILE_LIST)) : 2>/dev/null | \
	awk -v RS= -F: '/^# File/,/^# Finished Make data base/ \
	{if ($$1 !~ "^[#.]") {print $$1}}' | sort | egrep -v -e '^[^[:alnum:]]' -e '^$@$$'

# Backend
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
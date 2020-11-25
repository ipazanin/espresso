# common
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

# docker
compose-database::
	make -f scripts/make/docker/Makefile compose-database arg1=$(arg1) arg2=$(arg2)

compose-rabbitmq::
	make -f scripts/make/docker/Makefile compose-rabbitmq arg=$(arg)

compose-parser::
	make -f scripts/make/docker/Makefile compose-parser arg=$(arg)

compose-webapi::
	make -f scripts/make/docker/Makefile compose-webapi arg=$(arg)

compose-local::
	make -f scripts/make/docker/Makefile compose-local arg=$(arg)

compose-local-rabbitmq::
	make -f scripts/make/docker/Makefile compose-local-rabbitmq arg=$(arg)

docker-build-webapi::
	make -f scripts/make/docker/Makefile docker-build-webapi  v=$(v)

docker-build-parserdeleter::
	make -f scripts/make/docker/Makefile docker-build-parserdeleter v=$(v)

docker-build::
	make docker-build-webapi v=$(v)
	make docker-build-parserdeleter v=$(v)

# backend
health-check-backend::
	make restore
	make build
	make test

rebuild-backend::
	make clean
	make build

clean::
	dotnet clean --configuration Release --verbosity minimal source/Espresso.sln && \
	find . -iname "bin" -o -iname "obj" | xargs rm -rf

build::
	make -f scripts/make/backend/Makefile build
	
database-update::
	make -f scripts/make/backend/Makefile database-update

migration-add::
	make -f scripts/make/backend/Makefile migration-add name=$(name)

migration-remove::
	make -f scripts/make/backend/Makefile migration-remove

update::
	make -f scripts/make/backend/Makefile update

restore::
	make -f scripts/make/backend/Makefile restore

test::
	make -f scripts/make/backend/Makefile test verbosity=$(verbosity)

test-coverage::
	make -f scripts/make/backend/Makefile test-coverage
	
create-coverage-report::
	make -f scripts/make/backend/Makefile create-coverage-report

coverage:
	make -f scripts/make/backend/Makefile coverage

# frontend
health-check-frontend::
	make -f scripts/make/frontend/Makefile health-check-frontend

rebuild-frontend::
	make -f scripts/make/frontend/Makefile rebuild-frontend

install::
	make -f scripts/make/frontend/Makefile install

build-frontend::
	make -f scripts/make/frontend/Makefile build-frontend

lint::
	make -f scripts/make/frontend/Makefile lint

test-frontend::
	make -f scripts/make/frontend/Makefile test-frontend

start-parser::
	make -f scripts/make/local/Makefile start-parser

start-webapi::
	make -f scripts/make/local/Makefile start-webapi

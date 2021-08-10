# common
list::
	@$(MAKE) -pRrq -f $(lastword $(MAKEFILE_LIST)) : 2>/dev/null | \
	awk -v RS= -F: '/^# File/,/^# Finished Make data base/ \
	{if ($$1 !~ "^[#.]") {print $$1}}' | sort | egrep -v -e '^[^[:alnum:]]' -e '^$@$$'

health-check::
	make health-check-backend

rebuild::
	make rebuild-backend
	make rebuild-frontend

# docker
compose-database::
	make -f scripts/make/docker.make compose-database arg1=$(arg1) arg2=$(arg2)

compose-rabbitmq::
	make -f scripts/make/docker.make compose-rabbitmq arg=$(arg)

compose-dashboard::
	make -f scripts/make/docker.make compose-dashboard arg=$(arg)

compose-webapi::
	make -f scripts/make/docker.make compose-webapi arg=$(arg)

compose-local::
	make -f scripts/make/docker.make compose-local arg=$(arg)

compose-local-rabbitmq::
	make -f scripts/make/docker.make compose-local-rabbitmq arg=$(arg)

docker-build-webapi::
	make -f scripts/make/docker.make docker-build-webapi  v=$(v)

docker-build-dashboard::
	make -f scripts/make/docker.make docker-build-dashboard v=$(v)

docker-build::
	make docker-build-webapi v=$(v)
	make docker-build-dashboard v=$(v)

# backend
health-check-backend::
	make restore
	make build
	make test

rebuild-backend::
	make clean
	make build

clean::
	make -f scripts/make/backend.make clean

build::
	make -f scripts/make/backend.make build

publish::
	make -f scripts/make/backend.make publish	
	
database-update-espresso-database::
	make -f scripts/make/backend.make database-update-espresso-database

migration-add-espresso-database::
	make -f scripts/make/backend.make migration-add-espresso-database name=$(name)

migration-remove-espresso-database::
	make -f scripts/make/backend.make migration-remove-espresso-database

database-update-identity-database::
	make -f scripts/make/backend.make database-update-identity-database

migration-add-identity-database::
	make -f scripts/make/backend.make migration-add-identity-database name=$(name)

migration-remove-identity-database::
	make -f scripts/make/backend.make migration-remove-identity-database

update::
	make -f scripts/make/backend.make update

restore::
	make -f scripts/make/backend.make restore

test::
	make -f scripts/make/backend.make test verbosity=$(verbosity)

test-coverage::
	make -f scripts/make/backend.make test-coverage verbosity=$(verbosity)
	
create-coverage-report::
	make -f scripts/make/backend.make create-coverage-report

coverage:
	make test-coverage verbosity=quiet
	make create-coverage-report verbosity=quiet

start-dashboard::
	make -f scripts/make/backend.make start-dashboard arg=$(arg) 

start-webapi::
	make -f scripts/make/backend.make start-webapi arg=$(arg)

infer-csharp::
	# make -f scripts/make/backend.make build
	# make -f scripts/make/backend.make publish
	make -f scripts/make/backend.make infer-csharp

# frontend
health-check-frontend::
	make -f scripts/make/frontend.make install
	make -f scripts/make/frontend.make build-frontend
	make -f scripts/make/frontend.make lint
	make -f scripts/make/frontend.make test-frontend

rebuild-frontend::
	make -f scripts/make/frontend.make rebuild-frontend

install::
	make -f scripts/make/frontend.make install

build-frontend::
	make -f scripts/make/frontend.make build-frontend

lint::
	make -f scripts/make/frontend.make lint

test-frontend::
	make -f scripts/make/frontend.make test-frontend

CLIENTAPP_DIRECTORY="source/Espresso.ClientApp"

list::
	@$(MAKE) -pRrq -f $(lastword $(MAKEFILE_LIST)) : 2>/dev/null | \
	awk -v RS= -F: '/^# File/,/^# Finished Make data base/ \
	{if ($$1 !~ "^[#.]") {print $$1}}' | sort | egrep -v -e '^[^[:alnum:]]' -e '^$@$$'

health-check-frontend::
	make -f scripts/make/frontend/Makefile install
	make -f scripts/make/frontend/Makefile build-frontend
	make -f scripts/make/frontend/Makefile lint
	make -f scripts/make/frontend/Makefile test-frontend

rebuild-frontend::
	cd $(CLIENTAPP_DIRECTORY); \
	rm -rf node_modules \
	rm -rf build
	make -f scripts/make/frontend/Makefile install
	make -f scripts/make/frontend/Makefile build-frontend

install::
	cd $(CLIENTAPP_DIRECTORY); \
	npm install

build-frontend::
	cd $(CLIENTAPP_DIRECTORY); \
	REACT_APP_ENVIRONMENT=production \
	./node_modules/.bin/react-scripts build

lint::
	cd $(CLIENTAPP_DIRECTORY); \
	./node_modules/.bin/eslint --ext .ts,.tsx src/ \
	--fix --cache --cache-location=./node_modules/.cache/

test-frontend::
	cd $(CLIENTAPP_DIRECTORY); \
	CI=true REACT_APP_ENVIRONMENT=test \
	./node_modules/.bin/react-scripts test

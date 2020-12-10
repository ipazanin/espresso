ClientAppDirectory="client"
TestReactAppEnvironment="test"

rebuild-frontend::
	cd $(ClientAppDirectory); \
	rm -rf node_modules \
	rm -rf build
	make -f scripts/make/frontend/Makefile install
	make -f scripts/make/frontend/Makefile build-frontend

install::
	cd $(ClientAppDirectory); \
	npm install

build-frontend::
	cd $(ClientAppDirectory); \
	REACT_APP_ENVIRONMENT=production \
	./node_modules/.bin/react-scripts build

lint::
	cd $(ClientAppDirectory); \
	./node_modules/.bin/eslint --ext .ts,.tsx src/ \
	--fix --cache --cache-location=./node_modules/.cache/

test-frontend::
	cd $(ClientAppDirectory); \
	CI=true REACT_APP_ENVIRONMENT=test \
	./node_modules/.bin/react-scripts test

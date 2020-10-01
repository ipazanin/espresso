#!/bin/bash
docker build --force-rm -f ./source/Espresso.WebApi/Dockerfile -t ipazanin/espresso-webapi:$1 --build-arg REACT_APP_ENVIRONMENT=production ./source && docker push ipazanin/espresso-webapi:$1

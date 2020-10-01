#!/bin/bash
docker build --force-rm -f ./source/Espresso.ParserDeleter/Dockerfile -t ipazanin/espresso-parserdeleter:$1 ./source && docker push ipazanin/espresso-parserdeleter:$1
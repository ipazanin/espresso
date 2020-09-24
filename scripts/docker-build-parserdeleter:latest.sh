#!/bin/bash
docker build --force-rm -f ./source/Espresso.ParserDeleter/Dockerfile -t ipazanin/espresso-parserdeleter:latest ./source && docker push ipazanin/espresso-parserdeleter:latest
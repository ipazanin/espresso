#!/bin/bash
./scripts/docker-build-webapi.sh $1
./scripts/docker-build-parserdeleter.sh $1
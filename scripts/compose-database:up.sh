#!/bin/bash
docker-compose -f ./compose/docker-compose-database.yml up --build --remove-orphans $1
#!/bin/bash
if [ $1 == "up" ]
then
    docker-compose -f ./compose/docker-compose-local.yml up --build --remove-orphans
elif [ $1 == "down" ]
then
    docker-compose -f ./compose/docker-compose-local.yml down
else
    echo "Invalid Argument. Accepted arguments: up, down"
fi
#!/bin/bash
if [ $1 == "up" ]
then
    docker-compose -f ./compose/docker-compose-database.yml up --build --remove-orphans $2
elif [ $1 == "down" ]
then
    docker-compose -f ./compose/docker-compose-database.yml down
else
    echo "Invalid Argument. Accepted arguments: up, down"
fi
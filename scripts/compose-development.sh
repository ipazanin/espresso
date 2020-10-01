#!/bin/bash
if [ $1 == "up" ]
then
    sudo docker-compose -f ./compose/docker-compose-development.yml up --build --remove-orphans 
elif [ $1 == "down" ]
then
    sudo docker-compose -f ./compose/docker-compose-development.yml down
else
    echo "Invalid Argument. Accepted arguments: up, down"
fi
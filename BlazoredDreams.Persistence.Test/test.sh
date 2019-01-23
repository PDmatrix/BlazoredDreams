#!/bin/bash 
name='blazoredDreamsTest'
if [ ! "$(docker ps -q -f name=$name)" ]; then
    if [ "$(docker ps -aq -f status=exited -f name=$name)" ]; then
        # cleanup
        docker rm $name
    fi
    # run your container
    docker run --name $name -d -e POSTGRES_PASSWORD=password -e POSTGRES_USER=test -p 5432:5432 postgres:alpine
    sleep 5
fi
dotnet test

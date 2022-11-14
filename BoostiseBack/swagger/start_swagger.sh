#!/bin/bash

docker run --rm -p 7001:8080 -v $PWD:/files -e SWAGGER_JSON=/files/boostise_back_api.yml  --name boostise-swagger-ui swaggerapi/swagger-ui
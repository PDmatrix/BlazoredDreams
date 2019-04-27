#!/usr/bin/env bash

export TS_POST_PROCESS_FILE="$(pwd)/../../node_modules/.bin/prettier --write"
curl -O "http://localhost:5000/swagger/v1/swagger.json"
openapi-generator generate -i swagger.json -g typescript-axios --enable-post-process-file
sed -i "/<any>/d" ./api.ts
sed -i -e "s/json-patch+json/json/g" ./api.ts

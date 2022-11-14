#/bin/bash
rm -rf ./newman/*
newman run "API Requests.postman_collection.json" -e "Dev Environment.postman_environment.json" --reporters html,cli,json --reporter-json-export newman_report.json


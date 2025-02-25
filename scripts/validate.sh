#!/bin/bash

curl -v http://localhost:8080/
curl -v http://localhost:8080/analyze -H "Content-Type: multipart/form-data" -F "file=@../images/coin.png" | jq
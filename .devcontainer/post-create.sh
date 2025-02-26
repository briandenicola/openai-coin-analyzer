#!/bin/bash

# this runs at Codespace creation - not part of pre-build

echo "$(date)    post-create start" >> ~/status

#Install jq
sudo apt update
sudo apt install -y jq

#Install envsubst
curl -Lso envsubst https://github.com/a8m/envsubst/releases/download/v1.2.0/envsubst-Linux-x86_64
sudo install envsubst /usr/local/bin
rm -rf ./envsubst

#Install Task
sudo sh -c "$(curl -sL https://taskfile.dev/install.sh)" -- -d -b /usr/local/bin

#Install Playwright
sudo npx playwright install-deps

#Install az extensions
sudo az aks install-cli -y
sudo az extension add --name application-insights -y
sudo az extension add --name aks-preview -y

#Install Azure Static WebApp cli
sudo npm install -g @azure/static-web-apps-cli

echo "$(date)    post-create complete" >> ~/status
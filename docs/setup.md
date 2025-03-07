# Setup and Deployment

## Codespaces
> You can use the following link to launch a Codespaces configured for this project:

[![Open in GitHub Codespaces](https://img.shields.io/static/v1?style=for-the-badge&label=GitHub+Codespaces&message=Open&color=brightgreen&logo=github)](https://codespaces.new/briandenicola/openai-coin-analyzer?quickstart=1)
[![Open in Dev Containers](https://img.shields.io/static/v1?style=for-the-badge&label=Dev%20Containers&message=Open&color=blue&logo=visualstudiocode)](https://vscode.dev/redirect?url=vscode://ms-vscode-remote.remote-containers/cloneInVolume?url=https://github.com/briandenicola/openai-coin-analyzer)  

## Prerequisites
* A Posix compliant System. It could be one of the following:
    * [Github CodeSpaces](https://github.com/features/codespaces)
    * Azure Linux VM - Standard_B1s VM will work ($18/month)
    * Windows 11 with [Windows Subsystem for Linux](https://docs.microsoft.com/en-us/windows/wsl/install)
* [dotnet 9](https://dotnet.microsoft.com/download) - The .NET Platform
* [Visual Studio Code](https://code.visualstudio).com/) or equivalent - A lightweight code editor
* [Docker](https://www.docker.com/products/docker-desktop) - The Docker Desktop to build/push containers
* [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli) - A tool for managing Azure resources
* [git](https://git-scm.com/) - The source control tool
* [Taskfile](https://taskfile.dev/#/) - A task runner for the shell
* [Terraform](https://www.terraform.io/) - A tool for building Azure infrastructure and infrastructure as code

## DNS Records
* The following DNS records are required for the application to work correctly.  These are used for the application to be accessed externally.  
* You must own a domain where you have the ability create DNS records.
* The following records are required __replace bjd.demo with your domain name__: 
    Name | Usage | DNS Record Type | IP Address
    ------ | ---- | ---- | ----
    ric.bjd.demo | APIM Gateway | A | APIM Gateway IP Address in West US
    
<p align="right">(<a href="#setup-and-deployment">Back to Top</a>)</p>

## Required Certificates
* The Azure Service Mesh has an External Gateway and requires a TLS Certificate. 
* [This guide](./docs/letsencrypt.md) will walk you through Let's Encrypt with Azure DNS

<p align="right">(<a href="#setup-and-deployment">Back to Top</a>)</p>

## Task
* The deployment of this application has been automated using [Taskfile](https://taskfile.dev/#/).  This was done instead of using a CI/CD pipeline to make it easier to understand the deployment process.  
* Of course, the application can be deployed manually
* The Taskfile is a simple way to run commands and scripts in a consistent manner.  
* The [Taskfile](../Taskfile.yaml) definition is located in the root of the repository
* The Task file declares the default values that can be updated to suit specific requirements: 

### Taskfile Variables

Name | Usage | Location | Required | Default or Example Value
------ | ------ | ------ | ------ | ------
TITLE | Value used in Azure Tags | taskfile.yaml | Yes | Roman Imperial Coin Analyzer
DEFAULT_REGION | Default region to deploy to | taskfile.yaml | Yes | canadacentral
DEFAULT_DOMAIN | Default domain used for all URLs & certs | taskfile.yaml | Yes | bjd.demo
COUNT | Number of replicas for the application | taskfile.yaml | No | 2
HELM_CHART | Helm Chart to use for deployment | taskfile.yaml | No | ric-analyzer
SKU | SKU for the nodes in AKS | taskfile.yaml | No | Standard_D4ads_v5

### Task Commands
* Running the `task` command without any options will run the default command. This will list all the available tasks.
    * build:                Builds application via ACR tasks
    * certs:                Deploys Istio Certificate and Otel infrastructure via Helm/GitOps
    * creds:                Gets credential file for newly created AKS cluster
    * deploy:               Deploys application via Helm/GitOps
    * dns:                  Gets the IP Address of the Istio Gateway
    * down:                 Destroys all Azure resources and cleans up Terraform
    * hubble:               Install Hubble Dashboard for Network Observability
    * init:                 Initializes Terraform for Roman Imperial Coin Analyzer
    * restart:              Performs a rollout restart on all deployments in eshop namespace
    * status:               Gets the overall status of the cluster
    * ui:                   Builds and Deploys the UI code to Azure Static Web Apps
    * up:                   Creates complete Azure environment for Roman Imperial Coin Analyzer
    * local:apply:          Applies Terraform configuration for Roman Imperial Coin Analyzer (local)
    * local:down:           Destroys all resources and cleans up Terraform for Roman Imperial Coin Analyzer (local)
    * local:run:            Starts Aspire Dashboard and compiles/runs API (local)
    * local:up:             Creates a simple Azure environment for Roman Imperial Coin Analyzer (local)
    * local:validate:       Runs the script ./scripts/validate.sh to test running API  (local)
<p align="right">(<a href="#setup-and-deployment">Back to Top</a>)</p>

# Navigation
[⏪ Previous Section](../README.md) ‖ [Return to Main Index 🏠](../README.md) ‖ [Next Section ⏩](../docs/letsencrypt.md) 
<p align="right">(<a href="#setup-and-deployment">Back to Top</a>)</p>
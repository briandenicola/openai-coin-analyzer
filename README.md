# Overview
TBD

![UI](./images/example.png)

## Components
Component | Usage
------ | ------
Azure Kubernetes Service | Container Orchestration Runtime Platform  
Azure Blob Storage | Podcast Storage 
Azure OpenAI Service Services | Service that analyze the coin
Azure Static Web Apps | Hosting platform for React UI
Azure Key Vault | Secret store 
Azure APIM Managment | API Gateway 

# Architecture
![UI](./images/architecture.png)

# Setup
* The environment can setup following the steps in the [Setup and Deployment](./docs/setup.md) document.  
* The setup process will create all the required resources and deploy the application code to Azure.

# Roadmap
- [x] DevContainers
- [x] Helm Chart Updates
- [x] APIM Configuration and API Policies 
- [x] Simple React Front End 
- [x] ACR Container Build Task
- [x] Deployment to SWA
- [x] Deployment to AKS
- [x] End to End Testing
- [x] Architecture Diagram
- [ ] Updated Documentation
- [ ] Update API to save image to blob storage and return URL in response
- [ ] Update Front End to display image

<p align="right">(<a href="#Introduction">Back to Top</a>)</p>
[Return to Main Index 🏠](../README.md)  ‖ [Next Section ⏩](./docs/setup.md)
# PulseAsset

## Overview
PulseAsset is an IT Asset Management web application designed to streamline asset tracking in business environments. This application addresses the challenges of disjointed and manual IT asset management processes by offering functionalities such as asset tracking, category and location management, user authentication, and report generation.

## Key Features
- Asset management with options to add, edit, and delete assets.
- Management of asset categories and locations.
- User authentication system for secure access.
- Generation of asset reports in CSV format.

## Technologies Used
- MVC (Model-View-Controller) design pattern with .NET Core 7.0.
- Entity Framework Core for data abstraction and storage.
- SQLite for development and MySQL for production databases.
- Docker for containerization and simplified deployment.

## Deployment
The application is containerized using Docker, facilitating deployment to platforms like Azure Container Registry and Azure App Service.

To build and deploy the image to an Azure Container Registry instance, simply run `./DeployToACR.sh --acr-token [token] --acr-instance [instance-fqdn] --acr-username [username]`.

## Contribution
Contributions to this project are welcome. Please follow the standard procedures for submitting issues and pull requests.

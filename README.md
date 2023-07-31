<h1 align="center">Crudtoso API</h1>

Crudtoso API is an ASP.NET Core REST API used by the online store CRUDToso to manage inventory. It communicates with the Crudtoso SQL server and lets users interact with it in a RESTful manner.

# Documentation
The API is documented via Swagger/OpenAPI.

# Data Model
## Bike
* Autoincremented Product ID
* Product Name
* Category
* Price
* Stock Quantity
* Supplier
* Date Added

# API Features
**Generic CRUD Operations**: API Contains full CRUD operations for the Bike database
**Data Transfer Objects**: The API uses AutoMapper to create mappings between domain models (entities) and Data Transfer Objects (DTOs).

# CI/CD
The docker repo used in the Azure deployment of crudtoso-api can be found here: https://hub.docker.com/repository/docker/sybrenk/crudtoso-api/
The Azure deployment features continuous deployment, using a Docker Webhook.

# Footage
Deployment footage is included in the `Deployments` directory. This includes the used commands and screenshots.

# Issues
- When deployed on Azure, the API does not grab the connectionstring from the Web App environment, despite being specified in the Application settings along with `ASPNETCORE_ENVIRONMENT = Production`. 

# Future implementations
- Images: Blob Storage + CDN media endpoint
- Authentication via Azure AD

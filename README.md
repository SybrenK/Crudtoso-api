<h1 align="center">Streamlink</h1>

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

#CI/CD
The docker repo used in the Azure deployment of crudtoso-api can be found here: https://hub.docker.com/repository/docker/sybrenk/crudtoso-api/

# Footage
Deployment footage is included in the `Deployments` directory. This includes the used commands and screenshots.

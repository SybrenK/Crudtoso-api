<h1 align="center">Crudtoso API</h1>

Crudtoso API is an ASP.NET Core REST API used by the online store CRUDToso to manage inventory. It communicates with the Crudtoso SQL server and lets users interact with it in a RESTful manner.

## Name of our project: Online Retail Store's Inventory Management System

### Scenario of the project
We will assume the role of a cloud engineer at an online retail company that sells bicycles. The company needs a system that allows the inventory manager to add, update, and delete the product information and the system should also display the products information to the end-users. With the increased traffic and the growing number of products, the company needs a solution that can easily scale to meet the demand and is cost-effective. This system will ensure that the inventory manager can efficiently manage the inventory and the users can view the available products. Therefore, we will be creating an API that can perform CRUD operations on the products in the inventory.

### Infrastructure Setup
- **Resource Group:** `rg-azure-project-prod-norwayeast-ZcDrL`
- **Virtual Network:** Created an Azure Virtual Network and associated Network Security Group for a secure isolated network. (Vnet is called `vnet-prod-norwayeast-001`)
- **Public IP Addresses:** Allocated Public IP Addresses for the services that require them.

### Database Setup
- **Azure SQL Database:** Provision Azure SQL Database for relational data storage. (Made SQL database in Azure with a query file called `bikesdb`)
- **Access Control:** Set up Role-Based Access Control (RBAC) for secure database access. (Note: No Azure AD permission, used SQL Authentication instead)

### Event-Driven Architecture
- **Event Integration:** Depending on the needs, integrate Azure Event Grid or Azure Logic Apps for event-driven notifications to administrators whenever specific events occur. (Name of the logic app is `InventoryNotificationLogicApp`, name of the workflow is `ProductInventoryChangeNotification`. This workflow triggers when a stock quantity is below a certain amount and sends an email to a specified recipient)

### Security and Encryption
- **Network Security:** Implemented Azure Firewall for network security.
- **Encryption:** Created Azure Key Vault and Azure Storage Service Encryption to encrypt sensitive data in transit and at rest. (Note: Not possible to use yet because of Azure AD permissions missing, set to future implementations)

## Documentation
The API is documented via Swagger/OpenAPI.

## Data Model
### Bike
- Autoincremented Product ID
- Product Name
- Category
- Price
- Stock Quantity
- Supplier
- Date Added

## API Features
- **Generic CRUD Operations:** API Contains full CRUD operations for the Bike database
- **Data Transfer Objects:** The API uses AutoMapper to create mappings between domain models (entities) and Data Transfer Objects (DTOs).

## CI/CD
The docker repo used in the Azure deployment of crudtoso-api can be found here: https://hub.docker.com/repository/docker/sybrenk/crudtoso-api/ The Azure deployment features continuous deployment, using a Docker Webhook.

## Footage
Deployment footage is included in the Deployments directory. This includes:
- The used commands and screenshots of the API
- Metrics screenshots for the API and SQL database

## Cost management
Cost estimates are provided in the Costs directory. Currently contains the following:
- CostEstimateTestArchitecture: Costs of our staging environment
- CostEstimateDeploymentSmall: Hypothetical delivery environment costs

## Issues
- When deployed on Azure, the API does not grab the connection string from the Web App environment, despite being specified in the Application settings along with ASPNETCORE_ENVIRONMENT = Production.
- (01-08) Event Grid cannot send emails on event. (Said will add on this)

## Future implementations
- Images: Blob Storage + CDN media endpoint
- Authentication via Azure AD
- Autoscaling: When demand increases sufficiently for Standard/Premium Plan

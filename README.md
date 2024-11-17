# Product Catalog Application

## Architecture
Application for product catalog manipulations and data retrieval.

This application follows a three-layer architecture: Presentation, Business, and Data layers are implemented. It leverages the UnitOfWork and Repository patterns to ensure better separation of concerns and adding some more abstractions over data access and working with transactions for better testing experience and posibility of adding another data providers such as inmemo db or noSQL dbs. 
Additionally, a Contracts library is used to avoid circular dependencies between layers.
Application layer utilizes custom created BussinesActionResult class for better results and easier mapping to ActionResults in controllers.
Project also includes Core library with some utility classes, used throughout the solution.
Application implements some custom validation logics and ensures proper values are provided for data manipulation.

## Technologies

- **ORM**: Entity Framework
- **Mapping**: [AutoMapper](https://automapper.org/) (Version: 12.0)
- **Validation**: [FluentValidation](https://fluentvalidation.net/) (Version: 11.0)
- **Database**: [MSSQL](https://www.microsoft.com/en-us/sql-server) (Version: 2022)
- **Docs**: [Swagger](https://swagger.io/) (Version: 6.0)
- **Tests**: [NUnit](https://nunit.org/) (Version: 3.13)
- **Mocking**: [Moq](https://github.com/moq/moq4) (Version: 4.20.72)
- **Framework**: [.NET 8.0 LTS](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) (Long-Term Support)

## Startup Guide

### Prerequisites:
- .NET 8.0 installed: [Download .NET 8.0 LTS](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Docker Engine installed: [Get Docker](https://www.docker.com/get-started)

This guide assumes setting up the database in a Docker container.

### Steps:

1. **Clone the Solution**  
   Clone the repository using Visual Studio or your preferred Git client.

2. **Set the Startup Project**  
   Set `Alza.Host` as the startup project in Visual Studio.

3. **Run Docker Command**  
   Open a command prompt (cmd) and run the following command to start a Docker container with a clean MSSQL database:

   ```bash
   docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong(!)Password" -e "MSSQL_PID=Evaluation" -p 1433:1433 --name sqlpreview -d mcr.microsoft.com/mssql/server:2022-preview-ubuntu-22.04
   ```
   This will startup a docker container with clean MSSQL database. Connection tring is already setup to corespon the values from command.

4. **Start the application**\
   Application can be started as a usual project in Visual Studio. It will migrate database and open browser window with swagger in it.

6. **Switching between mock and db data**\
   For switching between mock data and database parameter UseMock in appsettings.json (Alza.Host) is used. Just switch the value to needed one when testing.

## Testing

The business logic layer is covered by 100% unit tests. NUnit testing framework is used along with the Moq library for mocking dependencies.

### Running Tests

#### Using Visual Studio:
1. Open the solution in Visual Studio.
2. Navigate to `Test` > `Run All Tests`.
3. The results will be displayed in a popup window.

#### Using the .NET CLI:
1. Open a terminal in the root directory of the project.
2. Run the following command:
   ```bash
   dotnet test
   
## API Documentation

The API is fully documented and versioned. All available endpoints, response schemas, and error handling details can be accessed through the integrated Swagger UI.

### Swagger UI

The API documentation is accessible at:

/swagger/index.html

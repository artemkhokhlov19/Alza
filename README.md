# Product Catalog Application

## Architecture
This application follows a three-layer architecture: Presentation, Business, and Data layers. It leverages the UnitOfWork and Repository patterns to ensure better separation of concerns. Additionally, a Contracts library is used to avoid circular dependencies between layers.
Application layer utilizes custom created BussinesActionResult class for better results and easier mapping to ActionResults in controllers.
Project also includes Core library with some utility classes, used throughout the solution.

## Technologies
- **ORM**: Entity Framework
- **Mapping**: AutoMapper
- **Validation**: FluentValidation
- **Database**: MSSQL
- **Docs**: Swagger
- **Tests**: NUnit
- **Mocking**: Moq

## Setup Guide
**TODO**

## Testing
**TODO**

## API Documentation
**TODO**

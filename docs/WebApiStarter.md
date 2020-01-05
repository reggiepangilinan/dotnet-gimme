# Gimme!
[< Back](https://github.com/reggieboyYEAH/dotnet-gimme/blob/master/README.md)

## Web Api Starter
An Asp.Net Core 3.1 Web Api Starter Kit.

It will generate RESTFul Api backend that is enterprise ready.

The architecture is mainly based on concepts such as

- [SOLID Principle](https://www.youtube.com/watch?v=TMuno5RZNeE)
- [CLEAN Architecture](https://www.youtube.com/watch?v=IAcxetnsiCQ)
- [JasonGT's Northwind Traders](https://github.com/JasonGT/NorthwindTraders)

Watch his full presentation here
<iframe width="560" height="315" src="https://www.youtube-nocookie.com/embed/fAJrVf8f6M4" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen></iframe>

# How to use the `webapi-starter` command.

### 1. Generate the whole solution.
```
Usage: gimme webapi-starter [arguments] [options]

Arguments:
  SolutionName  Name of the solution
```

### 2. Initialize database

To initialize database. Create the initial migration

> Make sure you already created an entitity that is wired in the DbContext

Go to the `Persistence` project
```
cd ProjectName.Persistence
```
Then execute
```
dotnet ef migrations add InitialCreate
```
You should see a `Migrations` folder with the initial one created.

# Solution Overview and Recommendations

## Projects List
- **WebApi** - The main project, where
    - The controller lives
    - We bootstrap the application - Register dependency and services etc.
    - Catch exceptions
- **Application** - Project where most of the application logic lives.
    - Has the `Commands` and `Queries` for the application
    - Has the `Custom Exceptions`
    - Application level `Enums`
    - Application level `Validations`
    - Application `Seed`


- **Application.UnitTest** - Project that contains the Application Unit Tests.

- **Persistence** - Project that contains
    - EF DbContext
    - EF Migrations
    - EF Entity Mapping

- **Domain** - Project that contains
    - Domain Entities
    - Value Objects


- **Services** - Project that contains
    - External services

## Recommended Folder structure
When the solution is generated it will the folder structure below. You can extend or modify it as needed.

```
  /SolutionName.Api
      |--Configurations
          |---Auth.cs
          |---Automapper.cs
          |---Db.cs
          |---FluentValidation.cs
          |---MediatR.cs
          |---Mvc.cs
          |---Services.cs
          |---Swagger.cs
      |--Controllers {All of your controllers goes here}
      |--Filters
          |---ActionValidationFilterAttribute.cs
      |--Infrastructure
          |---MediatorController.cs
      |--Security
      |--wwwroot
      |--Program.cs
      |--Startup.cs
      |--appsettings.json

  /SolutionName.Application
      |--Module1
          |--Commands
          |--Queries
          |--Models        
      |--Module2
          |--Commands
          |--Queries
          |--Models
      |--Enums
      |--Exceptions
      |--Security 
      |--Seed    

  /SolutionName.Application.UnitTest
      ...
      TODO
      ...
  /SolutionName.Persistence
        |--Context
        |--Migrations

  /SolutionName.Domain
        |--Entities
        |--Enums

  /SolutionName.Services
        |--Service1
        |--Service2

```

#### Web API analyzers

In ASP.NET Core 3.0 or later, the analyzers are included in the .NET Core SDK. To enable the analyzer in your project, include the IncludeOpenAPIAnalyzers property in the api project file:

```
<PropertyGroup>
 <IncludeOpenAPIAnalyzers>true</IncludeOpenAPIAnalyzers>
</PropertyGroup>
```

https://docs.microsoft.com/en-us/aspnet/core/web-api/advanced/analyzers?view=aspnetcore-3.1&tabs=visual-studio


#### Nuget Packages Used
- [AutoMapper](https://automapper.org/)
- [FluentValidation](https://github.com/JeremySkinner/FluentValidation)
- [MediatR](https://github.com/jbogard/MediatR)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) 
- [NSwag.AspNetCore](https://github.com/RSuter/NSwag)
- [nunit](https://nunit.org/)
- [Moq](https://github.com/moq/moq4)
- [RestSharp](http://restsharp.org/)



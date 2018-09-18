# Gimme!

> A CLI tool for dotnetcore that gives you want you want ;)

```
 ==============================================================
 |                  _       _              _                  |
 |               __| | ___ | |_ _ __   ___| |_                |
 |              / _` |/ _ \| __| '_ \ / _ \ __|               |
 |             | (_| | (_) | |_| | | |  __/ |_                |
 |              \__,_|\___/ \__|_| |_|\___|\__|               |
 |                                                            |
 |                    _                            _          |
 |               __ _(_)_ __ ___  _ __ ___   ___  / \         |
 |              / _` | | '_ ` _ \| '_ ` _ \ / _ \/  /         |
 |             | (_| | | | | | | | | | | | |  __/\_/          |
 |              \__, |_|_| |_| |_|_| |_| |_|\___\/            |
 |              |___/                                         |
 ==============================================================
```

## How to install

To install `gimme` globally run this command
```
dotnet tool install --global dotnet-gimme --version 1.0.0
```
Once installed, the command `gimme` can now be used.


## Documentation

> TODO : Create proper documentation

- SOLID Principle
- CLEAN Architecture
- Mediator + CQRS Pattern
- Validations - FluentValidations
- Data Access using Entity Framework Core
- EF Core Migrations
  - https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/

To initialize database. Create the initial migration

> Make sure you already created an entitity that is wired in the DbContext

Go to the `Persistance` project
```
cd ProjectName.Persistance
```
Then execute
```
dotnet ef migrations add InitialCreate
```
You should see a `Migrations` folder with the initial one created.

- AutoMapper
- Swagger - Open Api Specs
- Exception Handling



### Development
Make sure you are in the `gimme` folder
```
cd gimme
```

You can create your tool package like this:
```
dotnet pack --output ./ -c Release
```

This creates a file named `dotnet-gimme.x.x.x.nupkg` 

You can install your package like this:
```
dotnet tool install -g dotnet-gimme --add-source ./ --version x.x.x
```

Additional commands
dotnet tool has other commands you can invoke.

```
dotnet tool list -g
dotnet tool uninstall -g dotnet-gimme
dotnet tool update -g dotnet-gimme
```

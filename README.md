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
dotnet tool install --global dotnet-gimme --version 1.0.6
```
Once installed, the command `gimme` can now be used.


## Documentation

```
Usage: gimme [options] [command]

Options:
  -?|-h|--help    Show help information

Commands:
  api-controller  Web Api Controller that derives from Mediator Controller
  app-command     Application Command (CQRS)
  app-exception   Application Exception
  app-model       Application Model
  app-query       Application Query (CQRS)
  settings        Creates a 'gimmesettings.json' in the current directory
  webapi-starter  An ASP.NET Core WebApi Starter Kit.

Run 'gimme [command] --help' for more information about a command.
```


- [Web Api Starter](https://github.com/reggieboyYEAH/dotnet-gimme/blob/master/docs/WebApiStarter.md)
- Roadmap

## Development
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

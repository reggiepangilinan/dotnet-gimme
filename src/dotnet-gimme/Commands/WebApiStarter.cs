using System;
using System.ComponentModel.DataAnnotations;
using dotnetgimme.Utils;
using McMaster.Extensions.CommandLineUtils;

namespace dotnetgimme.Commands
{
    [Command(Name = "webapi-starter", Description = "An ASP.NET Core WebApi Starter Kit.")]
    public class WebApiStarter
    {
        [Required(ErrorMessage = "You must specify the solution name.")]
        [Argument(1, Description = "Name of the solution")]
        public string SolutionName { get; }

        public void OnExecute()
        {
            ConsoleUtil.HiglightedMessage("Please sit back and wait while we generate things for you...");

            Console.WriteLine(ShellHelper.Bash($"mkdir {SolutionName}"));

            ConsoleUtil.HiglightedMessage("Creating Solution File");
            Console.WriteLine(ShellHelper.Bash($"dotnet new sln -n {SolutionName} -o {SolutionName}"));

            ConsoleUtil.HiglightedMessage("Creating WebApi");
            Console.WriteLine(ShellHelper.Bash($"dotnet new webapi -n {SolutionName}.Api -o {SolutionName}/{SolutionName}.Api"));

            ConsoleUtil.HiglightedMessage("Creating Application Project");
            Console.WriteLine(ShellHelper.Bash($"dotnet new classlib -n {SolutionName}.Application -o {SolutionName}/{SolutionName}.Application -f netcoreapp2.1"));

            ConsoleUtil.HiglightedMessage("Creating Services");
            Console.WriteLine(ShellHelper.Bash($"dotnet new classlib -n {SolutionName}.Services -o {SolutionName}/{SolutionName}.Services -f netcoreapp2.1"));

            ConsoleUtil.HiglightedMessage("Creating Domain");
            Console.WriteLine(ShellHelper.Bash($"dotnet new classlib -n {SolutionName}.Domain -o {SolutionName}/{SolutionName}.Domain -f netcoreapp2.1"));

            ConsoleUtil.HiglightedMessage("Creating Persistance");
            Console.WriteLine(ShellHelper.Bash($"dotnet new classlib -n {SolutionName}.Persistance -o {SolutionName}/{SolutionName}.Persistance -f netcoreapp2.1"));


            ConsoleUtil.HiglightedMessage("Add projects to solution");
            Console.WriteLine(ShellHelper.Bash($"dotnet sln {SolutionName}/{SolutionName}.sln add {SolutionName}/{SolutionName}.Api/{SolutionName}.Api.csproj"));
            Console.WriteLine(ShellHelper.Bash($"dotnet sln {SolutionName}/{SolutionName}.sln add {SolutionName}/{SolutionName}.Application/{SolutionName}.Application.csproj"));
            Console.WriteLine(ShellHelper.Bash($"dotnet sln {SolutionName}/{SolutionName}.sln add {SolutionName}/{SolutionName}.Services/{SolutionName}.Services.csproj"));
            Console.WriteLine(ShellHelper.Bash($"dotnet sln {SolutionName}/{SolutionName}.sln add {SolutionName}/{SolutionName}.Domain/{SolutionName}.Domain.csproj"));
            Console.WriteLine(ShellHelper.Bash($"dotnet sln {SolutionName}/{SolutionName}.sln add {SolutionName}/{SolutionName}.Persistance/{SolutionName}.Persistance.csproj"));

            ConsoleUtil.HiglightedMessage("Configure project reference");
            Console.WriteLine(ShellHelper.Bash($"dotnet add {SolutionName}/{SolutionName}.Api/{SolutionName}.Api.csproj reference {SolutionName}/{SolutionName}.Application/{SolutionName}.Application.csproj"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {SolutionName}/{SolutionName}.Api/{SolutionName}.Api.csproj reference {SolutionName}/{SolutionName}.Services/{SolutionName}.Services.csproj"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {SolutionName}/{SolutionName}.Api/{SolutionName}.Api.csproj reference {SolutionName}/{SolutionName}.Persistance/{SolutionName}.Persistance.csproj"));


            Console.WriteLine(ShellHelper.Bash($"dotnet add {SolutionName}/{SolutionName}.Application/{SolutionName}.Application.csproj reference {SolutionName}/{SolutionName}.Persistance/{SolutionName}.Persistance.csproj"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {SolutionName}/{SolutionName}.Application/{SolutionName}.Application.csproj reference {SolutionName}/{SolutionName}.Domain/{SolutionName}.Domain.csproj"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {SolutionName}/{SolutionName}.Application/{SolutionName}.Application.csproj reference {SolutionName}/{SolutionName}.Services/{SolutionName}.Services.csproj"));

            Console.WriteLine(ShellHelper.Bash($"dotnet add {SolutionName}/{SolutionName}.Persistance/{SolutionName}.Persistance.csproj reference {SolutionName}/{SolutionName}.Domain/{SolutionName}.Domain.csproj"));


            ConsoleUtil.HiglightedMessage($"Add package reference to {SolutionName}/{SolutionName}.Api/{SolutionName}.Api.csproj");
            Console.WriteLine(ShellHelper.Bash($"dotnet add {SolutionName}/{SolutionName}.Api/{SolutionName}.Api.csproj package AutoMapper.Extensions.Microsoft.DependencyInjection -f netcoreapp2.1"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {SolutionName}/{SolutionName}.Api/{SolutionName}.Api.csproj package FluentValidation.AspNetCore -f netcoreapp2.1"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {SolutionName}/{SolutionName}.Api/{SolutionName}.Api.csproj package MediatR -f netcoreapp2.1"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {SolutionName}/{SolutionName}.Api/{SolutionName}.Api.csproj package MediatR.Extensions.Microsoft.DependencyInjection -f netcoreapp2.1"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {SolutionName}/{SolutionName}.Api/{SolutionName}.Api.csproj package Microsoft.EntityFrameworkCore.SqlServer -f netcoreapp2.1"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {SolutionName}/{SolutionName}.Api/{SolutionName}.Api.csproj package Microsoft.EntityFrameworkCore.Tools -f netcoreapp2.1"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {SolutionName}/{SolutionName}.Api/{SolutionName}.Api.csproj package Microsoft.Extensions.Configuration.Json -f netcoreapp2.1"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {SolutionName}/{SolutionName}.Api/{SolutionName}.Api.csproj package NSwag.AspNetCore -f netcoreapp2.1"));

            ConsoleUtil.HiglightedMessage($"Add package reference to {SolutionName}/{SolutionName}.Application/{SolutionName}.Application.csproj");
            Console.WriteLine(ShellHelper.Bash($"dotnet add {SolutionName}/{SolutionName}.Application/{SolutionName}.Application.csproj package AutoMapper -f netcoreapp2.1"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {SolutionName}/{SolutionName}.Application/{SolutionName}.Application.csproj package FluentValidation -f netcoreapp2.1"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {SolutionName}/{SolutionName}.Application/{SolutionName}.Application.csproj package MediatR -f netcoreapp2.1"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {SolutionName}/{SolutionName}.Application/{SolutionName}.Application.csproj package NJsonSchema -f netcoreapp2.1"));

            ConsoleUtil.HiglightedMessage($"Add package reference to {SolutionName}/{SolutionName}.Persistance/{SolutionName}.Persistance.csproj");
            Console.WriteLine(ShellHelper.Bash($"dotnet add {SolutionName}/{SolutionName}.Persistance/{SolutionName}.Persistance.csproj package Microsoft.EntityFrameworkCore.SqlServer -f netcoreapp2.1"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {SolutionName}/{SolutionName}.Persistance/{SolutionName}.Persistance.csproj package Microsoft.EntityFrameworkCore.Tools -f netcoreapp2.1"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {SolutionName}/{SolutionName}.Persistance/{SolutionName}.Persistance.csproj package Microsoft.Extensions.Configuration.Json -f netcoreapp2.1"));

            ConsoleUtil.HiglightedMessage($"Add package reference to {SolutionName}/{SolutionName}.Services/{SolutionName}.Services.csproj");
            Console.WriteLine(ShellHelper.Bash($"dotnet add {SolutionName}/{SolutionName}.Services/{SolutionName}.Services.csproj package RestSharp -f netcoreapp2.1"));

            ConsoleUtil.SuccessMessage("All done lazy bum!");
        }
    }
}

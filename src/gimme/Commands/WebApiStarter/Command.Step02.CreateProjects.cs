using gimme.Utils;
using System;
using System.IO;

namespace gimme.Commands.WebApiStarter
{

    public partial class Command
    {
        private void Step02_CreateProjects()
        {
            ConsoleUtil.HiglightedMessage("Creating WebApi");
            Console.WriteLine(shellService.Exec($"dotnet new webapi -n {variable.ApiProjectName} -o {SolutionName}/{variable.ApiProjectName} -f netcoreapp2.2"));

            ConsoleUtil.HiglightedMessage("Creating Application Project");
            Console.WriteLine(shellService.Exec($"dotnet new classlib -n {variable.ApplicationProjectName} -o {SolutionName}/{variable.ApplicationProjectName} -f netcoreapp2.2"));

            ConsoleUtil.HiglightedMessage("Creating Application Unit Test Project");
            Console.WriteLine(shellService.Exec($"dotnet new classlib -n {variable.ApplicationUnitTestProjectName} -o {SolutionName}/{variable.ApplicationUnitTestProjectName} -f netcoreapp2.2"));

            ConsoleUtil.HiglightedMessage("Creating Services Project");
            Console.WriteLine(shellService.Exec($"dotnet new classlib -n {variable.ServicesProjectName} -o {SolutionName}/{variable.ServicesProjectName}  -f netcoreapp2.2"));

            ConsoleUtil.HiglightedMessage("Creating Domain Project");
            Console.WriteLine(shellService.Exec($"dotnet new classlib -n {variable.DomainProjectName} -o {SolutionName}/{variable.DomainProjectName}  -f netcoreapp2.2"));

            ConsoleUtil.HiglightedMessage("Creating Persistance Project");
            Console.WriteLine(shellService.Exec($"dotnet new classlib -n {variable.PersistanceProjectName} -o {SolutionName}/{variable.PersistanceProjectName} -f netcoreapp2.2"));

            ConsoleUtil.HiglightedMessage("Add projects to solution");
            Console.WriteLine(shellService.Exec($"dotnet sln {variable.SLN_SolutionFile} add {variable.CSPROJ_WebApiProjectFile}"));
            Console.WriteLine(shellService.Exec($"dotnet sln {variable.SLN_SolutionFile} add {variable.CSPROJ_ApplicationProjectFile}"));
            Console.WriteLine(shellService.Exec($"dotnet sln {variable.SLN_SolutionFile} add {variable.CSPROJ_ApplicationUnitTestProjectFile}"));
            Console.WriteLine(shellService.Exec($"dotnet sln {variable.SLN_SolutionFile} add {variable.CSPROJ_ServicesProjectFile}"));
            Console.WriteLine(shellService.Exec($"dotnet sln {variable.SLN_SolutionFile} add {variable.CSPROJ_DomainProjectFile}"));
            Console.WriteLine(shellService.Exec($"dotnet sln {variable.SLN_SolutionFile} add {variable.CSPROJ_PersistanceProjectFile}"));

            ConsoleUtil.HiglightedMessage("Configure project reference");
            Console.WriteLine(shellService.Exec($"dotnet add {variable.CSPROJ_WebApiProjectFile} reference {variable.CSPROJ_ApplicationProjectFile}"));
            Console.WriteLine(shellService.Exec($"dotnet add {variable.CSPROJ_WebApiProjectFile} reference {variable.CSPROJ_ServicesProjectFile}"));
            Console.WriteLine(shellService.Exec($"dotnet add {variable.CSPROJ_WebApiProjectFile} reference {variable.CSPROJ_PersistanceProjectFile}"));
            Console.WriteLine(shellService.Exec($"dotnet add {variable.CSPROJ_ApplicationProjectFile} reference {variable.CSPROJ_PersistanceProjectFile}"));
            Console.WriteLine(shellService.Exec($"dotnet add {variable.CSPROJ_ApplicationProjectFile} reference {variable.CSPROJ_DomainProjectFile}"));
            Console.WriteLine(shellService.Exec($"dotnet add {variable.CSPROJ_ApplicationProjectFile} reference {variable.CSPROJ_ServicesProjectFile}"));
            Console.WriteLine(shellService.Exec($"dotnet add {variable.CSPROJ_ApplicationUnitTestProjectFile} reference {variable.CSPROJ_ApplicationProjectFile}"));
            Console.WriteLine(shellService.Exec($"dotnet add {variable.CSPROJ_ApplicationUnitTestProjectFile} reference {variable.CSPROJ_PersistanceProjectFile}"));
            Console.WriteLine(shellService.Exec($"dotnet add {variable.CSPROJ_ApplicationUnitTestProjectFile} reference {variable.CSPROJ_ServicesProjectFile}"));
            Console.WriteLine(shellService.Exec($"dotnet add {variable.CSPROJ_ApplicationUnitTestProjectFile} reference {variable.CSPROJ_DomainProjectFile}"));
            Console.WriteLine(shellService.Exec($"dotnet add {variable.CSPROJ_PersistanceProjectFile} reference {variable.CSPROJ_DomainProjectFile}"));


            ConsoleUtil.HiglightedMessage("Delete All Default Files");
            var defaultFile = "Class1.cs";
            filesService.DeleteFile(Path.Combine(variable.SolutionBasePath, variable.ApplicationProjectName, defaultFile));
            filesService.DeleteFile(Path.Combine(variable.SolutionBasePath, variable.ApplicationUnitTestProjectName, defaultFile));
            filesService.DeleteFile(Path.Combine(variable.SolutionBasePath, variable.ServicesProjectName, defaultFile));
            filesService.DeleteFile(Path.Combine(variable.SolutionBasePath, variable.PersistanceProjectName, defaultFile));
            filesService.DeleteFile(Path.Combine(variable.SolutionBasePath, variable.DomainProjectName, defaultFile));

            ConsoleUtil.HiglightedMessage("Creating GitIgnore");
            var gitignorefilename = Path.Combine(variable.SolutionBasePath, ".gitignore");
            filesService.WriteAllTextToFile(gitignorefilename, ResourceUtil.GetResourceText("WAS_GitIgnore"));

            ConsoleUtil.HiglightedMessage("Creating gimmesettings.json");
            filesService.WriteAllTextToFile(variable.GimmeSettingsJsonPath, ResourceUtil.GetResourceText("GimmeSettings").Replace("{{name}}", variable.SolutionName));
        }

    }
}

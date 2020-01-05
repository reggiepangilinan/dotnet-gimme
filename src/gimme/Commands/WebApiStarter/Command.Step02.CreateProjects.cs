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
            Console.WriteLine(shellService.Exec($"dotnet new webapi -n {variable.ApiProjectName} -o {SolutionName}/{variable.ApiProjectName} -f netcoreapp3.1"));

            ConsoleUtil.HiglightedMessage("Creating Application Project");
            Console.WriteLine(shellService.Exec($"dotnet new classlib -n {variable.ApplicationProjectName} -o {SolutionName}/{variable.ApplicationProjectName} -f netcoreapp3.1"));

            ConsoleUtil.HiglightedMessage("Creating Application Unit Test Project");
            Console.WriteLine(shellService.Exec($"dotnet new classlib -n {variable.ApplicationUnitTestProjectName} -o {SolutionName}/{variable.ApplicationUnitTestProjectName} -f netcoreapp3.1"));

            ConsoleUtil.HiglightedMessage("Creating Services Project");
            Console.WriteLine(shellService.Exec($"dotnet new classlib -n {variable.ServicesProjectName} -o {SolutionName}/{variable.ServicesProjectName}  -f netcoreapp3.1"));

            ConsoleUtil.HiglightedMessage("Creating Services Unit Test Project");
            Console.WriteLine(shellService.Exec($"dotnet new classlib -n {variable.ServicesUnitTestProjectName} -o {SolutionName}/{variable.ServicesUnitTestProjectName}  -f netcoreapp3.1"));

            ConsoleUtil.HiglightedMessage("Creating Domain Project");
            Console.WriteLine(shellService.Exec($"dotnet new classlib -n {variable.DomainProjectName} -o {SolutionName}/{variable.DomainProjectName}  -f netcoreapp3.1"));

            ConsoleUtil.HiglightedMessage("Creating Persistence Project");
            Console.WriteLine(shellService.Exec($"dotnet new classlib -n {variable.PersistenceProjectName} -o {SolutionName}/{variable.PersistenceProjectName} -f netcoreapp3.1"));

            ConsoleUtil.HiglightedMessage("Add projects to solution");
            Console.WriteLine(shellService.Exec($"dotnet sln {variable.SLN_SolutionFile} add {variable.CSPROJ_WebApiProjectFile}"));
            Console.WriteLine(shellService.Exec($"dotnet sln {variable.SLN_SolutionFile} add {variable.CSPROJ_ApplicationProjectFile}"));
            Console.WriteLine(shellService.Exec($"dotnet sln {variable.SLN_SolutionFile} add {variable.CSPROJ_ApplicationUnitTestProjectFile}"));
            Console.WriteLine(shellService.Exec($"dotnet sln {variable.SLN_SolutionFile} add {variable.CSPROJ_ServicesProjectFile}"));
            Console.WriteLine(shellService.Exec($"dotnet sln {variable.SLN_SolutionFile} add {variable.CSPROJ_ServicesUnitTestProjectFile}"));
            Console.WriteLine(shellService.Exec($"dotnet sln {variable.SLN_SolutionFile} add {variable.CSPROJ_DomainProjectFile}"));
            Console.WriteLine(shellService.Exec($"dotnet sln {variable.SLN_SolutionFile} add {variable.CSPROJ_PersistenceProjectFile}"));

            ConsoleUtil.HiglightedMessage("Configure project reference");
            Console.WriteLine(shellService.Exec($"dotnet add {variable.CSPROJ_WebApiProjectFile} reference {variable.CSPROJ_ApplicationProjectFile}"));
            Console.WriteLine(shellService.Exec($"dotnet add {variable.CSPROJ_WebApiProjectFile} reference {variable.CSPROJ_ServicesProjectFile}"));
            Console.WriteLine(shellService.Exec($"dotnet add {variable.CSPROJ_WebApiProjectFile} reference {variable.CSPROJ_PersistenceProjectFile}"));
            Console.WriteLine(shellService.Exec($"dotnet add {variable.CSPROJ_ApplicationProjectFile} reference {variable.CSPROJ_PersistenceProjectFile}"));
            Console.WriteLine(shellService.Exec($"dotnet add {variable.CSPROJ_ApplicationProjectFile} reference {variable.CSPROJ_DomainProjectFile}"));
            Console.WriteLine(shellService.Exec($"dotnet add {variable.CSPROJ_ApplicationProjectFile} reference {variable.CSPROJ_ServicesProjectFile}"));
            Console.WriteLine(shellService.Exec($"dotnet add {variable.CSPROJ_ApplicationUnitTestProjectFile} reference {variable.CSPROJ_ApplicationProjectFile}"));
            Console.WriteLine(shellService.Exec($"dotnet add {variable.CSPROJ_ApplicationUnitTestProjectFile} reference {variable.CSPROJ_PersistenceProjectFile}"));
            Console.WriteLine(shellService.Exec($"dotnet add {variable.CSPROJ_ApplicationUnitTestProjectFile} reference {variable.CSPROJ_ServicesProjectFile}"));
            Console.WriteLine(shellService.Exec($"dotnet add {variable.CSPROJ_ApplicationUnitTestProjectFile} reference {variable.CSPROJ_DomainProjectFile}"));
            Console.WriteLine(shellService.Exec($"dotnet add {variable.CSPROJ_PersistenceProjectFile} reference {variable.CSPROJ_DomainProjectFile}"));
            Console.WriteLine(shellService.Exec($"dotnet add {variable.CSPROJ_ServicesUnitTestProjectFile} reference {variable.CSPROJ_ServicesProjectFile}"));


            ConsoleUtil.HiglightedMessage("Delete All Default Files");
            var defaultFile = "Class1.cs";
            filesService.DeleteFile(Path.Combine(variable.SolutionBasePath, variable.ApplicationProjectName, defaultFile));
            filesService.DeleteFile(Path.Combine(variable.SolutionBasePath, variable.ApplicationUnitTestProjectName, defaultFile));
            filesService.DeleteFile(Path.Combine(variable.SolutionBasePath, variable.ServicesProjectName, defaultFile));
            filesService.DeleteFile(Path.Combine(variable.SolutionBasePath, variable.ServicesUnitTestProjectName, defaultFile));
            filesService.DeleteFile(Path.Combine(variable.SolutionBasePath, variable.PersistenceProjectName, defaultFile));
            filesService.DeleteFile(Path.Combine(variable.SolutionBasePath, variable.DomainProjectName, defaultFile));

            ConsoleUtil.HiglightedMessage("Creating GitIgnore");
            var gitignorefilename = Path.Combine(variable.SolutionBasePath, ".gitignore");
            filesService.WriteAllTextToFile(gitignorefilename, ResourceUtil.GetResourceText("WAS_GitIgnore"));

            ConsoleUtil.HiglightedMessage("Creating gimmesettings.json");
            filesService.WriteAllTextToFile(variable.GimmeSettingsJsonPath, ResourceUtil.GetResourceText("GimmeSettings").Replace("{{name}}", variable.SolutionName));
        }

    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using dotnet_gimme;
using dotnetgimme.Constants;
using dotnetgimme.Utils;
using McMaster.Extensions.CommandLineUtils;

namespace dotnetgimme.Commands.WebApiStarter
{
    [Command(Name = "webapi-starter", Description = "An ASP.NET Core WebApi Starter Kit.")]
    public class WebApiStarter
    {
        WebApiStaterVariableGenerator variable;

        [Required(ErrorMessage = "You must specify the solution name.")]
        [Argument(1, Description = "Name of the solution")]
        public string SolutionName { get; }

        public void OnExecute()
        {
            variable = new WebApiStaterVariableGenerator(SolutionName);

            ConsoleUtil.HiglightedMessage("Please sit back and wait while we generate things for you...");

            CreateSolutionFolder();
            CreateSolutionFile();

            //Create Projects
            CreateWebApiProject();
            CreateApplicationProject();
            CreateApplicationUnitTestProject();
            CreateServicesProject();
            CreateDomainProject();
            CreatePersistanceProject();
            AddAllProjectsToSolution();

            //Add Project Reference
            ConfigureProjectReference();

            //Configure Packages
            ConfigureWebApiPackages();
            ConfigureApplicationPackages();
            ConfigurePersistancePackages();
            ConfigureServicesPackages();

            DeleteAllDefaultFiles();
            DomainBaseEntity();
            CreateGitIgnore();
            CreateGimmeSettings();
            ScaffoldApplication();
            ScaffoldWebApi();

            ConsoleUtil.SuccessMessage("All done lazy bum!");
        }

        void ScaffoldApplication()
        {
            ConsoleUtil.HiglightedMessage($"Creating Application Exceptions {variable.ApplicationProject}");
            var ApplicationPath = Path.Combine(Environment.CurrentDirectory, $"{SolutionName}/{SolutionName}.Application");
            var ExceptionsPath = Path.Combine(ApplicationPath, "Exceptions");
            Directory.CreateDirectory(ExceptionsPath);

            var RecordAlreadyExistsExceptionTemplate = Path.Combine(GimmeConfiguration.TemplateDirectory, "WebApiStarter", "Application", "RecordAlreadyExistsExceptionTemplate.txt");
            var RecordNotFoundExceptionTemplate = Path.Combine(GimmeConfiguration.TemplateDirectory, "WebApiStarter", "Application", "RecordNotFoundExceptionTemplate.txt");

            var recordAlreadyExistsExceptionContent = File.ReadAllText(RecordAlreadyExistsExceptionTemplate);
            var recordNotFoundExceptionContent = File.ReadAllText(RecordNotFoundExceptionTemplate);

            var recordAlreadyExistsExceptionFilename = Path.Combine(ExceptionsPath, "RecordAlreadyExistsException.cs");
            File.WriteAllText(recordAlreadyExistsExceptionFilename,
                              recordAlreadyExistsExceptionContent.Replace("{{solutionname}}", variable.SolutionName));

            var recordNotFoundExceptionFilename = Path.Combine(ExceptionsPath, "RecordNotFoundException.cs");
            File.WriteAllText(recordNotFoundExceptionFilename,
                              recordNotFoundExceptionContent.Replace("{{solutionname}}", variable.SolutionName));
        }

        void ScaffoldWebApi()
        {
            ConsoleUtil.HiglightedMessage($"Creating Web Api Controllers and Filters {variable.WebApiProject}");
            var WebApiPath = Path.Combine(Environment.CurrentDirectory, $"{SolutionName}/{SolutionName}.Api");

            var ControllersPath = Path.Combine(WebApiPath, "Controllers");
            var FiltersPath = Path.Combine(WebApiPath, "Filters");
            var ConfigurationsPath = Path.Combine(WebApiPath, "Configurations");

            var MediatorControllerTemplate = Path.Combine(GimmeConfiguration.TemplateDirectory, "WebApiStarter", "Api", "MediatorControllerTemplate.txt");
            var HomeControllerTemplate = Path.Combine(GimmeConfiguration.TemplateDirectory, "WebApiStarter", "Api", "HomeControllerTemplate.txt");
            var ActionValidationFilterTemplate = Path.Combine(GimmeConfiguration.TemplateDirectory, "WebApiStarter", "Api", "ActionValidationFilterAttributeTemplate.txt");
            var CustomExceptionFilterTemplate = Path.Combine(GimmeConfiguration.TemplateDirectory, "WebApiStarter", "Api", "CustomExceptionFilterAttributeTemplate.txt");

            var ConfigAutoMapperTemplate = Path.Combine(GimmeConfiguration.TemplateDirectory, "WebApiStarter", "Api", "Configuration_AutoMapper.txt");
            var ConfigFluentValidationTemplate = Path.Combine(GimmeConfiguration.TemplateDirectory, "WebApiStarter", "Api", "Configuration_FluentValidation.txt");
            var ConfigMediatRTemplate = Path.Combine(GimmeConfiguration.TemplateDirectory, "WebApiStarter", "Api", "Configuration_MediatR.txt");
            var ConfigMvcTemplate = Path.Combine(GimmeConfiguration.TemplateDirectory, "WebApiStarter", "Api", "Configuration_Mvc.txt");
            var ConfigSwaggerTemplate = Path.Combine(GimmeConfiguration.TemplateDirectory, "WebApiStarter", "Api", "Configuration_Swagger.txt");
            var StartupTemplate = Path.Combine(GimmeConfiguration.TemplateDirectory, "WebApiStarter", "Api", "Startup.txt");


            Directory.CreateDirectory(ControllersPath);
            Directory.CreateDirectory(FiltersPath);
            Directory.CreateDirectory(ConfigurationsPath);

            var mediatorControllerContent = File.ReadAllText(MediatorControllerTemplate);
            var homeControllerTemplateContent = File.ReadAllText(HomeControllerTemplate);
            var actionValidationFilterContent = File.ReadAllText(ActionValidationFilterTemplate);
            var customZExceptionFilterContent = File.ReadAllText(CustomExceptionFilterTemplate);

            var configAutoMapperContent = File.ReadAllText(ConfigAutoMapperTemplate);
            var configFluentValidationContent = File.ReadAllText(ConfigFluentValidationTemplate);
            var configMediatRContent = File.ReadAllText(ConfigMediatRTemplate);
            var configMvcContent = File.ReadAllText(ConfigMvcTemplate);
            var configSwaggerContent = File.ReadAllText(ConfigSwaggerTemplate);

            var startupContent = File.ReadAllText(StartupTemplate);


            //Mediator Controller
            var mediatorControllerFilename = Path.Combine(ControllersPath, "MediatorController.cs");
            File.WriteAllText(mediatorControllerFilename,
                              mediatorControllerContent.Replace("{{solutionname}}", variable.SolutionName));

            //Home Controller
            var homeControllerFilaname = Path.Combine(ControllersPath, "HomeController.cs");
            File.WriteAllText(homeControllerFilaname,
                              homeControllerTemplateContent.Replace("{{solutionname}}", variable.SolutionName));

            //Action Validation Filter Attribute
            var actionValidationFilterFilename = Path.Combine(FiltersPath, "ActionValidationFilterAttribute.cs");
            File.WriteAllText(actionValidationFilterFilename,
                              actionValidationFilterContent.Replace("{{solutionname}}", variable.SolutionName));


            //Custom Exception Filter Attribute
            var customExceptionFilterFilename = Path.Combine(FiltersPath, "CustomExceptionFilterAttribute.cs");
            File.WriteAllText(customExceptionFilterFilename,
                              customZExceptionFilterContent.Replace("{{solutionname}}", variable.SolutionName));


            var configAutoMapper = Path.Combine(ConfigurationsPath, "AutoMapper.cs");
            File.WriteAllText(configAutoMapper,
                              configAutoMapperContent.Replace("{{solutionname}}", variable.SolutionName));

            var configFluentValidation = Path.Combine(ConfigurationsPath, "FluentValidation.cs");
            File.WriteAllText(configFluentValidation,
                              configFluentValidationContent.Replace("{{solutionname}}", variable.SolutionName));


            var configMediatR = Path.Combine(ConfigurationsPath, "MediatR.cs");
            File.WriteAllText(configMediatR,
                              configMediatRContent.Replace("{{solutionname}}", variable.SolutionName));


            var configMvc = Path.Combine(ConfigurationsPath, "Mvc.cs");
            File.WriteAllText(configMvc,
                              configMvcContent.Replace("{{solutionname}}", variable.SolutionName));

            var configSwagger = Path.Combine(ConfigurationsPath, "Swagger.cs");
            File.WriteAllText(configSwagger,
                              configSwaggerContent.Replace("{{solutionname}}", variable.SolutionName));


            var startup = Path.Combine(WebApiPath, "Startup.cs");
            File.WriteAllText(startup,
                              startupContent.Replace("{{solutionname}}", variable.SolutionName));

        }

        void CreateGimmeSettings()
        {
            //Generate gimmesettings.json
            var SolutionDirectoryPath = Path.Combine(Environment.CurrentDirectory, variable.SolutionName);
            var templateFile = Path.Combine(GimmeConfiguration.TemplateDirectory, DefaultTemplates.GIMME_SETTINGS);
            var stringContent = File.ReadAllText(templateFile);
            var settingsfilename = Path.Combine(SolutionDirectoryPath, $"gimmesettings.json");
            File.WriteAllText(settingsfilename,
                         stringContent.Replace("{{name}}", variable.SolutionName)
            );
        }

        void CreateGitIgnore()
        {
            //Create .gitignore
            var SolutionDirectoryPath = Path.Combine(Environment.CurrentDirectory, variable.SolutionName);
            var GitIgnoreTemplatePath = Path.Combine(GimmeConfiguration.TemplateDirectory, "WebApiStarter", "GitIgnoreTemplate.txt");
            var gitignorefilename = Path.Combine(SolutionDirectoryPath, ".gitignore");
            File.WriteAllText(gitignorefilename, File.ReadAllText(GitIgnoreTemplatePath));
        }

        void DomainBaseEntity()
        {
            ConsoleUtil.HiglightedMessage($"Creating base classes {variable.DomainProject}");

            // Create Base Entity
            var DomainPath = Path.Combine(Environment.CurrentDirectory, $"{SolutionName}/{SolutionName}.Domain");
            var DomainEnumsPath = Path.Combine(DomainPath, "Enums");
            var DomainEntitiesPath = Path.Combine(DomainPath, "Entities");
            var DomainBaseClassTemplatePath = Path.Combine(GimmeConfiguration.TemplateDirectory, "WebApiStarter", "Domain", "BaseEntity.txt");
            Directory.CreateDirectory(DomainEnumsPath);
            Directory.CreateDirectory(DomainEntitiesPath);
            var templateContent = File.ReadAllText(DomainBaseClassTemplatePath);

            var ns = $"{variable.SolutionName}.Domain.Entities";
            var filename = Path.Combine(DomainEntitiesPath, "BaseEntity.cs");
            File.WriteAllText(filename,
                              templateContent.Replace("{{namespace}}", ns));
        }

        void ConfigureServicesPackages()
        {
            ConsoleUtil.HiglightedMessage($"Add package reference to {variable.ServicesProject}");
            Console.WriteLine(ShellHelper.Bash($"dotnet add {variable.ServicesProject} package RestSharp -f netcoreapp2.1"));
        }

        void ConfigurePersistancePackages()
        {
            ConsoleUtil.HiglightedMessage($"Add package reference to {variable.PersistanceProject}");
            Console.WriteLine(ShellHelper.Bash($"dotnet add {variable.PersistanceProject} package Microsoft.EntityFrameworkCore.SqlServer -f netcoreapp2.1"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {variable.PersistanceProject} package Microsoft.EntityFrameworkCore.Tools -f netcoreapp2.1"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {variable.PersistanceProject} package Microsoft.Extensions.Configuration.Json -f netcoreapp2.1"));
        }

        void ConfigureApplicationPackages()
        {
            ConsoleUtil.HiglightedMessage($"Add package reference to {variable.ApplicationProject}");
            Console.WriteLine(ShellHelper.Bash($"dotnet add {variable.ApplicationProject} package AutoMapper -f netcoreapp2.1"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {variable.ApplicationProject} package FluentValidation -f netcoreapp2.1"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {variable.ApplicationProject} package MediatR -f netcoreapp2.1"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {variable.ApplicationProject} package NJsonSchema -f netcoreapp2.1"));
        }

        void ConfigureApplicationUnitTestPackages()
        {
            ConsoleUtil.HiglightedMessage($"Add package reference to {variable.ApplicationUnitTestProject}");
            Console.WriteLine(ShellHelper.Bash($"dotnet add {variable.ApplicationProject} package Microsoft.EntityFrameworkCore.InMemory -f netcoreapp2.1"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {variable.ApplicationProject} package Moq -f netcoreapp2.1"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {variable.ApplicationProject} package nunit -f netcoreapp2.1"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {variable.ApplicationProject} package NUnit3TestAdapter -f netcoreapp2.1"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {variable.ApplicationProject} package Microsoft.NET.Test.Sdk -f netcoreapp2.1"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {variable.ApplicationProject} package AutoMapper -f netcoreapp2.1"));
        }

        void ConfigureWebApiPackages()
        {
            ConsoleUtil.HiglightedMessage($"Add package reference to {variable.WebApiProject}");
            Console.WriteLine(ShellHelper.Bash($"dotnet add {variable.WebApiProject} package AutoMapper.Extensions.Microsoft.DependencyInjection -f netcoreapp2.1"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {variable.WebApiProject} package FluentValidation.AspNetCore -f netcoreapp2.1"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {variable.WebApiProject} package MediatR -f netcoreapp2.1"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {variable.WebApiProject} package MediatR.Extensions.Microsoft.DependencyInjection -f netcoreapp2.1"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {variable.WebApiProject} package Microsoft.EntityFrameworkCore.SqlServer -f netcoreapp2.1"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {variable.WebApiProject} package Microsoft.EntityFrameworkCore.Tools -f netcoreapp2.1"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {variable.WebApiProject} package Microsoft.Extensions.Configuration.Json -f netcoreapp2.1"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {variable.WebApiProject} package NSwag.AspNetCore -f netcoreapp2.1"));
        }

        void ConfigureProjectReference()
        {
            ConsoleUtil.HiglightedMessage("Configure project reference");
            Console.WriteLine(ShellHelper.Bash($"dotnet add {variable.WebApiProject} reference {variable.ApplicationProject}"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {variable.WebApiProject} reference {variable.ServicesProject}"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {variable.WebApiProject} reference {variable.PersistanceProject}"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {variable.ApplicationProject} reference {variable.PersistanceProject}"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {variable.ApplicationProject} reference {variable.DomainProject}"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {variable.ApplicationProject} reference {variable.ServicesProject}"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {variable.ApplicationUnitTestProject} reference {variable.ApplicationProject}"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {variable.ApplicationUnitTestProject} reference {variable.PersistanceProject}"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {variable.ApplicationUnitTestProject} reference {variable.ServicesProject}"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {variable.ApplicationUnitTestProject} reference {variable.DomainProject}"));
            Console.WriteLine(ShellHelper.Bash($"dotnet add {variable.PersistanceProject} reference {variable.DomainProject}"));
        }

        void AddAllProjectsToSolution()
        {
            ConsoleUtil.HiglightedMessage("Add projects to solution");
            Console.WriteLine(ShellHelper.Bash($"dotnet sln {variable.SolutionFilePath} add {variable.WebApiProject}"));
            Console.WriteLine(ShellHelper.Bash($"dotnet sln {variable.SolutionFilePath} add {variable.ApplicationProject}"));
            Console.WriteLine(ShellHelper.Bash($"dotnet sln {variable.SolutionFilePath} add {variable.ApplicationUnitTestProject}"));
            Console.WriteLine(ShellHelper.Bash($"dotnet sln {variable.SolutionFilePath} add {variable.ServicesProject}"));
            Console.WriteLine(ShellHelper.Bash($"dotnet sln {variable.SolutionFilePath} add {variable.DomainProject}"));
            Console.WriteLine(ShellHelper.Bash($"dotnet sln {variable.SolutionFilePath} add {variable.PersistanceProject}"));
        }

        void CreatePersistanceProject()
        {
            ConsoleUtil.HiglightedMessage("Creating Persistance");
            Console.WriteLine(ShellHelper.Bash($"dotnet new classlib -n {SolutionName}.Persistance -o {SolutionName}/{SolutionName}.Persistance -f netcoreapp2.1"));
        }

        void CreateDomainProject()
        {
            ConsoleUtil.HiglightedMessage("Creating Domain");
            Console.WriteLine(ShellHelper.Bash($"dotnet new classlib -n {SolutionName}.Domain -o {SolutionName}/{SolutionName}.Domain -f netcoreapp2.1"));
        }

        void CreateServicesProject()
        {
            ConsoleUtil.HiglightedMessage("Creating Services");
            Console.WriteLine(ShellHelper.Bash($"dotnet new classlib -n {SolutionName}.Services -o {SolutionName}/{SolutionName}.Services -f netcoreapp2.1"));
        }

        void CreateApplicationProject()
        {
            ConsoleUtil.HiglightedMessage("Creating Application Project");
            Console.WriteLine(ShellHelper.Bash($"dotnet new classlib -n {SolutionName}.Application -o {SolutionName}/{SolutionName}.Application -f netcoreapp2.1"));


        }

        void DeleteAllDefaultFiles()
        {
            ConsoleUtil.HiglightedMessage("Delete All Default Files");
            var defaultFile = "Class1.cs";
            File.Delete(Path.Combine(Environment.CurrentDirectory, $"{SolutionName}/{SolutionName}.Application", defaultFile));
            File.Delete(Path.Combine(Environment.CurrentDirectory, $"{SolutionName}/{SolutionName}.Application.UnitTest", defaultFile));
            File.Delete(Path.Combine(Environment.CurrentDirectory, $"{SolutionName}/{SolutionName}.Domain", defaultFile));
            File.Delete(Path.Combine(Environment.CurrentDirectory, $"{SolutionName}/{SolutionName}.Persistance", defaultFile));
            File.Delete(Path.Combine(Environment.CurrentDirectory, $"{SolutionName}/{SolutionName}.Services", defaultFile));
        }

        void CreateApplicationUnitTestProject()
        {
            ConsoleUtil.HiglightedMessage("Creating Application Unit Test Project");
            Console.WriteLine(ShellHelper.Bash($"dotnet new classlib -n {SolutionName}.Application.UnitTest -o {SolutionName}/{SolutionName}.Application.UnitTest -f netcoreapp2.1"));
        }

        void CreateWebApiProject()
        {
            ConsoleUtil.HiglightedMessage("Creating WebApi");
            Console.WriteLine(ShellHelper.Bash($"dotnet new webapi -n {SolutionName}.Api -o {SolutionName}/{SolutionName}.Api"));
        }

        void CreateSolutionFile()
        {
            ConsoleUtil.HiglightedMessage("Creating Solution File");
            Console.WriteLine(ShellHelper.Bash($"dotnet new sln -n {SolutionName} -o {SolutionName}"));
        }

        void CreateSolutionFolder()
        {
            Console.WriteLine(ShellHelper.Bash($"mkdir {variable.SolutionName}"));
        }
    }
}

using gimme.Utils;
using System.IO;

namespace gimme.Commands.WebApiStarter
{

    public partial class Command
    {
        private void Step03_Scaffold()
        {
            ///
            ConsoleUtil.HiglightedMessage($"Creating base classes {variable.CSPROJ_DomainProjectFile}");
            var DomainPath = Path.Combine(variable.SolutionBasePath, variable.DomainProjectName);
            var DomainEnumsPath = Path.Combine(DomainPath, "Enums");
            var DomainEntitiesPath = Path.Combine(DomainPath, "Entities");

            filesService.CreateDirectory(DomainEnumsPath);
            filesService.CreateDirectory(DomainEntitiesPath);

            var baseEntity = Path.Combine(DomainEntitiesPath, "BaseEntity.cs");
            filesService.WriteAllTextToFile(baseEntity, ResourceUtil.GetResourceText("WAS_Domain_BaseEntity").Replace("{{solutionname}}", variable.SolutionName));
            
            ///
            ConsoleUtil.HiglightedMessage($"Creating Application Exceptions {variable.CSPROJ_ApplicationProjectFile}");
            var ApplicationPath = Path.Combine(variable.SolutionBasePath, variable.ApplicationProjectName);
            var ExceptionsPath = Path.Combine(ApplicationPath, "Exceptions");

            filesService.CreateDirectory(ExceptionsPath);

            var recordAlreadyExistsException = Path.Combine(ExceptionsPath, "RecordAlreadyExistsException.cs");
            filesService.WriteAllTextToFile(recordAlreadyExistsException,
                              ResourceUtil.GetResourceText("WAS_App_RecordAlreadyExistsException").Replace("{{solutionname}}", variable.SolutionName));

            var recordNotFoundException = Path.Combine(ExceptionsPath, "RecordNotFoundException.cs");
            filesService.WriteAllTextToFile(recordNotFoundException,
                              ResourceUtil.GetResourceText("WAS_App_RecordNotFoundException").Replace("{{solutionname}}", variable.SolutionName));

            ///
            ConsoleUtil.HiglightedMessage($"Creating Web Api Controllers and Filters {variable.CSPROJ_WebApiProjectFile}");
            var WebApiPath = Path.Combine(variable.SolutionBasePath, variable.ApiProjectName);

            var ControllersPath = Path.Combine(WebApiPath, "Controllers");
            var FiltersPath = Path.Combine(WebApiPath, "Filters");
            var ConfigurationsPath = Path.Combine(WebApiPath, "Configurations");

            filesService.CreateDirectory(ControllersPath);
            filesService.CreateDirectory(FiltersPath);
            filesService.CreateDirectory(ConfigurationsPath);

            //Mediator Controller
            var mediatorController = Path.Combine(ControllersPath, "MediatorController.cs");
            filesService.WriteAllTextToFile(mediatorController,
                                ResourceUtil.GetResourceText("WAS_Api_MediatorController").Replace("{{solutionname}}", variable.SolutionName));

            //Home Controller
            var homeControllerFilaname = Path.Combine(ControllersPath, "HomeController.cs");
            filesService.WriteAllTextToFile(homeControllerFilaname,
                              ResourceUtil.GetResourceText("WAS_Api_HomeController").Replace("{{solutionname}}", variable.SolutionName));

            //Action Validation Filter Attribute
            var actionValidationFilterAtt = Path.Combine(FiltersPath, "ActionValidationFilterAttribute.cs");
            filesService.WriteAllTextToFile(actionValidationFilterAtt,
                              ResourceUtil.GetResourceText("WAS_Api_ActionValidationFilterAttribute").Replace("{{solutionname}}", variable.SolutionName));

            //Custom Exception Filter Attribute
            var customExceptionFilterAtt = Path.Combine(FiltersPath, "CustomExceptionFilterAttribute.cs");
            filesService.WriteAllTextToFile(customExceptionFilterAtt,
                              ResourceUtil.GetResourceText("WAS_Api_CustomExceptionFilterAttribute").Replace("{{solutionname}}", variable.SolutionName));

            var configAutoMapper = Path.Combine(ConfigurationsPath, "AutoMapper.cs");
            filesService.WriteAllTextToFile(configAutoMapper,
                              ResourceUtil.GetResourceText("WAS_Api_Conf_AutoMapper").Replace("{{solutionname}}", variable.SolutionName));

            var configFluentValidation = Path.Combine(ConfigurationsPath, "FluentValidation.cs");
            filesService.WriteAllTextToFile(configFluentValidation,
                              ResourceUtil.GetResourceText("WAS_Api_Conf_FluentValidation").Replace("{{solutionname}}", variable.SolutionName));

            var configMediatR = Path.Combine(ConfigurationsPath, "MediatR.cs");
            filesService.WriteAllTextToFile(configMediatR,
                              ResourceUtil.GetResourceText("WAS_Api_Conf_MediatR").Replace("{{solutionname}}", variable.SolutionName));


            var configMvc = Path.Combine(ConfigurationsPath, "Mvc.cs");
            filesService.WriteAllTextToFile(configMvc,
                              ResourceUtil.GetResourceText("WAS_Api_Conf_Mvc").Replace("{{solutionname}}", variable.SolutionName));

            var configSwagger = Path.Combine(ConfigurationsPath, "Swagger.cs");
            filesService.WriteAllTextToFile(configSwagger,
                              ResourceUtil.GetResourceText("WAS_Api_Conf_Swagger").Replace("{{solutionname}}", variable.SolutionName));

            var configDb = Path.Combine(ConfigurationsPath, "Db.cs");
            filesService.WriteAllTextToFile(configDb,
                              ResourceUtil.GetResourceText("WAS_Api_Conf_Db").Replace("{{solutionname}}", variable.SolutionName));

            var startup = Path.Combine(WebApiPath, "Startup.cs");
            filesService.WriteAllTextToFile(startup,
                              ResourceUtil.GetResourceText("WAS_Api_Startup").Replace("{{solutionname}}", variable.SolutionName));

            var appSettingsJsonWebApi = Path.Combine(WebApiPath, "appsettings.json");
            filesService.WriteAllTextToFile(appSettingsJsonWebApi,
                              ResourceUtil.GetResourceText("WAS_AppSettingsJson").Replace("{{solutionname}}", variable.SolutionName));

            ///
            ConsoleUtil.HiglightedMessage($"Creating DbContext {variable.CSPROJ_PersistenceProjectFile}");
            var PersistencePath = Path.Combine(variable.SolutionBasePath, variable.PersistenceProjectName);
            var contextPath = Path.Combine(PersistencePath, "Context");
            filesService.CreateDirectory(contextPath);

            var dbContext = $"{variable.SolutionName}DbContext";
            var dbContextFactory = $"{variable.SolutionName}DbContextFactory";

            var dbContextFile = Path.Combine(contextPath, $"{dbContext}.cs");
            filesService.WriteAllTextToFile(dbContextFile,
                                ResourceUtil.GetResourceText("WAS_Persistence_DbContext").Replace("{{solutionname}}", variable.SolutionName));

            var dbContextFactoryFile = Path.Combine(contextPath, $"{dbContextFactory}.cs");
            filesService.WriteAllTextToFile(dbContextFactoryFile,
                    ResourceUtil.GetResourceText("WAS_Persistence_DbContextFactory").Replace("{{solutionname}}", variable.SolutionName));

            var appSettingsJsonPersistence = Path.Combine(PersistencePath, "appsettings.json");
            filesService.WriteAllTextToFile(appSettingsJsonPersistence,
                              ResourceUtil.GetResourceText("WAS_AppSettingsJson").Replace("{{solutionname}}", variable.SolutionName));
        }
    }
}

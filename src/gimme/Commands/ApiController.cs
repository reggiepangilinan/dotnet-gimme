using gimme.Services.Files;
using gimme.Utils;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;

namespace gimme.Commands
{
    [Command(Name = Constants.Command.API_CONTROLLER, Description = "Web Api Controller that derives from Mediator Controller")]
    public class ApiController
    {
        private IFilesService filesService;

        [Required(ErrorMessage = "You must specify the controller name.")]
        [Argument(1, Description = "Name of the controller")]
        public string ControllerName { get; }


        public ApiController(IFilesService filesService)
        {
            this.filesService = filesService;
        }

        public void OnExecute()
        {
            //if (!filesService.DirectoryExists(GimmeConfiguration.WebApiProjectName))
            //    throw new DirectoryNotFoundException($"Could not find Web Api Project Folder '{GimmeConfiguration.WebApiProjectName}'");

            var apiDirectory = Path.Combine(
                Environment.CurrentDirectory,
                                                GimmeConfiguration.WebApiProjectName,
                                                "Controllers"
                                               );
            var controllerCs = $"{ControllerName}.cs";
            var controllernamespace = $"{GimmeConfiguration.WebApiProjectName.Replace("\\",".")}.Controllers";
            var appnamespace = $"{GimmeConfiguration.ApplicationProjectName.Replace("\\", ".")}";

            var controllerFile = Path.Combine(apiDirectory, controllerCs);

            if (!filesService.DirectoryExists(apiDirectory))
                filesService.CreateDirectory(apiDirectory);

            filesService.WriteAllTextToFile(controllerFile,
                  ResourceUtil.GetResourceText("Api_Controller")
                  .Replace("{{namespace}}", controllernamespace)
                  .Replace("{{appnamespace}}", appnamespace)
                  .Replace("{{name}}", ControllerName)
                 );

            ConsoleUtil.SuccessMessage($"Mediator Controller succesfully generated!");
        }
    }
}

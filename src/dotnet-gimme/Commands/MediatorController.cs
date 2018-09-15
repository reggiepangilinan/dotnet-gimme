using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using dotnet_gimme;
using dotnetgimme.Constants;
using dotnetgimme.Utils;
using McMaster.Extensions.CommandLineUtils;

namespace dotnetgimme.Commands
{
    [Command(Name = "api-mediator-controller", Description = "Web Api Controller that derives from Mediator Controller")]
    public class MediatorController
    {
        [Required(ErrorMessage = "You must specify the controller name.")]
        [Argument(1, Description = "Name of the controller")]
        public string ControllerName { get; }

        public void OnExecute()
        {
            if (!Directory.Exists(GimmeConfiguration.ApplicationProjectName))
                throw new DirectoryNotFoundException($"Could not find Web Api Project Folder '{GimmeConfiguration.WebApiProjectName}'");
            var workingDirectory = Path.Combine(
                Environment.CurrentDirectory,
                                                GimmeConfiguration.WebApiProjectName,
                                                "Controllers"
                                               );
            var controllerFileName = $"{ControllerName}.cs";
            var nameSpace = $"{GimmeConfiguration.WebApiProjectName}.Controllers";
            var outputFile = Path.Combine(workingDirectory, controllerFileName);
            if (!Directory.Exists(workingDirectory))
                Directory.CreateDirectory(workingDirectory);
            var templateFile = Path.Combine(GimmeConfiguration.TemplateDirectory, DefaultTemplates.MEDIATOR_CONTROLLER);
            if (!File.Exists(templateFile))
                throw new FileNotFoundException(ExceptionHelper.FileNotFoundMessage(templateFile));
            var stringContent = File.ReadAllText(templateFile);
            File.WriteAllText(outputFile,
                  stringContent
                  .Replace("{{namespace}}", nameSpace)
                              .Replace("{{name}}", ControllerName)
                 );
            ConsoleUtil.SuccessMessage($"Mediator Controller succesfully generated!");
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using dotnet_gimme;
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

            string currentDirectory = Environment.CurrentDirectory;

            var workingDirectory = Path.Combine(
                                                currentDirectory,
                                                GimmeConfiguration.WebApiProjectName,
                                                "Controllers"
                                               );

            if (!Directory.Exists(workingDirectory))
                Directory.CreateDirectory(workingDirectory);

            var templateFile = Path.Combine(GimmeConfiguration.TemplateDirectory, "MediatorControllerTemplate.txt");

            if (!File.Exists(templateFile))
                throw new FileNotFoundException($"Cannot find file {templateFile}");

            var controllerFileName = $"{ControllerName}.cs";

            var nameSpace = $"{GimmeConfiguration.WebApiProjectName}.Controllers";

            var stringContent = File.ReadAllText(templateFile);

            var exceptionOutputFilePath = Path.Combine(workingDirectory, controllerFileName);

            File.WriteAllText(exceptionOutputFilePath,
                  stringContent
                  .Replace("{{namespace}}", nameSpace)
                              .Replace("{{name}}", ControllerName)
                 );

            ConsoleUtil.SuccessMessage($"Mediator Controller succesfully generated!");
        }
    }
}

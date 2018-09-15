using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using dotnet_gimme;
using dotnetgimme.Constants;
using dotnetgimme.Utils;
using McMaster.Extensions.CommandLineUtils;

namespace dotnetgimme.Commands
{
    [Command(Name = "app-model", Description = "Application Model")]
    public class ApplicationModel
    {
        [Required(ErrorMessage = "You must specify the group name.")]
        [Argument(0, Description = "The logical grouping that the command belongs to.")]
        public string GroupName { get; }

        [Required(ErrorMessage = "You must specify the model name.")]
        [Argument(1, Description = "Name of the model")]
        public string ModelName { get; }

        public void OnExecute()
        {
            if (!Directory.Exists(GimmeConfiguration.ApplicationProjectName))
                throw new DirectoryNotFoundException(ApplicationProject.Message.CannotFindApplicationFolder);
            
            var filename = $"{ModelName}.cs";
            var nameSpace = $"{GimmeConfiguration.ApplicationProjectName}.{GroupName}.Models";
            var workingDirectory = Path.Combine(
                                                Environment.CurrentDirectory,
                                                GimmeConfiguration.ApplicationProjectName,
                                                GroupName,
                                                "Models"
                                               );
            var outputPath = Path.Combine(workingDirectory, filename);

            if (!Directory.Exists(workingDirectory))
                Directory.CreateDirectory(workingDirectory);

            var templateFile = Path.Combine(GimmeConfiguration.TemplateDirectory, DefaultTemplates.APP_MODEL);

            if (!File.Exists(templateFile))
                throw new FileNotFoundException(ExceptionHelper.FileNotFoundMessage(templateFile));

            var templateContent = File.ReadAllText(templateFile);

            File.WriteAllText(outputPath,
                              templateContent
                              .Replace("{{namespace}}", nameSpace)
                              .Replace("{{name}}", ModelName)
                             );

            ConsoleUtil.SuccessMessage($"Model succesfully generated!");
        }
    }
}

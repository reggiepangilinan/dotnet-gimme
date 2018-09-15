using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using dotnet_gimme;
using dotnetgimme.Constants;
using dotnetgimme.Utils;
using McMaster.Extensions.CommandLineUtils;

namespace dotnetgimme.Commands
{
    [Command(Name = "app-exception", Description = "Application Exception")]
    public class ApplicationException
    {

        [Required(ErrorMessage = "You must specify the exception name.")]
        [Argument(1, Description = "Name of the exception")]
        public string ExceptionName { get; }

        public void OnExecute()
        {
            if (!Directory.Exists(GimmeConfiguration.ApplicationProjectName))
                throw new DirectoryNotFoundException(ApplicationProject.Message.CannotFindApplicationFolder);

            var outputFile = $"{ExceptionName}.cs";
            var nameSpace = $"{GimmeConfiguration.ApplicationProjectName}.Exceptions";
            var workingDirectory = Path.Combine(
                                                Environment.CurrentDirectory,
                                                GimmeConfiguration.ApplicationProjectName,
                                                "Exceptions"
                                               );
            var exceptionOutputFilePath = Path.Combine(workingDirectory, outputFile);

            if (!Directory.Exists(workingDirectory))
                Directory.CreateDirectory(workingDirectory);

            var template = Path.Combine(GimmeConfiguration.TemplateDirectory, DefaultTemplates.APP_EXCEPTION);

            if (!File.Exists(template))
                throw new FileNotFoundException(ExceptionHelper.FileNotFoundMessage(template));
            
            var templateContent = File.ReadAllText(template);

            File.WriteAllText(exceptionOutputFilePath,
                              templateContent
                  .Replace("{{namespace}}", nameSpace)
                  .Replace("{{name}}", ExceptionName)
                 );
            
            ConsoleUtil.SuccessMessage($"Exception succesfully generated!");
        }
    }
}


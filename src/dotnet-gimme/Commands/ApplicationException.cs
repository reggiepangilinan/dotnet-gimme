using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using dotnet_gimme;
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
                throw new DirectoryNotFoundException($"Could not find Application Project Folder '{GimmeConfiguration.ApplicationProjectName}'");

            string currentDirectory = Environment.CurrentDirectory;

            var workingDirectory = Path.Combine(
                                                currentDirectory,
                                                GimmeConfiguration.ApplicationProjectName,
                                                "Exceptions"
                                               );

            if (!Directory.Exists(workingDirectory))
                Directory.CreateDirectory(workingDirectory);

            var exceptionTemplateFile = Path.Combine(GimmeConfiguration.TemplateDirectory, "ApplicationExceptionTemplate.txt");

            if (!File.Exists(exceptionTemplateFile))
                throw new FileNotFoundException($"Cannot find file {exceptionTemplateFile}");

            var exceptionFilename = $"{ExceptionName}.cs";

            var nameSpace = $"{GimmeConfiguration.ApplicationProjectName}.Exceptions";

            var exceptionStringContent = File.ReadAllText(exceptionTemplateFile);

            var exceptionOutputFilePath = Path.Combine(workingDirectory, exceptionFilename);

            File.WriteAllText(exceptionOutputFilePath,
                  exceptionStringContent
                  .Replace("{{namespace}}", nameSpace)
                  .Replace("{{name}}", ExceptionName)
                 );

            ConsoleUtil.SuccessMessage($"Exception succesfully generated!");
        }
    }
}


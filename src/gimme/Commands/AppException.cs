using gimme.Services.Files;
using gimme.Utils;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace gimme.Commands
{
    [Command(Name = Constants.Command.APP_EXCEPTION, Description = "Application Exception")]
    public class AppException
    {
        private readonly IFilesService filesService;

        [Required(ErrorMessage = "You must specify the exception name.")]
        [Argument(1, Description = "Name of the exception")]
        public string ExceptionName { get; }

        public AppException(IFilesService filesService)
        {
            this.filesService = filesService;
        }

        public void OnExecute()
        {
            //if (!filesService.DirectoryExists(GimmeConfiguration.ApplicationProjectName))
            //    throw new DirectoryNotFoundException($"Directory `{GimmeConfiguration.ApplicationProjectName}` does not exists.");

            var exceptionCs = $"{ExceptionName}.cs";
            var appnamespace = $"{GimmeConfiguration.ApplicationProjectName.Replace("\\", ".")}.Exceptions";
            var appDirectory = Path.Combine(
                                                Environment.CurrentDirectory,
                                                GimmeConfiguration.ApplicationProjectName,
                                                "Exceptions"
                                               );
            var exceptionOutputFilePath = Path.Combine(appDirectory, exceptionCs);

            if (!filesService.DirectoryExists(appDirectory))
                filesService.CreateDirectory(appDirectory);


            File.WriteAllText(exceptionOutputFilePath,
                              ResourceUtil.GetResourceText("App_Exception")
                  .Replace("{{namespace}}", appnamespace)
                  .Replace("{{name}}", ExceptionName)
                 );

            ConsoleUtil.SuccessMessage($"Exception succesfully generated!");
        }
    }
}

using gimme.Services.Files;
using gimme.Utils;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace gimme.Commands
{
    [Command(Name = "app-command", Description = "Application Command (CQRS)")]
    public class AppCommand
    {
        private readonly IFilesService filesService;

        [Required(ErrorMessage = "You must specify the group name.")]
        [Argument(0, Description = "The logical grouping that the command belongs to.")]
        public string GroupName { get; }

        [Required(ErrorMessage = "You must specify the command name.")]
        [Argument(1, Description = "Name of the command")]
        public string CommandName { get; }

        public AppCommand(IFilesService filesService)
        {
            this.filesService = filesService;
        }

        public void OnExecute()
        {
            if (!filesService.DirectoryExists(GimmeConfiguration.ApplicationProjectName))
                throw new DirectoryNotFoundException($"Directory `{GimmeConfiguration.ApplicationProjectName}` does not exists.");

            var commandCs = "Command.cs";
            var validatorCs = "CommandValidator.cs";


            var appNamespace = $"{GimmeConfiguration.ApplicationProjectName}.{GroupName}.Commands.{CommandName}";
            var appDirectory = Path.Combine(
                                                Environment.CurrentDirectory,
                                                GimmeConfiguration.ApplicationProjectName,
                                                GroupName,
                                                "Commands",
                                                CommandName
                                               );

            if (!filesService.DirectoryExists(appDirectory))
                filesService.CreateDirectory(appDirectory);

            var commandFile = Path.Combine(appDirectory, commandCs);
            var validtorFile = Path.Combine(appDirectory, validatorCs);

            filesService.WriteAllTextToFile(commandFile,
                              ResourceUtil.GetResourceText("App_Command")
                              .Replace("{{namespace}}", appNamespace)
                              .Replace("{{name}}", CommandName)
                             );

            filesService.WriteAllTextToFile(validtorFile,
                              ResourceUtil.GetResourceText("App_Command_Validator")
                  .Replace("{{namespace}}", appNamespace)
                 );

            ConsoleUtil.SuccessMessage($"Command succesfully generated!");
        }
    }
}

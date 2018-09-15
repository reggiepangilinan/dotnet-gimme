using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using dotnet_gimme;
using dotnetgimme.Utils;
using McMaster.Extensions.CommandLineUtils;
using static dotnetgimme.Utils.ExceptionHelper;
using static dotnetgimme.Constants.DefaultTemplates;

namespace dotnetgimme.Commands
{
    [Command(Name = "app-command", Description = "Application Command (CQRS)")]
    public class ApplicationCommand
    {

        [Required(ErrorMessage = "You must specify the group name.")]
        [Argument(0, Description = "The logical grouping that the command belongs to.")]
        public string GroupName { get; }

        [Required(ErrorMessage = "You must specify the command name.")]
        [Argument(1, Description = "Name of the command")]
        public string CommandName { get; }

        public void OnExecute()
        {
            if (!Directory.Exists(GimmeConfiguration.ApplicationProjectName))
                throw new DirectoryNotFoundException(ApplicationProject.Message.CannotFindApplicationFolder);

            var commandFilename = "Command.cs";
            var commandValidatorFilename = "CommandValidator.cs";
            var applicationNamespace = $"{GimmeConfiguration.ApplicationProjectName}.{GroupName}.Commands.{CommandName}";
            var applicationDirectory = Path.Combine(
                                                Environment.CurrentDirectory,
                                                GimmeConfiguration.ApplicationProjectName,
                                                GroupName,
                                                "Commands",
                                                CommandName
                                               );

            if (!Directory.Exists(applicationDirectory))
                Directory.CreateDirectory(applicationDirectory);

            var commandTemplateFile = Path.Combine(GimmeConfiguration.TemplateDirectory, APP_COMMAND);
            var commandValidatorTemplateFile = Path.Combine(GimmeConfiguration.TemplateDirectory, APP_COMMAND_VALIDATOR);

            if (!File.Exists(commandTemplateFile))
                throw new FileNotFoundException(FileNotFoundMessage(commandTemplateFile));

            if (!File.Exists(commandValidatorTemplateFile))
                throw new FileNotFoundException(FileNotFoundMessage(commandValidatorTemplateFile));


            var commandString = File.ReadAllText(commandTemplateFile);
            var commandValidatorString = File.ReadAllText(commandValidatorTemplateFile);

            var commandOutputFilePath = Path.Combine(applicationDirectory, commandFilename);
            var commandValidatorOutputFilePath = Path.Combine(applicationDirectory, commandValidatorFilename);

            File.WriteAllText(commandOutputFilePath,
                              commandString
                              .Replace("{{namespace}}", applicationNamespace)
                              .Replace("{{name}}", CommandName)
                             );

            File.WriteAllText(commandValidatorOutputFilePath,
                              commandValidatorString
                  .Replace("{{namespace}}", applicationNamespace)
                 );

            ConsoleUtil.SuccessMessage($"Command succesfully generated!");
        }
    }
}

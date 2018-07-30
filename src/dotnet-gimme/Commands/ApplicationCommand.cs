using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using dotnet_gimme;
using dotnetgimme.Utils;
using McMaster.Extensions.CommandLineUtils;

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
                throw new DirectoryNotFoundException($"Could not find Application Project Folder '{GimmeConfiguration.ApplicationProjectName}'");
            
            string currentDirectory = Environment.CurrentDirectory;

            var workingDirectory = Path.Combine(
                                                currentDirectory,
                                                GimmeConfiguration.ApplicationProjectName,
                                                GroupName,
                                                "Commands",
                                                CommandName
                                               );

            if (!Directory.Exists(workingDirectory))
                Directory.CreateDirectory(workingDirectory);


            var commandTemplateFile = Path.Combine(GimmeConfiguration.TemplateDirectory, "ApplicationCommandTemplate.txt");
            var commandValidatorTemplateFile = Path.Combine(GimmeConfiguration.TemplateDirectory, "ApplicationCommandValidatorTemplate.txt");


            if (!File.Exists(commandTemplateFile))
                throw new FileNotFoundException($"Cannot find file {commandTemplateFile}");


            if (!File.Exists(commandValidatorTemplateFile))
                throw new FileNotFoundException($"Cannot find file {commandValidatorTemplateFile}");

            var commandFilename = "Command.cs";
            var commandValidatorFilename = "CommandValidator.cs";

            var nameSpace = $"{GimmeConfiguration.ApplicationProjectName}.{GroupName}.Commands.{CommandName}";

            var commandString = File.ReadAllText(commandTemplateFile);
            var commandValidatorString = File.ReadAllText(commandValidatorTemplateFile);

            var commandOutputFilePath = Path.Combine(workingDirectory, commandFilename);
            var commandValidatorOutputFilePath = Path.Combine(workingDirectory, commandValidatorFilename);

            File.WriteAllText(commandOutputFilePath,
                              commandString
                              .Replace("{{namespace}}", nameSpace)
                              .Replace("{{name}}", CommandName)
                             );

            File.WriteAllText(commandValidatorOutputFilePath,
                              commandValidatorString
                  .Replace("{{namespace}}", nameSpace)
                 );
            ConsoleUtil.SuccessMessage($"Command succesfully generated!");
        }
    }
}
